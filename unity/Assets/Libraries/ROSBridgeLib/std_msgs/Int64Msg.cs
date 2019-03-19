using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Int64Msg : ROSBridgeMsg {
			private long _data;
			
			public Int64Msg(JSONNode msg) {
				_data = long.Parse(msg["data"]);
			}
			
			public Int64Msg(long data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Int64";
			}
			
			public long GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Int64 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}