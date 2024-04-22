using UnityEngine;

public class SpellCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem endParticleSystem;
    [SerializeField] private GameObject projectile;

    private RoundManager roundManager;
    private int targetLayer; // Layer index for the "TargetLayer"

    private void Start()
    {
        targetLayer = LayerMask.NameToLayer("TargetLayer"); // Set the target layer index
        // Find the RoundManager in the scene
        roundManager = FindObjectOfType<RoundManager>();
        if (roundManager == null)
        {
            Debug.LogError("RoundManager not found in the scene!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == targetLayer || collision.gameObject.CompareTag("Target"))
        {
            if (roundManager != null)
            {
                roundManager.IncrementTargetsHit();
                Instantiate(endParticleSystem, transform.position, Quaternion.identity);
                Destroy(projectile);
            }
            else
            {
                Debug.LogError("RoundManager reference not set!");
            }
        }
    }
}









