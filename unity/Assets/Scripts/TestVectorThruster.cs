using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVectorThruster : MonoBehaviour {

  GameObject capsule;
  CapsuleCollider capsuleColl;
  Rigidbody rb;
  Vector3 pos;

	// Use this for initialization
	void Start () {
    capsule = GameObject.Find ("Capsule");
    capsuleColl = capsule.GetComponent<CapsuleCollider>();
    rb = capsule.GetComponent<Rigidbody>();
    pos = capsule.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
    float radius = capsuleColl.radius;
    float height = capsuleColl.height;
    
    Vector3 p1 = new Vector3(pos.x + radius, pos.y, pos.z - height/2);
    Vector3 p2 = new Vector3(pos.x - radius, pos.y, pos.z - height/2);
    Vector3 p3 = new Vector3(pos.x + radius, pos.y, pos.z + height/2);
    Vector3 p4 = new Vector3(pos.x - radius, pos.y, pos.z + height/2);


    float tfl = 100;
    float tfr = 100;
    float trl = 100;
    float trr = 100;


		rb.AddForceAtPosition(new Vector3(-tfl/2, 0, -tfl/2), p1);
    rb.AddForceAtPosition(new Vector3( tfr/2, 0, -tfr/2), p2);
    rb.AddForceAtPosition(new Vector3( trl/2, 0, -trl/2), p3);
    rb.AddForceAtPosition(new Vector3(-trr/2, 0, -trr/2), p4);
	}
}
