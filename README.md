# UnityARFoundation-ROS

## Feature 1: connect with ROS using [Unity Robotics Hub](https://github.com/Unity-Technologies/Unity-Robotics-Hub)

1. Followed this [tutorial](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/setup.md) to setup ROS-Unity TCP connection.
2. For ROS IP Address, enter the ip address of the device that will run the rosnode/roscore later.
3. To build for Android, some special steps are needed [here](https://github.com/Unity-Technologies/Unity-Robotics-Hub/issues/169#issuecomment-788323044)
4. Followed [this page](https://github.com/Unity-Technologies/Unity-Robotics-Hub/tree/main/tutorials/ros_unity_integration) to add [publisher](https://github.com/KKKKKyle/UnityARFoundation-ROS/blob/master/Assets/Scripts/RosPublisher.cs) and subscriber (not used).
5. Tested on ROS1 Melodic. Start with `roscore` and `rosrun ros_tcp_endpoint default_server_endpoint.py`. Use `rostopic echo pos_rot` to show messages received from Unity.

## Feature 2: add AR to Unity

1. use ARFoundation to enable AR in Unity. Follow [this guide](https://codelabs.developers.google.com/arcore-unity-ar-foundation#1) to set up the environment.
2. Add Unity GameObjects **AR Session** and **AR Session Origin**.

## Feature3: track device position related to origin

Use the gameobject "AR Pose Tracker" to track device movement
  1. Component "AR Pose Driver" will update its transofrm
  2. Component "Ros Publisher" to define the ros message and send to the topic **pos_rot**.
  3. Component "Track Manager" will send the transform in the form of a ros message every 0.5s.
  
## Feature4: report QR Code position and show 3D image on it

0. Followed the Image Tracking demo [here](https://github.com/Unity-Technologies/arfoundation-demos#image-tracking--also-available-on-the-asset-store-here).
1. Use the component "AR Tracked Image Manager" under "AR Session Origin" to "memorize" the qr code (stored under **Serialized Library**) that will be detected. Add **TrackedImagePrefab** that will be shown on the QR Code once detected.
2. Use the component "Ros Publisher" to define the ros message and send to the topic **qr_rot**.
  
