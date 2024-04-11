using UnityEngine;

public class LaunchSpell : MonoBehaviour
{
    [SerializeField] private SpellCastingAura spellCasting;

    [SerializeField] private GameObject fireSpell1;
    [SerializeField] private GameObject fireSpell2;
    [SerializeField] private GameObject fireSpell3;

    // Placeholder for additional spells
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

    public Transform launchPoint; // The point where the prefab will be instantiated
    public float launchForce = 10f; // The force applied to the prefab when launched

    private GameObject prefabToLaunch;
    private int currentSpellLevel;

    // Sets the current spell level and prepares the appropriate spell prefab for launching
    public void PrepareSpell(bool isLeftHand)
    {
        currentSpellLevel = isLeftHand ? spellCasting.leftHandSpellLevel : spellCasting.rightHandSpellLevel;
        WhichSpell(currentSpellLevel);
    }

    public void Launch()
    {
        if (prefabToLaunch == null)
        {
            Debug.LogError("No spell prefab set for launching. Call PrepareSpell() first.");
            return;
        }

        GameObject launchedPrefab = Instantiate(prefabToLaunch, launchPoint.position, launchPoint.rotation);
        Rigidbody rb = launchedPrefab.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(launchPoint.forward * launchForce, ForceMode.VelocityChange);
        }

        Destroy(launchedPrefab, 5f);
        // Reset prefabToLaunch to ensure PrepareSpell() is called before each launch
        prefabToLaunch = null;
    }

    private void WhichSpell(int spellLevel)
    {
        switch (spellLevel)
        {
            case 1:
                prefabToLaunch = fireSpell1;
                break;
            case 2:
                prefabToLaunch = fireSpell2;
                break;
            case 3:
                prefabToLaunch = fireSpell3;
                break;
            default:
                Debug.LogError("Invalid spell level: " + spellLevel);
                break;
        }
    }
}