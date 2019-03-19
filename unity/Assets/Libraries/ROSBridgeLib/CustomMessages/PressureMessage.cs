using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib {
	namespace CustomMessages {
		public class PressureMessage : ROSBridgeMsg {
			private float _data;
			
			public PressureMessage(JSONNode msg) {
				_data = float.Parse(msg["data"]);
			}
			
			public PressureMessage(float data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "alpheus_msgs/pressure";
			}
			
			public float GetData() {
				return _data;
			}
			
			public override string ToString() {
                return "alpheus_msgs/pressure [pressure=" + _data + "]";
			}
			
			public override string ToYAMLString() {
                return "{\"pressure\" : " + _data + "}";
			}
		}
	}
}