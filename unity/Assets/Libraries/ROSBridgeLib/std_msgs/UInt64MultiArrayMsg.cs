using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class UInt64MultiArrayMsg : ROSBridgeMsg {
			private MultiArrayLayoutMsg _layout;
            private ulong[] _data;
			
			public UInt64MultiArrayMsg(JSONNode msg) {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new ulong[msg["data"].Count];
                for (int i = 0; i < _data.Length; i++) {
					_data[i] = ulong.Parse(msg["data"][i]);
                }
			}
			
			public UInt64MultiArrayMsg(MultiArrayLayoutMsg layout, ulong[] data) {
                _layout = layout;
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/UInt64MultiArray";
			}

			public ulong[] GetData() {
				return _data;
			}

            public MultiArrayLayoutMsg GetLayout() {
                return _layout;
            }

			public override string ToString() {
                string array = "[";
                for (int i = 0; i < _data.Length; i++) {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }
                array += "]";
				return "UInt64MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
			}

			public override string ToYAMLString() {
                string array = "[";
                for (int i = 0; i < _data.Length; i++) {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }
                array += "]";

				return "{\"layout\" : " + _layout.ToYAMLString() + ", \"data\" : " + array + "}";
			}
		}
	}
}