import rospy
from smach import StateMachine
import smach
from smach_ros import SimpleActionState
import actionlib
from control.msg import *

class Surge(smach.State):
    def __init__(self, GOAL, TASK):
        self.GOAL = GOAL
        self.TASK = TASK
        smach.State.__init__(self, outcomes=[self.TASK])

    def execute(self, ud):
        rospy.loginfo("Execute State Surge")
        client = actionlib.SimpleActionClient('surgeServer', surgeAction)
        client.wait_for_server()
        goal = surgeGoal(surge_setpoint=self.GOAL)
        client.send_goal(goal)
        client.wait_for_result()
        result = client.get_state()
        if result == -1:
            return 'SurgeReached'
        elif result == 'preempted':
            return 'aborted'
        elif result == 'aborted':
            return 'aborted'
        else: return 'aborted'
