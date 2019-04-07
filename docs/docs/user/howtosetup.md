---
layout: default
title: Setup
parent: User
nav_order: 1
has_children: false
parmalink: docs/user/
---

## **Setup**

1. **Ubuntu 16.04LTS**    
   Instructions for installing Ubuntu 16.04LTS can be found [here](https://tutorials.ubuntu.com/tutorial/tutorial-install-ubuntu-desktop-1604#0). Please stick to Ubuntu 16.Xs only.

2. **ROS**     
   Instructions for installing ROS Kinetic Kame can be found [here](http://wiki.ros.org/kinetic/Installation). Please stick to ROS Kinetic Kame only.

3. **Unity**    
   You can download Unity3D from [here](https://store.unity.com/download) and install it by running the executable file downloaded.

4. **Setting up repository**
	  1. In your Ubuntu terminal enter the following command to git clone the [URSim repository](https://github.com/srmauvsoftware/URSim):     
   	   `git clone https://github.com/srmauvsoftware/URSim.git`     
    2. Source your environnment:     
       `source /opt/ros/kinetic/setup.bash`    
   	   Create a new catkin workspace:     
   	   `mkdir -p ~/catkin_ws/src`    
   	   `cd ~/catkin_ws/`    
   	   `catkin_make`     
    3. Copy the contents of *ros* directory to the new workspace:    
       `cd /path/to/repo/ros ~/catkin_ws/src`       
    4. Install rosbridge:    
       `sudo apt-get install ros-kinetic-rosbridge-suite`                  


         
5. **Port forwarding**
    1. Open your web browser and enter the url for opening the configuration page of your router somewhat similar to the examples below:    
       `TP Link Routers: http://192.168.0.1   `        
       `Asus Routers: http://192.168.1.1     `      
    2. Find the option for port forwarding. It could be in security settings or advanced setting. 
    3. Add a new port and ip address to forward and save the settings.
    4. Restart the router.
          

5. **Setting up URSim**    
	  1. Download the repository from [here](https://github.com/srmauvsoftware/URSim). Extract the downloaded zip file.    
    2. Navigate to /path/to/repo/URSim-master/unity/Assets/Scripts and open  the file `AUV.cs`.
    3. Edit the ip address and the forwarded port of your ubuntu machine in the following line in the file:  
       `ros = new ROSBridgeWebSocketConnection ("<ip address>", <port>);`
       for example:     
       `ros = new ROSBridgeWebSocketConnection ("ws://192.168.0.100", 9090);`
        


   



