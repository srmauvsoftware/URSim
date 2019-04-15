---
layout: default
title: Guided

nav_order: 3

has_children: true
permalink: docs/user/
---

# **Guided** 

## **Introduction**
Welcome to step by step tutorials for the users with little or no experience using URSim. The steps will guide you through the most basic usage of URSim. We will run a simple prebuilt scene in Unity and connect it to ROS.
You will find the guidelines to so the following tasks ahead:<br> 
1. Downloading and installing ROS, Unity and other required packages.    
2. Setting up URSim on windows and connecting it to ROS running on Ubuntu.    
3. Running your first simulation on URSim. 

## **Downloading and installing ROS, Unity**

1. **Ubuntu 16.04LTS**    
   Instructions for installing Ubuntu 16.04LTS can be found [here](https://tutorials.ubuntu.com/tutorial/tutorial-install-ubuntu-desktop-1604#0). Please stick to Ubuntu 16.Xs only.

2. **ROS**     
   Instructions for installing ROS Kinetic Kame can be found [here](http://wiki.ros.org/kinetic/Installation). Please stick to ROS Kinetic Kame only.

3. **Unity**    
   You can download Unity3D from [here](https://store.unity.com/download) and install it by running the executable file downloaded.

## **Setting up the repository and URSim** 
**Setting up repository**
1. In your Ubuntu terminal enter the following command to git clone the [URSim repository](https://github.com/srmauvsoftware/URSim):<br>`git clone https://github.com/srmauvsoftware/URSim.git`     
2. Source your environnment:<br>`source /opt/ros/kinetic/setup.bash`<br>Create a new catkin workspace:<br>`mkdir -p ~/catkin_ws/src`<br>`cd ~/catkin_ws/`<br>`catkin_make`
3. Copy the contents of *ros* directory to the new workspace:<br>`cd /path/to/repo/ros ~/catkin_ws/src`       
4. Install rosbridge:<br>`sudo apt-get install ros-kinetic-rosbridge-suite`          
5. Port forwarding:<br>Open your web browser and enter the url for opening the configuration page of your router somewhat similar to the examples below:<br>`TP Link Routers: http://192.168.0.1`<br>`Asus Routers: http://192.168.1.1`   
<br>
Find the option for port forwarding. It could be in security settings or advanced setting. 
Add a new port and ip address to forward and save the settings then restart the router. 
          

**Setting up URSim**    
1. On your Windows machine, download the repository from [here](https://github.com/srmauvsoftware/URSim). Extract the downloaded zip file.    
2. Navigate to /path/to/repo/URSim-master/unity/Assets/Scripts and open  the file `AUV.cs`.
3. Edit the ip address and the forwarded port of your ubuntu machine in the following line in the file:  
`ros = new ROSBridgeWebSocketConnection ("<ip address>", <port>);`
for example:     
`ros = new ROSBridgeWebSocketConnection ("ws://192.168.0.100", 9090);`

## **Runnning simulations and using ROS launch files**

1. **Running the simulation**
	1. Launch Unity3D and after signing in/skipping, click on 'open' icon at the top right of the window.
	2. Navigate to  `/path/to/repo/URSim-master/unity/` to select the project.
    3. When the project opens, click on `file->open scene` and navigate to `\pathtorepo\URSim-master\unity\Assets\Scenes` and select the scene to run. 

2. **Using ROS launch files** 
	Launch files in ROS are very common to both users and developers. They provide a convenient way to start up multiple nodes and a master, as well as other initialization requirements such as setting up parameters.
	The launch files are written in `xml` 
	**roslaunch** is used to open launch files. This is done by specifying the package the launch files are contained in followed by the name of the launch file. 
	We have placed all the launch files in `URSim/ros/src/mission_planner/launch/`. 
	You can run these launch files as follows:
	`roslaunch launch <launch_file_name>`. Eg. `roslaunch launch depth_controller.launch`. Make sure you have sourced the repository before running any commands.

	We provide you with the following launch files:

	**Launch files for controllers**

	1. `heave_controller.launch`<br>
		launches the ‘controller’ node from package ‘pid’ under the namespace ‘heave_controller’.<br>
		Sets values to the following parameters - 
		kp, ki, kd <br>
		upper_limit, lower_limit, windup_limit <br>
		max_loop_frequency, min_loop_frequency<br>
		Remaps <br>‘setpoint’ to ‘/depth_setpoint’ <br>
		 ‘state’ to  ‘/depth’<br>
		 ‘control_effort’ to ‘/heave_controller/control_effort’<br><br>
	2. `yaw_controller.launch`<br>
		launches the ‘controller’ node from package ‘pid’ under the namespace ‘yaw_controller’.<br>
		Sets values to the following parameters - 
		kp, ki, kd <br>
		upper_limit, lower_limit, windup_limit <br>
		max_loop_frequency, min_loop_frequency<br>
		Remaps <br>‘setpoint’ to ‘/heading_setpoint’ <br>
		 ‘state’ to  ‘/imu/HeadingTrue_degree/theta’<br>
		 ‘control_effort’ to ‘/yaw_controller/control_effort’<br><br>
	3. `sway_controller.launch`<br>
		launches the ‘controller’ node from package ‘pid’ under the namespace ‘sway_controller’.<br>
		Sets values to the following parameters - 
		kp, ki, kd <br>
		upper_limit, lower_limit, windup_limit <br>
		max_loop_frequency, min_loop_frequency<br>
		Remaps <br>‘setpoint’ to ‘/sway_scale_setpoint’ <br>
		 ‘state’ to  ‘/sway_scale/’<br>
		 ‘control_effort’ to ‘/sway_controller/control_effort’<br><br>
	4. `surge_controller.launch`<br>
		launches the ‘controller’ node from package ‘pid’ under the namespace ‘surge_controller’.<br>
		Sets values to the following parameters - 
		kp, ki, kd <br>
		upper_limit, lower_limit, windup_limit <br>
		max_loop_frequency, min_loop_frequency<br>
		Remaps <br>‘setpoint’ to ‘/surge_scale_setpoint’ <br>
		 ‘state’ to  ‘/surge_scale/’<br>
		 ‘control_effort’ to ‘/surge_controller/control_effort’<br><br>
	5. `controllers.launch`<br>
		launches all four controllers - heave, sway, yaw, surge. 

	**Launch files for thrusters**

	1. `depth_thruster.launch`
		Launches the node 'depthThrusters' in package 'thrusters'
	2. `vector_thruster.launch`
		Launches the node 'vectorThrusters' in package 'thrusters'
	3. `thrusters.launch` 
		Launches both the nodes - 'depth_thrusters' and 'vectorThrusters'
	


        


{: .no_toc }


{: .fs-6 .fw-300 }
