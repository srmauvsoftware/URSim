using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
        public class UInt8MultiArrayMsg : ROSBridgeMsg {
            private MultiArrayLayoutMsg _layout;
            private byte[] _data;

            public UInt8MultiArrayMsg(JSONNode msg) {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new byte[msg["data"].Count];
				for (int i = 0; i < _data.Length; i++) {
                    _data[i] = byte.Parse(msg["data"][i]);
                }
            }

            public UInt8MultiArrayMsg(MultiArrayLayoutMsg layout, byte[] data) {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType() {
                return "std_msgs/UInt8MultiArray";
            }

            public byte[] GetData() {
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
                return "UInt8MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
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