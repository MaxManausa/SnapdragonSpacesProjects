using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSpell : MonoBehaviour
{
    [SerializeField] private SpellCastingAura spellCasting;

    [SerializeField] private GameObject fireSpell1;
    [SerializeField] private GameObject fireSpell2;
    [SerializeField] private GameObject fireSpell3;

    /*
    [SerializeField] private GameObject iceSpell1;
    [SerializeField] private GameObject iceSpell2;
    [SerializeField] private GameObject iceSpell3;
    [SerializeField] private GameObject lightningSpell1;
    [SerializeField] private GameObject lightningSpell2;
    [SerializeField] private GameObject lightningSpell3;
    [SerializeField] private GameObject gravitySpell1;
    [SerializeField] private GameObject gravitySpell2;
    [SerializeField] private GameObject gravitySpell3;
    */

    // Assign these in the Unity Inspector
    public GameObject prefabToLaunch;
    public Transform launchPoint; // The point where the prefab will be instantiated

    // The force applied to the prefab when launched
    public float launchForce = 10f;


    public void Launch()
    {
        WhichSpell();

        // Instantiate the prefab at the launchPoint's position and rotation
        
        GameObject launchedPrefab = Instantiate(prefabToLaunch, launchPoint.position, launchPoint.rotation);

        // Check if the prefab has a Rigidbody component to apply force
        Rigidbody rb = launchedPrefab.GetComponent<Rigidbody>();
        if (rb != null)
        {
        // Apply a force to launch the prefab
            rb.AddForce(launchPoint.forward * launchForce, ForceMode.VelocityChange);
        }

        Destroy(launchedPrefab, 5f);
    }

    public void WhichSpell()
    {
        // if blank magic, do blank magic
        if (spellCasting.spellLevel == 1)
        {
            prefabToLaunch = fireSpell1;
        }
        else if (spellCasting.spellLevel == 2)
        {
            prefabToLaunch = fireSpell2;
        }
        else if (spellCasting.spellLevel == 3)
        {
            prefabToLaunch = fireSpell3;
        }
    }
}
