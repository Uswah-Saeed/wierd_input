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
            E_Health.TakeDamage(10);
            SoundManager.Instance.PlayProjectileHitSound();
            GameObject vfx = Instantiate(explosion, this.transform.position, Quaternion.identity);
            vfx.GetComponent<ParticleSystem>().Play();
            Destroy(vfx, 2f);
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }
}