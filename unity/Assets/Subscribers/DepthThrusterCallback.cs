using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using ROSBridgeLib.CustomMessages;

public class DepthThrusterCallback : ROSBridgeSubscriber {

	public new static string GetMessageTopic() {
		return "/depthThruster";
	}  

	public new static string GetMessageType() {
		return "thrusters/DepthThrusterMsg";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new DepthThrusterMsg(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {

    GameObject capsule = GameObject.Find ("Vehicle");
		DepthThrusterMsg dt = (DepthThrusterMsg) msg;
    
    if (capsule == null) {
      Debug.Log ("Cant Find AUV");
    } else {
      int td1 = dt.Gettd1();
      int td2 = dt.Gettd2();
      int td3 = dt.Gettd3();
      int td4 = dt.Gettd4();

      Debug.Log("td1 value: " + td1 + " td2 value: " + td2);
      float k = 0.005f;

      int diff = td1 - 290;
      float zforce = diff * k;

      capsule.GetComponent<Rigidbody>().AddForce(0,zforce,0);
    }
  }
}