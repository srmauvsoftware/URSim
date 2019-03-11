#!/usr/bin/env python
from control.msg import swayFeedback, swayAction, swayResult
import rospy
import time
import actionlib

class Sway(object):
    feedback = swayFeedback()
    result = swayResult()

    def __init__(self, name):
        self.swayPub = rospy.Publisher('/sway_setpoint', Float64, queue_size=1)
        rospy.Subscriber("/sway", Float64, self.swayCallback)
        self.serverName = name
        self.swayServer = actionlib.SimpleActionServer(
                    self.serverName,
                    swayAction,
                    execute_cb=self.swayActionCallback,
                    auto_start=False)
        self.swayServer.start()

    def swayCallback(self, data):
        self.sway = data.data

    def swayActionCallback(self, goal):
        success = False
        while(goal.sway_setpoint != self.sway):
            start = int(time.time())
            while(abs(goal.sway_setpoint - self.sway) < 3):
                if(int(time.time()) == start + 10):
                    success = True
                    break
            if(successt):
                break
            self.swayPub.publish(goal.sway_setpoint)
        rospy.loginfo('sway: %f, sway Setpoint: %f, Error: %f', \
                        self._sway, req.sway_setpoint, \
                        req.sway_setpoint-self.sway)
        if success:
            self.result.sway_final = self.sway
            rospy.loginfo('%s : Success' % self.serverName)
            self.swayServer.set_succeeded(self.result)

if __name__ == '__main__':
    rospy.init_node('swayServer')
    server = Sway(rospy.get_name())
    rospy.spin()
