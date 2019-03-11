#!/usr/bin/env python
from control.msg import surgeFeedback, surgeAction, surgeResult
import rospy
import time
import actionlib

class Surge(object):
    feedback = surgeFeedback()
    result = surgeResult()

    def __init__(self, name):
        self.surgePub = rospy.Publisher('/surge_setpoint', Float64, queue_size=1)
        rospy.Subscriber("/surge", Float64, self.surgeCallback)
        self.serverName = name
        self.surgeServer = actionlib.SimpleActionServer(
                    self.serverName,
                    surgeAction,
                    execute_cb=self.surgeActionCallback,
                    auto_start=False)
        self.surgeServer.start()

    def surgeCallback(self, data):
        self.surge = data.data

    def surgeActionCallback(self, goal):
        success = False
        while(goal.surge_setpoint != self.surge):
            start = int(time.time())
            while(abs(goal.surge_setpoint - self.surge) < 3):
                if(int(time.time()) == start + 10):
                    success = True
                    break
            if(successt):
                break
            self.surgePub.publish(goal.surge_setpoint)
        rospy.loginfo('surge: %f, surge Setpoint: %f, Error: %f', \
                        self._surge, req.surge_setpoint, \
                        req.surge_setpoint-self.surge)
        if success:
            self.result.surge_final = self.surge
            rospy.loginfo('%s : Success' % self.serverName)
            self.surgeServer.set_succeeded(self.result)

if __name__ == '__main__':
    rospy.init_node('surgeServer')
    server = Surge(rospy.get_name())
    rospy.spin()
