#!/usr/bin/env python
from control.msg import yawFeedback, yawAction, yawResult
import rospy
import time
import actionlib

class Yaw(object):
    feedback = yawFeedback()
    result = yawResult()

    def __init__(self, name):
        self.yawPub = rospy.Publisher('/yaw_setpoint', Float64, queue_size=1)
        rospy.Subscriber("/yaw", Float64, self.yawCallback)
        self.serverName = name
        self.yawServer = actionlib.SimpleActionServer(
                    self.serverName,
                    yawAction,
                    execute_cb=self.yawActionCallback,
                    auto_start=False)
        self.yawServer.start()

    def yawCallback(self, data):
        self.yaw = data.data

    def yawActionCallback(self, goal):
        success = False
        while(goal.yaw_setpoint != self.yaw):
            start = int(time.time())
            while(abs(goal.yaw_setpoint - self.yaw) < 3):
                if(int(time.time()) == start + 10):
                    success = True
                    break
            if(successt):
                break
            self.yawPub.publish(goal.yaw_setpoint)
        rospy.loginfo('yaw: %f, yaw Setpoint: %f, Error: %f', \
                        self._yaw, req.yaw_setpoint, \
                        req.yaw_setpoint-self.yaw)
        if success:
            self.result.yaw_final = self.yaw
            rospy.loginfo('%s : Success' % self.serverName)
            self.yawServer.set_succeeded(self.result)

if __name__ == '__main__':
    rospy.init_node('yawServer')
    server = Yaw(rospy.get_name())
    rospy.spin()
