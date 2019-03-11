import rospy
from smach import StateMachine
import smach
from smach_ros import SimpleActionState
import actionlib
from control.msg import *

class Heave(smach.State):
    def __init__(self, GOAL, TASK):
        self.GOAL = GOAL
        self.TASK = TASK
        smach.State.__init__(self, outcomes=[self.TASK])

    def addHeaveAction(self, sm):
        sm.add('DEPTH',
                SimpleActionState('heaveServer',
                                    heaveAction,
                                    goal_cb=self.heaveCallback),
                                    transitions={'succeeded':self.TASK,
                                                'preempted':'DEPTH',
                                                'aborted':'aborted'}

    def heaveCallback(self, ud, goal):
        rospy.loginfo('Executing State Concurrent Depth')
        heaveOrder = heaveGoal()
        heaveOrder.heave_setpoint = self.GOAL
        return heaveOrder

    def execute(self, ud):
        rospy.loginfo("Execute State heave")
        client = actionlib.SimpleActionClient('heaveServer', heaveAction)
        client.wait_for_server()
        goal = heaveGoal(heave_setpoint=self.GOAL)
        client.send_goal(goal)
        client.wait_for_result()
        result = client.get_state()
        if result == -1:
            return 'heaveReached'
        elif result == 'preempted':
            return 'aborted'
        elif result == 'aborted':
            return 'aborted'
        else: return 'aborted'
