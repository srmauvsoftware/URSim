using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using SimpleJSON;
using UnityEngine;

/**
 * This class handles the connection with the external ROS world, deserializing
 * json messages into appropriate instances of packets and messages.
 * 
 * This class also provides a mechanism for having the callback's exectued on the rendering thread.
 * (Remember, Unity has a single rendering thread, so we want to do all of the communications stuff away
 * from that. 
 * 
 * The one other clever thing that is done here is that we only keep 1 (the most recent!) copy of each message type
 * that comes along.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */

namespace ROSBridgeLib {
	public class ROSBridgeWebSocketConnection {
		private class RenderTask {
			private Type _subscriber;
			private string _topic;
			private ROSBridgeMsg _msg;

			public RenderTask(Type subscriber, string topic, ROSBridgeMsg msg) {
				_subscriber = subscriber;
				_topic = topic;
				_msg = msg;
			}

			public Type getSubscriber() {
				return _subscriber;
			}

			public ROSBridgeMsg getMsg() {
				return _msg;
			}

			public string getTopic() {
				return _topic;
			}
		};
		private string _host;
		private int _port;
		private WebSocket _ws;
		private System.Threading.Thread _myThread;
		private List<Type> _subscribers; // our subscribers
		private List<Type> _publishers; //our publishers
		private Type _serviceResponse; // to deal with service responses
		private string _serviceName = null;
		private string _serviceValues = null;
		private List<RenderTask> _taskQ = new List<RenderTask>();

		private object _queueLock = new object ();

		private static string GetMessageType(Type t) {
			return (string) t.GetMethod ("GetMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, null);
		}

		private static string GetMessageTopic(Type t) {
			return (string) t.GetMethod ("GetMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, null);
		}

		private static ROSBridgeMsg ParseMessage(Type t, JSONNode node) {
			return (ROSBridgeMsg) t.GetMethod ("ParseMessage", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {node});
		}

		private static void Update(Type t, ROSBridgeMsg msg) {
			t.GetMethod ("CallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {msg});
		}

		private static void ServiceResponse(Type t, string service, string yaml) {
			t.GetMethod ("ServiceCallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {service, yaml});
		}

		private static void IsValidServiceResponse(Type t) {
			if (t.GetMethod ("ServiceCallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("invalid service response handler");
		}

		private static void IsValidSubscriber(Type t) {
			if(t.GetMethod ("CallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing Callback method");
			if (t.GetMethod ("GetMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageType method");
			if(t.GetMethod ("GetMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageTopic method");
			if(t.GetMethod ("ParseMessage", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing ParseMessage method");
		}

		private static void IsValidPublisher(Type t) {
			if (t.GetMethod ("GetMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageType method");
			if(t.GetMethod ("GetMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageTopic method");
		}

		/**
		 * Make a connection to a host/port. 
		 * This does not actually start the connection, use Connect to do that.
		 */
		public ROSBridgeWebSocketConnection(string host, int port) {
			_host = host;
			_port = port;
			_myThread = null;
			_subscribers = new List<Type> ();
			_publishers = new List<Type> ();
		}

		/**
		 * Add a service response callback to this connection.
		 */
		public void AddServiceResponse(Type serviceResponse) {
			IsValidServiceResponse (serviceResponse);
			_serviceResponse = serviceResponse;
		}

		/**
		 * Add a subscriber callback to this connection. There can be many subscribers.
		 */
		public void AddSubscriber(Type subscriber) {
			IsValidSubscriber(subscriber);
			_subscribers.Add (subscriber);
		}

		/**
		 * Add a publisher to this connection. There can be many publishers.
		 */
		public void AddPublisher(Type publisher) {
			IsValidPublisher(publisher);
			_publishers.Add (publisher);
		}

		/**
		 * Connect to the remote ros environment.
		 */
		public void Connect() {
			_myThread = new System.Threading.Thread (Run);
			_myThread.Start ();
		}

		/**
		 * Disconnect from the remote ros environment.
		 */
		public void Disconnect() {
			_myThread.Abort ();
			foreach(Type p in _subscribers) {
				_ws.Send(ROSBridgeMsg.UnSubscribe(GetMessageTopic(p)));
				//Debug.Log ("Sending " + ROSBridgePacket.unSubscribe(getMessageTopic(p)));
			}
			foreach(Type p in _publishers) {
				_ws.Send(ROSBridgeMsg.UnAdvertise (GetMessageTopic(p)));
				//Debug.Log ("Sending " + ROSBridgePacket.unAdvertise (getMessageTopic(p)));
			}
			_ws.Close ();
		}

		private void Run() {
			_ws = new WebSocket(_host + ":" + _port);
			_ws.OnMessage += (sender, e) => this.OnMessage(e.Data);
			_ws.Connect();

			foreach(Type p in _subscribers) {
				_ws.Send(ROSBridgeMsg.Subscribe (GetMessageTopic(p), GetMessageType (p)));
				// Debug.Log ("Sending " + ROSBridgeMsg.Subscribe (GetMessageTopic(p), GetMessageType (p)));
			}
			foreach(Type p in _publishers) {
				_ws.Send(ROSBridgeMsg.Advertise (GetMessageTopic(p), GetMessageType(p)));
				// Debug.Log ("Sending " + ROSBridgeMsg.Advertise (GetMessageTopic(p), GetMessageType(p)));
			}
			while(true) {
				Thread.Sleep (10000);
			}
		}

		private void OnMessage(string s) {
			Debug.Log ("Got a message " + s);
			if((s!= null) && !s.Equals ("")) {
				JSONNode node = JSONNode.Parse(s);
				Debug.Log ("Parsed it");
				string op = node["op"];
				Debug.Log ("Operation is " + op);
				if("publish".Equals (op)) {
					string topic = node["topic"];
					Debug.Log ("Got a message on " + topic);
					foreach(Type p in _subscribers) {
						if(topic.Equals (GetMessageTopic (p))) {
							Debug.Log ("And will parse it " + GetMessageTopic (p));
							ROSBridgeMsg msg = ParseMessage(p, node["msg"]);
							RenderTask newTask = new RenderTask(p, topic, msg);
							lock(_queueLock) {
								bool found = false;
								for(int i=0;i<_taskQ.Count;i++) {
									if(_taskQ[i].getTopic().Equals (topic)) {
										_taskQ.RemoveAt (i);
										_taskQ.Insert (i, newTask);
										found = true;
										break;
									}
								}
								if(!found)
									_taskQ.Add (newTask);
							}

						}
					}
				} else if("service_response".Equals (op)) {
					Debug.Log ("Got service response " + node.ToString ());
					_serviceName = node["service"];
					_serviceValues = (node["values"] == null) ? "" : node["values"].ToString ();
				} else
					Debug.Log ("Must write code here for other messages");
			} else
				Debug.Log ("Got an empty message from the web socket");
		}

		public void Render() {
			RenderTask newTask = null;
			lock (_queueLock) {
				if(_taskQ.Count > 0) {
					newTask = _taskQ[0];
					_taskQ.RemoveAt (0);
				}
			}
			if(newTask != null)
				Update(newTask.getSubscriber (), newTask.getMsg ());

			if (_serviceName != null) {
				ServiceResponse (_serviceResponse, _serviceName, _serviceValues);
				_serviceName = null;
			}
		}

		public void Publish(String topic, ROSBridgeMsg msg) {
			if(_ws != null) {
				string s = ROSBridgeMsg.Publish (topic, msg.ToYAMLString ());
				// Debug.Log ("Sending " + s);
				_ws.Send (s);
			}
		}

		public void CallService(string service, string args) {
			if (_ws != null) {
				string s = ROSBridgeMsg.CallService (service, args);
				// Debug.Log ("Sending " + s);
				_ws.Send (s);
			}
		}
	}
}
