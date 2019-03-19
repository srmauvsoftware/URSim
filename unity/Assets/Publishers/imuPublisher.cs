using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class imuPublisher : ROSBridgePublisher {

	public static string GetMessageTopic(){
		return "/imu/data";
	}
	public static string GetMessageType(){
		return "sensor_msgs/Imu";
	}
    public static string ToYAMLString(ImuMessage msg){
		return msg.ToYAMLString ();
	}
}