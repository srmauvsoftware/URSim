using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Int32Msg : ROSBridgeMsg {
			private int _data;
			
			public Int32Msg(JSONNode msg) {
				_data = msg["data"].AsInt;
			}
			
			public Int32Msg(int data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Int32";
			}
			
			public int GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Int32 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}