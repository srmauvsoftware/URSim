using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardMove : MonoBehaviour {

	public float mSpeed;
    public GameObject vehicle;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		mSpeed = 5f;
		// rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveHorizontal2 = Input.GetAxis ("Horizontalmove");
        float moveVertical = Input.GetAxis ("Vertical");
        float yaw = Input.GetAxis ("MouseY");

		Vector3 movement = new Vector3 (moveHorizontal, moveHorizontal2, moveVertical);
        rb.AddForce (movement * 2500f);
		rb.AddTorque(transform.up * 500 * yaw);
	}
	
	// Update is called once per frame
	// void Update () {
	// 	transform.Translate (mSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime * -1, -1 * mSpeed * Input.GetAxis ("Horizontalmove") * Time.deltaTime, mSpeed * Input.GetAxis ("Vertical") * -1 * Time.deltaTime);
	// }
}
