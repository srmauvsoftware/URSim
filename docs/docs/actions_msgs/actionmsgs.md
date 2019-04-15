---
layout: default
title: Actions and Messages

nav_order: 6

has_children: false
permalink: docs/action_msgs
---

# **Actions and Messages**

## **Messages**

ROS uses a simplified messages description language for describing the data values (aka messages) that ROS nodes publish. This description makes it easy for ROS tools to automatically generate source code for the message type in several target languages. Message descriptions are stored in .msg files in the msg/ subdirectory of a ROS package.

There are two parts to a .msg file: fields and constants. Fields are the data that is sent inside of the message. Constants define useful values that can be used to interpret those fields (e.g. enum-like constants for an integer value).

Each field consists of a type and a name, separated by a space, i.e.:

`fieldtype1 fieldname1`<br>
`fieldtype2 fieldname2`<br>
`fieldtype3 fieldname3`<br>

For example:

`int32 x`<br>
`int32 y`


There are 2 msg files each used by one of the thruster nodes:
1.  DepthThrusterMsg.msg
	Contains 4 variables - td1, td2, td3, td4 of type uint16.
	Each of the variables store the pwm values for the 4 depth thrusters.
2.  VectorThrusterMsg.msg
	Contains 4 variables - tfr, tfl, trr, trl of type uint16 where:<br>
		fr: front right<br>
		fl: front left<br>
		rr: rear right<br>
		rl: rear left<br>
	Each of the variables store the pwm values for the 4 heading thrusters.

## **Actions**

In order for the client and server to communicate, we need to define a few messages on which they communicate. This is with an action specification. This defines the Goal, Feedback, and Result messages with which clients and servers communicate:

### Goal
To accomplish tasks using actions, we introduce the notion of a goal that can be sent to an ActionServer by an ActionClient. In the case of moving the base, the goal would be a PoseStamped message that contains information about where the robot should move to in the world. For controlling the tilting laser scanner, the goal would contain the scan parameters (min angle, max angle, speed, etc).

### Feedback
Feedback provides server implementers a way to tell an ActionClient about the incremental progress of a goal. For moving the base, this might be the robot's current pose along the path. For controlling the tilting laser scanner, this might be the time left until the scan completes.

### Result
A result is sent from the ActionServer to the ActionClient upon completion of the goal. This is different than feedback, since it is sent exactly once. This is extremely useful when the purpose of the action is to provide some sort of information. For move base, the result isn't very important, but it might contain the final pose of the robot. For controlling the tilting laser scanner, the result might contain a point cloud generated from the requested scan.


### .action file
The action specification is defined using a .action file. The .action file has the goal definition, followed by the result definition, followed by the feedback definition, with each section separated by 3 hyphens (---).

These files are placed in a package's ./action directory, and look extremely similar to a service's .srv file. An action specification for doing the dishes might look like the following:

./action/DoDishes.action

`# Define the goal                                              `<br>
`uint32 dishwasher_id  # Specify which dishwasher we want to use`<br>
`---                                                            `<br>
`# Define the result                                            `<br>
`uint32 total_dishes_cleaned                                    `<br>
`---                                                            `<br>
`# Define a feedback message                                    `<br>
`float32 percent_complete                                       `<br>

There are 3 .action files used:<br>
1. 	dept.action<br>
	Has 3 fields - depth_setpoint, depth_final, depth_destination 
2. 	heading.action<br>
	Has 3 fields - heading_setpoint, heading_final, heading_destination 
3.  time.action<br>
	Has 3 fields - time_setpoint, time_final, time_destination 
