using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;

/// <summary>
///
/// </summary>
public class RosPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string topicName;

    private float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;


    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.instance;
        ros.RegisterPublisher<PosRotMsg>(topicName);
    }

    private void Update()
    {
        //timeElapsed += Time.deltaTime;

        //if (timeElapsed > publishMessageFrequency)
        //{
        //    transform.rotation = Random.rotation;

        //    PosRotMsg cubePos = new PosRotMsg(
        //        transform.position.x,
        //        transform.position.y,
        //        transform.position.z,
        //        transform.rotation.x,
        //        transform.rotation.y,
        //        transform.rotation.z,
        //        transform.rotation.w
        //    );

        //    // Finally send the message to server_endpoint.py running in ROS
        //    ros.Send(topicName, cubePos);

        //    timeElapsed = 0;
        //}
    }

    public void UpdatePos(Vector3 pos, Quaternion rot) {
        PosRotMsg currPos = new PosRotMsg(
            pos.x,
            pos.y,
            pos.z,
            rot.x,
            rot.y,
            rot.z,
            rot.w
        );

        // Finally send the message to server_endpoint.py running in ROS
        ros.Send(topicName, currPos);
    }

    public void UpdateQR(Vector3 pos, Quaternion rot)
    {
        PosRotMsg currPos = new PosRotMsg(
            pos.x,
            pos.y,
            pos.z,
            rot.x,
            rot.y,
            rot.z,
            rot.w
        );

        // Finally send the message to server_endpoint.py running in ROS
        ros.Send(topicName, currPos);
    }
}