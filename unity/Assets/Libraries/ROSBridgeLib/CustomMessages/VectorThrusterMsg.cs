using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib {
	namespace CustomMessages {
		public class VectorThrusterMsg : ROSBridgeMsg {
			private int _tfr, _tfl, _trr, _trl;

			public VectorThrusterMsg(JSONNode msg) {
				_tfr = int.Parse(msg["tfr"]);
				_tfl = int.Parse (msg["tfl"]);
				_trr = int.Parse (msg["trr"]);
				_trl = int.Parse (msg ["trl"]);
				//Debug.Log ("Thruster Message Parsed");
			}

			public void VectorThrusterMessage(int tfr, int tfl, int trr, int trl) {
				_tfr = tfr;
				_tfl = tfl;
				_trr = trr;
				_trl = trl;
			}

			public static string getMessageType() {
				return "thrusters/VectorThrusterMsg";
			}

			public int Gettfr() {
				return _tfr;
			}
			public int Gettfl() {
				return _tfl;
			}
			public int Gettrr() {
				return _trr;
			}
			public int Gettrl() {
				return _trl;
			}

			public override string ToString() {
				return "thrusters/VectorThrusterMsg [tfr=" + _tfr +
						", tfl=" + _tfl + 
						", trr=" + _trr +  
						", trl=" + _trl +
						"]";
			}

			public override string ToYAMLString() {
				return "{\"tfr\": " + _tfr + 
						", \"tfl\": " + _tfl + 
						", \"trr\": " + _trr + 
						", \"trl\": " + _trl + 
						" }";
			}
		}
	}
}


