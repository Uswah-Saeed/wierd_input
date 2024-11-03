using UnityEngine;
using System.Collections;

public class EnemyShootsAl : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public int distanceFactor = 20;
    public int heightFactor = 5;
    public float shootDelay = 1f;

    private float currentHeightMultiplier = 1f;
    private float currentDistanceMultiplier = 1f;
    private bool canShoot = true;

    public float xVariationRange = 1f; // Range for random x variation
    public float yVariationRange = 1f; // Range for random y variation

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }
 
    private void Shoot()
    {

        if (canShoot)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            DestroyBall(projectile);
            Vector3 shootDirection = CalculateProjectileDirection();
            projectile.GetComponent<EnemyProjectile>().Initialize(shootDirection);
            StartCoroutine(ShootCooldown());
        }
    }
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (canShoot)
            {
                Shoot();
                yield return new WaitForSeconds(Random.Range(shootDelay - 1f, shootDelay + 1f));
            }
            else
            {
                yield return null; // Wait for next frame
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;  // Disable shooting
        yield return new WaitForSeconds(shootDelay);  // Wait for the delay time
        canShoot = true;   // Enable shooting again after delay
    }
    void DestroyBall(GameObject ball)
    {
        Destroy(ball, 5f);
    }
    private Vector3 CalculateProjectileDirection()
    {
        float randomX = Random.Range(-xVariationRange, xVariationRange);
        float randomY = Random.Range(-yVariationRange, yVariationRange);

        Vector3 direction = -transform.right  * distanceFactor * currentDistanceMultiplier +
                            transform.up * heightFactor * currentHeightMultiplier;
        direction.x += randomX;
        direction.y += randomY;
        return direction.normalized;
    }

  
}
