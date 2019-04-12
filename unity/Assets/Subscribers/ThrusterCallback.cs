using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using ROSBridgeLib.CustomMessages;

// tfl ------- tfr
//  |           |
//  |           |
//  |           |
//  |           |
//  |           |
// trl ------- trr

public class ThrusterCallback : ROSBridgeSubscriber {

    public static Vector3 p1, p2, p3, p4, pfl, pfr, prl, prr;

	public new static string GetMessageTopic() {
		return "/thruster";
	}  

	public new static string GetMessageType() {
		return "thrusters/ThrusterMsg";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new ThrusterMsg(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {

    GameObject capsule = GameObject.Find ("Capsule");
    ThrusterMsg t = (ThrusterMsg) msg;
    CapsuleCollider capsuleColl = capsule.GetComponent<CapsuleCollider>();
    Rigidbody rb = capsule.GetComponent<Rigidbody>();

    float radius = capsuleColl.radius;
    float height = capsuleColl.height;
    Vector3 pos = capsule.transform.position;

    // thruster position calculation
    p1 = new Vector3(pos.x + radius, pos.y, pos.z - height/2);
    p2 = new Vector3(pos.x - radius, pos.y, pos.z - height/2);
    p3 = new Vector3(pos.x + radius, pos.y, pos.z + height/2);
    p4 = new Vector3(pos.x - radius, pos.y, pos.z + height/2);

    if (capsule == null) {
        Debug.Log ("Cant Find AUV");
    } else {
        float td1 = t.Gettd1() - 290;
        float td2 = t.Gettd2() - 290;
        float td3 = t.Gettd3() - 290;
        float td4 = t.Gettd4() - 290;
        float tfl = t.Gettfl() - 290;
        float tfr = t.Gettfr() - 290;
        float trl = t.Gettrl() - 290;
        float trr = t.Gettrr() - 290;

        rb = capsule.GetComponent<Rigidbody>();

        // depth thruster forces
        rb.AddForceAtPosition(new Vector3(0, td1, 0), p1);
        rb.AddForceAtPosition(new Vector3(0, td2, 0), p2);
        rb.AddForceAtPosition(new Vector3(0, td3, 0), p3);
        rb.AddForceAtPosition(new Vector3(0, td4, 0), p4);

        // vector thruster forces
        rb.AddForceAtPosition(new Vector3(-tfl/2, 0, -tfl/2), p1);
        rb.AddForceAtPosition(new Vector3( tfr/2, 0, -tfr/2), p2);
        rb.AddForceAtPosition(new Vector3( trl/2, 0, -trl/2), p3);
        rb.AddForceAtPosition(new Vector3(-trr/2, 0, -trr/2), p4);
        


        // v1 = new Vector3();



    //   capsule.GetComponent<Rigidbody>().AddForce(0,zforce,0);
    }
  }
}