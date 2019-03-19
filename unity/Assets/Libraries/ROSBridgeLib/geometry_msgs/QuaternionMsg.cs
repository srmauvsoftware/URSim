using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib {
	namespace geometry_msgs {
		public class QuaternionMsg : ROSBridgeMsg {
			private double _x, _y, _z, _w;

			public QuaternionMsg(JSONNode msg) {
				_x = double.Parse(msg["x"]);
				_y = double.Parse (msg["y"]);
				_z = double.Parse (msg["z"]);
				_w = double.Parse (msg ["w"]);
				//Debug.Log ("Quaternion Message Done");
			}

			public QuaternionMsg(double x, double y, double z, double w) {
				_x = x;
				_y = y;
				_z = z;
				_w = w;
			}

			public static string getMessageType() {
				return "geometry_msgs/Quaternion";
			}

			public double GetX() {
				return _x;
			}

			public double GetY() {
				return _y;
			}

			public double GetZ() {
				return _z;
			}

			public double GetW() {
				return _w;
			}

			public override string ToString() {
				return "Quaternion [x=" + _x + ",  y=" + _y + ", z=" + _z +  
					", w=" + _w + "]";
			}

			public override string ToYAMLString() {
				return "{\"x\": " + _x + ", \"y\": " + _y + ", \"z\": " + _z + 
					", \"w\": " + _w + "}";
			}
		}
	}
}

