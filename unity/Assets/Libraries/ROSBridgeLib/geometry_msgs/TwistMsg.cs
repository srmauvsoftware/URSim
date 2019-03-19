using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace geometry_msgs {
		public class TwistMsg : ROSBridgeMsg {
			private Vector3Msg _linear;
			private Vector3Msg _angular;
			
			public TwistMsg(JSONNode msg) {
				_linear = new Vector3Msg(msg["linear"]);
				_angular = new Vector3Msg(msg["angular"]);
			}
			
			public TwistMsg(Vector3Msg linear, Vector3Msg angular) {
				_linear = linear;
				_angular = angular;
			}
			
			public static string GetMessageType() {
				return "geometry_msgs/Twist";
			}
			
			public Vector3Msg GetLinear() {
				return _linear;
			}

			public Vector3Msg GetAngular() {
				return _angular;
			}
			
			public override string ToString() {
				return "Twist [linear=" + _linear.ToString() + ",  angular=" + _angular.ToString() + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"linear\" : " + _linear.ToYAMLString() + ", \"angular\" : " + _angular.ToYAMLString() + "}";
			}
		}
	}
}