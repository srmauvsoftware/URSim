using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class CompressedImagePublisher : ROSBridgePublisher {

	public static string GetMessageTopic(){
		return "/image/compressed";
	}
	public static string GetMessageType(){
		return "sensor_msgs/CompressedImage";
	}
	public static string ToYAMLString(CompressedImageMsg msg){
		return msg.ToYAMLString ();
	}
}