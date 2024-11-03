using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    Health P_Health;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
    }
    private void Start()
    {
        P_Health = CatapultController.instance.GetComponent<Health>();
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Catapult"))
        {
            print("Hit");
            P_Health.TakeDamage(10);
            Destroy(gameObject, 0.1f);

        }
    }
}
