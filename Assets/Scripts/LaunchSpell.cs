using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSpell : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public GameObject prefabToLaunch;
    public Transform launchPoint; // The point where the prefab will be instantiated

    // The force applied to the prefab when launched
    public float launchForce = 10f;



    public void Launch()
    {
        if (prefabToLaunch != null && launchPoint != null)
        {
            // Instantiate the prefab at the launchPoint's position and rotation
            GameObject launchedPrefab = Instantiate(prefabToLaunch, launchPoint.position, launchPoint.rotation);

            // Check if the prefab has a Rigidbody component to apply force
            Rigidbody rb = launchedPrefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply a force to launch the prefab
                rb.AddForce(launchPoint.forward * launchForce, ForceMode.VelocityChange);
            }
            else
            {
                Debug.LogWarning("Launched prefab does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Prefab to launch or launchPoint is not assigned.");
        }
    }
}
