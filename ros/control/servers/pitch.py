#!/usr/bin/env python
from control.msg import pitchFeedback, pitchAction, pitchResult
import rospy
import time
import actionlib

class Pitch(object):
    feedback = pitchFeedback()
    result = pitchResult()

    def __init__(self, name):
        self.pitchPub = rospy.Publisher('/pitch_setpoint', Float64, queue_size=1)
        rospy.Subscriber("/pitch", Float64, self.pitchCallback)
        self.serverName = name
        self.pitchServer = actionlib.SimpleActionServer(
                    self.serverName,
                    pitchAction,
                    execute_cb=self.pitchActionCallback,
                    auto_start=False)
        self.pitchServer.start()

    def pitchCallback(self, data):
        self.pitch = data.data

    def pitchActionCallback(self, goal):
        success = False
        while(goal.pitch_setpoint != self.pitch):
            start = int(time.time())
            while(abs(goal.pitch_setpoint - self.pitch) < 3):
                if(int(time.time()) == start + 10):
                    success = True
                    break
            if(successt):
                break
            self.pitchPub.publish(goal.pitch_setpoint)
        rospy.loginfo('pitch: %f, pitch Setpoint: %f, Error: %f', \
                        self._pitch, req.pitch_setpoint, \
                        req.pitch_setpoint-self.pitch)
        if success:
            self.result.pitch_final = self.pitch
            rospy.loginfo('%s : Success' % self.serverName)
            self.pitchServer.set_succeeded(self.result)

if __name__ == '__main__':
    rospy.init_node('pitchServer')
    server = Pitch(rospy.get_name())
    rospy.spin()
