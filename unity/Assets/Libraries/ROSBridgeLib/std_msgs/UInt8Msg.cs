using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class UInt8Msg : ROSBridgeMsg {
			private byte _data;
			
			public UInt8Msg(JSONNode msg) {
				_data = byte.Parse(msg["data"]);
			}
			
			public UInt8Msg(byte data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/UInt8";
			}
			
			public byte GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "UInt8 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}