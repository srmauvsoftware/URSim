using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib {
	namespace CustomMessages {
		public class Pose2DMessage : ROSBridgeMsg {
			private float _x,_y,_theta;
			
            public Pose2DMessage(JSONNode msg) {
				_x = float.Parse(msg["x"]);
				_y = float.Parse(msg["y"]);
				_theta = float.Parse(msg["theta"]);
			}
			
            public Pose2DMessage(float x, float y, float theta) {
				_x = x;
				_y = y;
				_theta = theta;
			}
			
			public static string GetMessageType() {
				return "geometry_msgs/Pose2D";
			}
			
			public float GetX() {
				return _x;
			}
			
            public float GetY() {
				return _y;
			}
			
            public float GetTheta() {
				return _theta;
			}
			
			public override string ToString() {
                return "geometry_msgs/Pose2D [x=" + _x + " ,y=" + _y + " ,theta=" + _theta+ "]";
			}
			
			public override string ToYAMLString() {
                return "{\"x\" : " + _x + ",\"y\" : " + _y + ",\"theta\" : "+ _theta+"}";
			}
		}
	}
}