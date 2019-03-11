import rospy
import actionlib
from control.msg import *

def generateClient(server, action, goal_order):
    client = actionlib.SimpleActionClient(server, action)
    client.wait_for_server()
    goal = SurgeGoal(order=goal_order)
    client.send_goal(goal)
    client.wait_for_result()
    result = client.get_result()
    return result

if __name__ == '__main__':
    try:
        rospy.init_node('control_clients')
        surgeResult = generateClient('surgeServer', surgeAction, 10)
        swayResult = generateClient('swayServer', swayAction, 10)
        heaveResult = generateClient('heaveServer', heaveAction, 10)
        rollResult = generateClient('rollServer', rollAction, 10)
        pitchResult = generateClient('pitchServer', pitchAction, 10)
        yawResult = generateClient('yawServer', yawAction, 10)
