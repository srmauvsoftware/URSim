using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class ImuSubscriberCallback : ROSBridgeSubscriber {

	public new static string GetMessageTopic() {
		return "/imu/data";
	}  

	public new static string GetMessageType() {
		return "sensor_msgs/Imu";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new ImuMessage(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {
		//Debug.Log (GetMessageTopic () + " received");
		ImuMessage orientation = (ImuMessage)msg;
		GameObject capsule = GameObject.Find ("Capsule");
		if (capsule == null)
			Debug.Log ("Cant Find AUV");
		else{
			float x = (float)orientation.GetQuaternionIMU ().GetX ();
			float y = (float)orientation.GetQuaternionIMU ().GetY ();
			float z = (float)orientation.GetQuaternionIMU ().GetZ ();
			float w = (float)orientation.GetQuaternionIMU ().GetW ();
			capsule.transform.rotation = new Quaternion (x, y, z, w);
		}
	}
}