using UnityEngine;

public class SpellCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem endParticleSystem;
    [SerializeField] private GameObject projectile;

    private int targetLayer; // Layer index for the "TargetLayer"

    private void Start()
    {
        targetLayer = LayerMask.NameToLayer("TargetLayer"); // Set the target layer index
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            Instantiate(endParticleSystem, transform.position, Quaternion.identity);
            Destroy(projectile);
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            Instantiate(endParticleSystem, transform.position, Quaternion.identity);
            Destroy(projectile);
        }
    }

}










