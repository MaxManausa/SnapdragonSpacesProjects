using UnityEngine;
using System.Collections.Generic;

// Define a class to store the position and rotation for each child object.
[System.Serializable]
public class TransformData
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformData(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}

// Main script to reset child transforms to their initial states when the parent GameObject is set active.
public class ResetChildrenTransforms : MonoBehaviour
{
    // Dictionary to store the initial transform states for each child.
    private Dictionary<Transform, TransformData> initialStates = new Dictionary<Transform, TransformData>();

    void Start()
    {
        // Capture and store the initial position and rotation for each child at startup.
        foreach (Transform child in transform)
        {
            initialStates[child] = new TransformData(child.position, child.rotation);
        }
    }

    void OnEnable()
    {
        // Reset all children to their original states each time this GameObject is enabled.
        ResetChildTransforms();
    }

    private void ResetChildTransforms()
    {
        // Apply the stored position and rotation to each child.
        foreach (Transform child in transform)
        {
            if (initialStates.TryGetValue(child, out TransformData data))
            {
                child.position = data.position;
                child.rotation = data.rotation;
            }
        }
    }
}