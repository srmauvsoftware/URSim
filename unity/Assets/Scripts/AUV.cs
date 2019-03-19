using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.CustomMessages;
using ROSBridgeLib;
using System.Text;
using System.IO;


public class AUV : MonoBehaviour {
	private ROSBridgeWebSocketConnection ros = null;
	int count;
	DateTime lastFrame;
	DateTime camStart;
	public GameObject vehicle;
	
    void Start(){
        ros = new ROSBridgeWebSocketConnection ("ws://192.168.0.100", 9090);
        ros.AddPublisher (typeof(CompressedImagePublisher));
        ros.AddPublisher(typeof(DepthPublisher));
        // ros.AddPublisher(typeof(imuPublisher));
        ros.AddPublisher(typeof(HeadingPublisher));
        ros.AddSubscriber(typeof(DepthThrusterCallback));		
        ros.AddSubscriber(typeof(VectorThrusterCallback));		
        ros.AddSubscriber(typeof(ImuSubscriberCallback));
        ros.Connect ();
        count = 0;
        lastFrame = DateTime.Now;
        camStart = DateTime.Now;
        
    }


  void Update()
  {
        StartCoroutine(SendImage());
        StartCoroutine(SendDepth());
        // StartCoroutine(SendImu());
        StartCoroutine(SendHeading());

  }

  void OnApplicationQuit() {
		if(ros!=null)
			ros.Disconnect ();
	}

	IEnumerator SendImage () {
		yield return new WaitForEndOfFrame ();
		int width = 640;
		int height = 480;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] data = tex.EncodeToJPG();

		var now = DateTime.Now;
		var timeSpan = now - lastFrame;
		var timeSinceStart = now - camStart;
		var timeMessage = new TimeMsg(timeSinceStart.Seconds, timeSinceStart.Milliseconds);
		var headerMessage = new HeaderMsg(count, timeMessage, "camera");

		Debug.Log ("data length: " + data.Length);
		string format = "jpeg";
		var compressedImageMsg = new CompressedImageMsg(headerMessage, format, data);
        Debug.Log(compressedImageMsg);
		ros.Publish(CompressedImagePublisher.GetMessageTopic(), compressedImageMsg);
		Destroy(tex);
		ros.Render ();
	}

    IEnumerator SendDepth(){
        yield return new WaitForEndOfFrame();
        float depth = vehicle.transform.position.y; 
        // Debug.Log(depth);
        var depthMessage = new Float64Msg(depth);
        ros.Publish(DepthPublisher.GetMessageTopic(), depthMessage);
        ros.Render();
    }

    IEnumerator SendImu()
    {
        yield return new WaitForEndOfFrame();

        var now = DateTime.Now;
        var timeSpan = now - lastFrame;
        var timeSinceStart = now - camStart;
        var timeMessage = new TimeMsg(timeSinceStart.Seconds, timeSinceStart.Milliseconds);
        var headerMessage = new HeaderMsg(count, timeMessage, "imu");

        Quaternion vehicleImu;
        vehicleImu = vehicle.transform.rotation;
        double xey = vehicleImu.x;
        double yey = vehicleImu.y;
        double zey = vehicleImu.z;
        double wey = vehicleImu.w;
        var imuData = new QuaternionMsg(xey, yey, zey, wey);

        double[] arg3 = { 0.00, 0.00, 0.00 };

        var vect3 = new Vector3Msg(0.00, 0.00, 0.00);

        var imumessage = new ImuMessage(headerMessage, imuData,arg3,vect3,arg3,vect3,arg3); 	
        ros.Publish(imuPublisher.GetMessageTopic(), imumessage);
        ros.Render();
	}

    IEnumerator SendHeading(){
        yield return new WaitForEndOfFrame();

        // float x = 0.00F;
        // float y = 0.00F;
        float theta = vehicle.transform.rotation.eulerAngles.y;
        var posemsg = new Float64Msg(theta);
        ros.Publish(HeadingPublisher.GetMessageTopic(), posemsg);
        ros.Render();
           
    }

}
     