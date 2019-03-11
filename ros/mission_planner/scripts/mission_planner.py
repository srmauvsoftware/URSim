#! /usr/bin/env python
import rospy
from smach import StateMachine
import smach
from smach_ros import IntrospectionServer
from Heave import Heave
from Roll import Roll
from Surge import Surge
from Sway import Sway
from Yaw import Yaw
from Pitch import Pitch

def main():
    sm = smach.StateMachine(outcomes=['mission_complete', 'mission_failed', 'aborted'])
    with sm:
        Sink(sm, 'HEAVE1', 500, 'mission_complete')
