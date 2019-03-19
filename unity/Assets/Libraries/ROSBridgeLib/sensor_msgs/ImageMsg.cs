using SimpleJSON;
using ROSBridgeLib.std_msgs;
using UnityEngine;

/**
 * Define a Image message.
 *  
 * @author Mathias Ciarlo Thorstensen
 */

namespace ROSBridgeLib {
    namespace sensor_msgs {
        public class ImageMsg : ROSBridgeMsg {
            private HeaderMsg _header;
            private uint _height;
            private uint _width;
            private string _encoding;
            private bool _is_bigendian;
            private uint _row_step;
            private byte[] _data;

            public ImageMsg(JSONNode msg) {
                _header = new HeaderMsg (msg ["header"]);
                _height = uint.Parse(msg ["height"]);
                _width = uint.Parse(msg ["width"]);
                _encoding = msg ["encoding"];
                _is_bigendian = msg["is_bigendian"].AsBool;
                _row_step = uint.Parse(msg ["step"]);
                _data = System.Convert.FromBase64String(msg["data"]);
            }

            public ImageMsg(HeaderMsg header, uint height, uint width, string encoding, bool is_bigendian, uint row_step, byte[] data) {
                _header = header;
                _height = height;
                _width = width;
                _encoding = encoding;
                _is_bigendian = is_bigendian;
                _row_step = row_step;
            }

            public HeaderMsg GetHeader()
            {
                return _header;
            }

            public uint GetWidth() {
                return _width;
            }

            public uint GetHeight() {
                return _height;
            }
                
            public uint GetRowStep() {
                return _row_step;
            }

            public byte[] GetImage() {
                return _data;
            }

            public static string GetMessageType() {
                return "sensor_msgs/Image";
            }

            public override string ToString() {
                return "Image [header=" + _header.ToString() +
                    "height=" + _height +
                    "width=" + _width +
                    "encoding=" + _encoding +
                    "is_bigendian=" + _is_bigendian +
                    "row_step=" + _row_step + "]";
            }

            public override string ToYAMLString() {
                return "{\"header\" :" + _header.ToYAMLString() +
                    "\"height\" :" + _height +
                    "\"width\" :" + _width +
                    "\"encoding\" :" + _encoding +
                    "\"is_bigendian\" :" + _is_bigendian +
                    "\"row_step\" :" + _row_step + "}";
            }
        }
    }
}
