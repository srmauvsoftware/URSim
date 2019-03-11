#! /usr/bin/env python
import rospy
import smach

'''
Class which do not inherit smach.State are used to nest different
states together. Used to make hierarchies in states.
'''
class Sink:
    def __init__(self, sm, name, goalSurge, nextTask):
        self.goal = goalSurge
        self.name = name
        sm_sub = smach.StateMachine(outcomes=['surge_success', 'aborted'])
        with sm_sub:
            heaveTask = Heave(self.goal, 'heave_success')
            heaveTask.addDepthAction(sm)
        sm.add(self.name, sm_sub, transitions={'heave_success':TASK})
