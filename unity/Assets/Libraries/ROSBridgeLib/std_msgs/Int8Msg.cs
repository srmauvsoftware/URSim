using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Int8Msg : ROSBridgeMsg {
			private sbyte _data;
			
			public Int8Msg(JSONNode msg) {
				_data = sbyte.Parse(msg["data"]);
			}
			
			public Int8Msg(sbyte data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Int8";
			}
			
			public sbyte GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Int8 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}