#!/usr/bin/env python
from control.srv import roll, rollResponse
import rospy
import time

class rollService():
    def __init__(self):
        rospy.init_node('roll_server')
        self.rollPub = rospy.Publisher('/roll_setpoint', Float64, queue_size=1)
        rospy.Subscriber("/roll", Float64, self.rollCallback)
        self.roll_service = rospy.Service('roll_service', roll, self.handleroll)
        rospy.spin()

    def rollCallback(self, data):
        self._roll = data.data

    def handleroll(self, req):
        self.rollPub.publish(Float64(req.roll_setpoint))
        rospy.loginfo('roll: %f, roll Setpoint: %f, Error: %f', \
                        self._roll, req.roll_setpoint, \
                        req.roll_setpoint-self.roll)
        return rollResponse(Bool(True))

if __name__ == "__main__":
    rollService()
