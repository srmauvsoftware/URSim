#!/usr/bin/env python
from control.msg import heaveFeedback, heaveAction, heaveResult
import rospy
import time
import actionlib

class Heave(object):
    feedback = heaveFeedback()
    result = heaveResult()

    def __init__(self, name):
        self.heavePub = rospy.Publisher('/heave_setpoint', Float64, queue_size=1)
        rospy.Subscriber("/heave", Float64, self.heaveCallback)
        self.serverName = name
        self.heaveServer = actionlib.SimpleActionServer(
                    self.serverName,
                    heaveAction,
                    execute_cb=self.heaveActionCallback,
                    auto_start=False)
        self.heaveServer.start()

    def heaveCallback(self, data):
        self.heave = data.data

    def heaveActionCallback(self, goal):
        success = False
        while(goal.heave_setpoint != self.heave):
            start = int(time.time())
            while(abs(goal.heave_setpoint - self.heave) < 3):
                if(int(time.time()) == start + 10):
                    success = True
                    break
            if(successt):
                break
            self.heavePub.publish(goal.heave_setpoint)
        rospy.loginfo('heave: %f, heave Setpoint: %f, Error: %f', \
                        self._heave, req.heave_setpoint, \
                        req.heave_setpoint-self.heave)
        if success:
            self.result.heave_final = self.heave
            rospy.loginfo('%s : Success' % self.serverName)
            self.heaveServer.set_succeeded(self.result)

if __name__ == '__main__':
    rospy.init_node('heaveServer')
    server = Heave(rospy.get_name())
    rospy.spin()
