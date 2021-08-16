using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image manager on the AR Session Origin")]
    ARTrackedImageManager m_ImageManager;

    public RosPublisher pub;

    /// <summary>
    /// Get the <c>ARTrackedImageManager</c>
    /// </summary>
    public ARTrackedImageManager ImageManager
    {
        get => m_ImageManager;
        set => m_ImageManager = value;
    }

    [SerializeField]
    [Tooltip("Reference Image Library")]
    XRReferenceImageLibrary m_ImageLibrary;

    /// <summary>
    /// Get the <c>XRReferenceImageLibrary</c>
    /// </summary>
    public XRReferenceImageLibrary ImageLibrary
    {
        get => m_ImageLibrary;
        set => m_ImageLibrary = value;
    }

    [SerializeField]
    [Tooltip("Prefab for tracked 1 image")]
    GameObject m_QRPrefab;

    /// <summary>
    /// Get the one prefab
    /// </summary>
    public GameObject QRPrefab
    {
        get => m_QRPrefab;
        set => m_QRPrefab = value;
    }

    GameObject m_SpawnedQRPrefab;

    /// <summary>
    /// get the spawned one prefab
    /// </summary>
    public GameObject spawnedQRPrefab
    {
        get => m_SpawnedQRPrefab;
        set => m_SpawnedQRPrefab = value;
    }

    int m_NumberOfTrackedImages;

    NumberManager m_QRNumberManager;

    static Guid s_QRImageGUID;

    void OnEnable()
    {
        s_QRImageGUID = m_ImageLibrary[0].guid;

        m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // added, spawn prefab
        foreach (ARTrackedImage image in obj.added)
        {
            if (image.referenceImage.guid == s_QRImageGUID)
            {
                m_SpawnedQRPrefab = Instantiate(m_QRPrefab, image.transform.position, image.transform.rotation);
                m_QRNumberManager = m_SpawnedQRPrefab.GetComponent<NumberManager>();
                pub.UpdateQR(image.transform.position, image.transform.rotation);

            }
        }

        // updated, set prefab position and rotation
        foreach (ARTrackedImage image in obj.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (image.trackingState == TrackingState.Tracking)
            {
                if (image.referenceImage.guid == s_QRImageGUID)
                {
                    m_QRNumberManager.Enable3DNumber(true);
                    m_SpawnedQRPrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    
                }
            }
            // image is no longer tracking, disable visuals TrackingState.Limited TrackingState.None
            else
            {
                if (image.referenceImage.guid == s_QRImageGUID)
                {
                    m_QRNumberManager.Enable3DNumber(false);
                }
            }
        }


        // removed, destroy spawned instance
        foreach (ARTrackedImage image in obj.removed)
        {
            if (image.referenceImage.guid == s_QRImageGUID)
            {
                Destroy(m_SpawnedQRPrefab);
            }
        }
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_NumberOfTrackedImages++;
            }
        }
        return m_NumberOfTrackedImages;
    }
}