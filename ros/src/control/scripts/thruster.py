#!/usr/bin/env python
import rospy
from std_msgs.msg import Float64
from control.msg import thrusterMsg
from dynamic_reconfigure.server import Server
from control.cfg import thrusterConfig

class Thruster:
    def __init__(self):
        self.thrusterPub = rospy.Publisher('/thruster', thruster, queue_size = 1)
        rospy.Rate(10)
        self.roll = 0
        self.pitch = 0
        self.yaw = 0
        self.surge = 0
        self.sway = 0
        self.heave = 0
        rospy.Subscriber('/roll_controller/control_effort', Float64, self.rollCallback)
        rospy.Subscriber('/pitch_controller/control_effort', Float64, self.pitchCallback)
        rospy.Subscriber('/yaw_controller/control_effort', Float64, self.yawCallback)
        rospy.Subscriber('/surge_controller/control_effort', Float64, self.surgeCallback)
        rospy.Subscriber('/sway_controller/control_effort', Float64, self.swayCallback)
        rospy.Subscriber('/heave_controller/control_effort', Float64, self.heaveCallback)
        srv = Server(thrusterConfig, self.dynamicThrusterCb)

    def rollCallback(self, data):
        self.roll = data.data

    def pitchCallback(self, data):
        self.pitch = data.data

    def yawCallback(self, data):
        self.yaw = data.data

    def surgeCallback(self, data):
        self.surge = data.data

    def swayCallback(self, data):
        self.sway = data.data

    def heaveCallback(self, data):
        self.heave = data.data

    def thrusterCb(self):
        msg = thrusterMsg()
        msg.td1 = 290 + self.roll + self.pitch + self.heave
        msg.td2 = 290 + self.roll + self.pitch + self.heave
        msg.td3 = 290 + self.roll + self.pitch + self.heave
        msg.td4 = 290 + self.roll + self.pitch + self.heave
        msg.tfr = 290 + (-self.surge +self.sway +self.yaw)
        msg.tfl = 290 + (-self.surge -self.sway -self.yaw)
        msg.trr = 290 + ( self.surge -self.sway +self.yaw)
        msg.trl = 290 + ( self.surge +self.sway -self.yaw)
        self.thrusterPub.publish(msg)

    def dynamicThrusterCb(self, config, level):
        msg = thrusterMsg()
        msg.td1 = config.td1
        msg.td2 = config.td2
        msg.td3 = config.td3
        msg.td4 = config.td4
        msg.tfr = config.tfr
        msg.tfl = config.tfl
        msg.trr = config.trr
        msg.trl = config.trl
        self.thrusterPub.publish(msg)
        return config

    def initAndShutdown(self):
        msg = thrusterMsg()
        msg.td1 = 290
        msg.td2 = 290
        msg.td3 = 290
        msg.td4 = 290
        msg.tfr = 290
        msg.tfl = 290
        msg.trr = 290
        msg.trl = 290
        self.thrusterPub.publish(msg)

if __name__ == '__main__':
    try:
        rospy.init_node('thruster')
        thruster = Thruster()
        thruster.initAndShutdown()
        while not rospy.is_shutdown():
            thruster.thrusterCb()
            rospy.sleep(1)
        rospy.on_shutdown(thruster.initAndShutdown)

    except rospy.ROSInterruptException: pass
