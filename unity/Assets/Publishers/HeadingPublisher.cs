using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class HeadingPublisher : ROSBridgePublisher
{

    public static string GetMessageTopic()
    {
        return "/imu/HeadingTrue_degree";
    }
    public static string GetMessageType()
    {
        return "std_msgs/Float64";
    }
    public static string ToYAMLString(Float64Msg msg)
    {
        return msg.ToYAMLString();
    }
}