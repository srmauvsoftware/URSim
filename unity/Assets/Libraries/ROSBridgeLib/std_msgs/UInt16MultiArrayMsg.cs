using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
        public class UInt16MultiArrayMsg : ROSBridgeMsg {
            private MultiArrayLayoutMsg _layout;
            private ushort[] _data;

            public UInt16MultiArrayMsg(JSONNode msg) {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new ushort[msg["data"].Count];
				for (int i = 0; i < _data.Length; i++) {
                    _data[i] = ushort.Parse(msg["data"][i]);
                }
            }

            public UInt16MultiArrayMsg(MultiArrayLayoutMsg layout, ushort[] data) {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType() {
                return "std_msgs/UInt16MultiArray";
            }

            public ushort[] GetData() {
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
                return "UInt16MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
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