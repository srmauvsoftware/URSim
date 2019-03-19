using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib {
	namespace CustomMessages {
		public class DepthThrusterMsg : ROSBridgeMsg {
			private int _td1, _td2, _td3, _td4;

			public DepthThrusterMsg(JSONNode msg) {
				_td1 = int.Parse(msg["td1"]);
				_td2 = int.Parse (msg["td2"]);
				_td3 = int.Parse (msg["td3"]);
				_td4 = int.Parse (msg ["td4"]);
				//Debug.Log ("Thruster Message Parsed");
			}

			public void DepthThrusterMessage(int td1, int td2, int td3, int td4) {
				_td1 = td1;
				_td2 = td2;
				_td3 = td3;
				_td4 = td4;
			}

			public static string getMessageType() {
				return "thrusters/DepthThrusterMsg";
			}

			public int Gettd1() {
				return _td1;
			}
			public int Gettd2() {
				return _td2;
			}
			public int Gettd3() {
				return _td3;
			}
			public int Gettd4() {
				return _td4;
			}

			public override string ToString() {
				return "thrusters/DepthThrusterMsg [td1=" + _td1 +
						", td2=" + _td2 + 
						", td3=" + _td3 +  
						", td4=" + _td4 +
						"]";
			}

			public override string ToYAMLString() {
				return "{\"td1\": " + _td1 + 
						", \"td2\": " + _td2 + 
						", \"td3\": " + _td3 + 
						", \"td4\": " + _td4 + 
						" }";
			}
		}
	}
}


