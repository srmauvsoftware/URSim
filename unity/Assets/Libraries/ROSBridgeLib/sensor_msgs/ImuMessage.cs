using System.Collections;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.geometry_msgs;
using UnityEngine;

namespace ROSBridgeLib {
	namespace sensor_msgs {
		public class ImuMessage : ROSBridgeMsg {
			private HeaderMsg _header;
			private double[] _orientation_c;  // C for Covariance
			private QuaternionMsg _quaternionIMU;
			private Vector3Msg _angularVelocity;
			private double[] _angular_velocity_c;
			private Vector3Msg _linearAcceleration;
			private double[] _linear_acceleration_c;


			public ImuMessage(JSONNode msg) {
				_header = new HeaderMsg (msg ["header"]);
				_quaternionIMU = new QuaternionMsg (msg ["orientation"]);
				/*
				for (int i = 0; i < 8; i++) {
					_orientation_c [i] = double.Parse (msg ["orientation_covariance"] [i]);
					_angular_velocity_c [i]= double.Parse (msg ["angular_velocity_covariance"][i]);
					_linear_acceleration_c [i]= double.Parse (msg ["linear_acceleration_covariance"][i]);
				}
				*/
				_angularVelocity = new Vector3Msg (msg ["angular_velocity"]);
				_linearAcceleration = new Vector3Msg (msg ["linear_acceleration"]);
				//Debug.Log ("IMU Message Done");
			}

			public ImuMessage(HeaderMsg header, QuaternionMsg quaternionIMU, double[] orientation_c, Vector3Msg angularVelocity, double[] angular_velocity_c, Vector3Msg linearAcceleration, double[] linear_acceleration_c) {
				_header = header;
				_quaternionIMU = quaternionIMU;
				_orientation_c = orientation_c;
				_angularVelocity = angularVelocity;
				_angular_velocity_c = angular_velocity_c;
				_linearAcceleration = linearAcceleration;
				_linear_acceleration_c = linear_acceleration_c;
			}
			/*	
			public ImuMessage(HeaderMsg header, QuaternionMsg quaternionIMU, Vector3Msg angularVelocity, Vector3Msg linearAcceleration) {
				_header = header;
				_quaternionIMU = quaternionIMU;
				_angularVelocity = angularVelocity;
				_linearAcceleration = linearAcceleration;
			}
			*/
				
			public static string GetMessageType() {
				return "sensor_msgs/Imu";
			}

			public HeaderMsg GetHeaderMsg(){
				return _header;
			}

			public QuaternionMsg GetQuaternionIMU(){
				return _quaternionIMU;
			}

			public double[] GetOrientationCovariance(){
				return _orientation_c;
			}

			public Vector3Msg GetAngularVelocity() {
				return _angularVelocity;
			}

			public double[] GetAngularVelocityCovariance(){
				return _angular_velocity_c;
			}

			public Vector3Msg GetlinearAcceleration() {
				return _linearAcceleration;
			}

			public double[] GetLinearAccelerationCovariance(){
				return _linear_acceleration_c;
			}

			public override string ToString() {
				return "sensor_msgs/Imu [header=" + _header.ToString() + ", orientation=" + _quaternionIMU.ToString() + ", orientation_covariance=" + _orientation_c + ", angular_velocity="+ _angularVelocity.ToString() + ", angular_velocity_covariance=" + _angular_velocity_c + ", linear_acceleration=" + _linearAcceleration.ToString() + ", linear_acceleration_covariance=" + _linear_acceleration_c + "]";
			}

			public override string ToYAMLString() {
				return "{\"header\" : " + _header.ToYAMLString() + ", \"orientation\" : " + _quaternionIMU.ToYAMLString() + ", \"orientation_covariance\" : " + _orientation_c + ", \"angular_velocity\" : " + _angularVelocity.ToYAMLString() + ", \"angular_velocity_covariance\" : " + _angular_velocity_c + " , \"linear_acceleration\" : " + _linearAcceleration.ToYAMLString() +" , \"linear_acceleration_covariance\" : " + _linear_acceleration_c + "}";
			}
			/*
			public override string ToString() {
				return "sensor_msgs/Imu [header=" + _header.ToString() + ", orientation=" + _quaternionIMU.ToString() + ", angular_velocity="+ _angularVelocity.ToString() + ", linear_acceleration=" + _linearAcceleration.ToString() + "]";
			}

			public override string ToYAMLString() {
				return "{\"header\" : " + _header.ToYAMLString() + ", \"orientation\" : " + _quaternionIMU.ToYAMLString() + ", \"angular_velocity\" : " + _angularVelocity.ToYAMLString() + ", \"linear_acceleration\" : " + _linearAcceleration.ToYAMLString() +"}";
			}
			*/

		}
	}
}