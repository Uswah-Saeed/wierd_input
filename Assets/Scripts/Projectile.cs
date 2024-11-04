using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    private Rigidbody rb;
    private TrailRenderer trail;
     Health E_Health;

    [SerializeField] GameObject explosion;
    private void Start()
    {
        E_Health = CatapultController.instance.Enemy.GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        
        if (rb != null)
        {
            //rb.interpolation = RigidbodyInterpolation.Interpolate;
            //rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            //rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }
        
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (trail != null)
        {
            trail.enabled = false;
        }
        if (collision.gameObject.CompareTag("Ship"))
        {
            print("hit");

            E_Health.TakeDamage(10);

        }
        // todo: impact effects
        //        
        //
        GameObject vfx = Instantiate(explosion,this.transform.position,Quaternion.identity);
        vfx.GetComponent<ParticleSystem>().Play();
        Destroy(vfx,2f);
        SoundManager.Instance.PlayProjectileHitSound();

        GetComponent<Collider>().enabled = false;
        
        Destroy(gameObject, 0.1f);
    }
}