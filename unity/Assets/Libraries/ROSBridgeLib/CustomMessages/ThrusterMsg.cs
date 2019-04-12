using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

// custom thruster msg to store values for 4 depth thrusters + 4 vector thrusters

namespace ROSBridgeLib {
	namespace CustomMessages {
		public class ThrusterMsg : ROSBridgeMsg {
			private int _td1, _td2, _td3, _td4, _tfl, _tfr, _trl, _trr;

			public ThrusterMsg(JSONNode msg) {
				_td1 = int.Parse(msg["td1"]);
				_td2 = int.Parse (msg["td2"]);
				_td3 = int.Parse (msg["td3"]);
				_td4 = int.Parse (msg ["td4"]);
				_tfl = int.Parse(msg["tfl"]);
				_tfr = int.Parse (msg["tfr"]);
				_trl = int.Parse (msg["trl"]);
				_trr = int.Parse (msg ["trr"]);
			}

			public void ThrusterMessage(int td1, int td2, int td3, int td4, int tfl, int tfr, int trl, int trr) {
				_td1 = td1;
				_td2 = td2;
				_td3 = td3;
				_td4 = td4;
                _tfl = tfl;
                _tfr = tfr;
                _trl = trl;
                _trr = trr;
			}

			public static string getMessageType() {
				return "control/thruster";
			}

			public int Gettd1() {
				return _td1;
			}
			public int Gettd2() {
				return _td2;
			}
			public int Gettd3() {
				return _td3;
			}
			public int Gettd4() {
				return _td4;
			}
			public int Gettfl() {
				return _tfl;
			}
			public int Gettfr() {
				return _tfr;
			}
			public int Gettrl() {
				return _trl;
			}
			public int Gettrr() {
				return _trr;
			}

			public override string ToString() {
				return "control/thruster [td1=" + _td1 +
						", td2=" + _td2 + 
						", td3=" + _td3 +  
						", td4=" + _td4 +
						", tfl=" + _tfl + 
						", tfr=" + _tfr +  
						", trl=" + _trl +
						", trr=" + _trr +
						"]";
			}

			public override string ToYAMLString() {
				return "{\"td1\": " + _td1 + 
						", \"td2\": " + _td2 + 
						", \"td3\": " + _td3 + 
						", \"td4\": " + _td4 + 
						", \"tfl\": " + _tfl + 
						", \"tfr\": " + _tfr + 
						", \"trl\": " + _trl + 
						", \"trr\": " + _trr + 
						" }";
			}
		}
	}
}


