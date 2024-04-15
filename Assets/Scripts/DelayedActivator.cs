using System.Collections;
using UnityEngine;

public class DelayedActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultTarget; // Default target can be set in the Inspector

    // Method to activate the default target with a delay
    public void ActivateDefaultTarget()
    {
        if (defaultTarget != null)
        {
            StartCoroutine(ActivateAfterDelay(defaultTarget, 3)); // 3-second delay
        }
        else
        {
            Debug.LogError("No default target has been set.");
        }
    }

    // Overloaded method to activate a specific GameObject with a delay
    public void ActivateGameObject(GameObject target)
    {
        if (target != null)
        {
            StartCoroutine(ActivateAfterDelay(target, 3)); // 3-second delay
        }
        else
        {
            Debug.LogError("Target object is null.");
        }
    }

    private IEnumerator ActivateAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
        Debug.Log(target.name + " activated after " + delay + " seconds.");
    }
}