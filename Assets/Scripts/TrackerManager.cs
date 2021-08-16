using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


namespace UnityEngine.XR.ARFoundation { 
    public class TrackerManager : MonoBehaviour
    {
        public RosPublisher pub;
        // Publish the cube's position and rotation every N seconds
        public float publishMessageFrequency = 0.5f;

        // Used to determine how much time has elapsed since the last message was published
        private float timeElapsed;


        // Start is called before the first frame update
        void Start()
        {
            // start the ROS connection
            gameObject.GetComponent<ARPoseDriver>().enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            timeElapsed += Time.deltaTime;
            // if scanned QR code:
            //gameObject.GetComponent<ARPoseDriver>().enabled = true;
            if (timeElapsed > publishMessageFrequency)
            {


                // report transform as Pose
                //Debug.Log("Pos" + transform.localPosition);
                //Debug.Log("Rot" + transform.localRotation);



                // Finally send the message to server_endpoint.py running in ROS

                pub.UpdatePos(transform.localPosition, transform.localRotation);

                timeElapsed = 0;
            }

        }
    }

}