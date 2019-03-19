using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using ROSBridgeLib.CustomMessages;

public class VectorThrusterCallback : ROSBridgeSubscriber {

	public new static string GetMessageTopic() {
		return "/vectorThruster";
	}  

	public new static string GetMessageType() {
		return "thrusters/VectorThrusterMsg";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new VectorThrusterMsg(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {
    
    GameObject capsule = GameObject.Find ("Vehicle");
		VectorThrusterMsg vt = (VectorThrusterMsg) msg;

    if (capsule == null) {
      Debug.Log ("Cant Find AUV");
    } else {  
      int tfr = vt.Gettfr();
      int tfl = vt.Gettfl();
      int trr = vt.Gettrr();
      int trl = vt.Gettrl();

      Debug.Log("tfr value: " + tfr + " tfl value: " + tfl);

      float k = 0.005f;
      float headingConst = 0.1f;
      
      float force_tfr = (tfr - 290) * k;
      float force_tfl = (tfl - 290) * k;
      float force_trr = (trr - 290) * k;
      float force_trl = (trr - 290) * k;

      // move forward or backward

      // if (tfr == tfl && trr == trl  && tfr == trr) {
        if (tfr == trr) {
          capsule.GetComponent<Rigidbody>().AddForce(0,0,-1*force_tfr);
        }
      // }

      // sway left or right

      // if (tfr == -1 * tfl && trr == -1 * trl && tfr == trl) {
      //   capsule.GetComponent<Rigidbody>().AddForce(force_tfr,0,0);
      // }
      
      // //yaw left or right

      // if (trr == tfr && tfl == trl && tfr == -1 * tfl) {
      //   // rotating
      // }

    }
  }
}