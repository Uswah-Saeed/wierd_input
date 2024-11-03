using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour
{

    #region Vars
    [Header("Catapult Rotation")]
    [SerializeField] private Transform catapultArm;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float restingAngle = 30f;   // starting pos
    [SerializeField] private float minAngle = 0f;        // end pos
    [SerializeField] private float shootAngle = 55f;     // shooting pos
    [SerializeField] private float baseShootingForce = 15f;
    [SerializeField] private float snapSpeed = 200f;     // shooting speed
    [SerializeField] private float maxRotationAngle = 20f;  // max rotation in each dir
    [SerializeField] private float forceFactor = 15f;  // max rotation in each dir

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;


    [Header("Trajectory")]
    [SerializeField] private LineRenderer trajectoryLine;
    [Range(10,50)][SerializeField] private int trajectoryPoints = 50;
    [Range(0.01f, 0.1f)][SerializeField] private float trajectoryTimeStep = 0.05f;
    [SerializeField] private Vector3 direction = Vector3.up;

    private float currentAngle;
    private float currentRotationAngle = 0f;
    private bool isShooting;
    private bool canShoot = true;
    public static CatapultController instance;
    public GameObject Enemy;
    #endregion
    private void Awake()
    {
        instance=this;

    }
    private void Start()
    {
        currentAngle = restingAngle;
        UpdateArmRotation();
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (trajectoryLine == null)
        {
            trajectoryLine = gameObject.AddComponent<LineRenderer>();
        }

        trajectoryLine.positionCount = trajectoryPoints;
        trajectoryLine.startWidth = 0.1f;
        trajectoryLine.endWidth = 0.1f;
        //trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.startColor = new Color(1, 1, 1, 0.5f);
        trajectoryLine.endColor = new Color(1, 1, 1, 0f);
    }

    private void Update()
    {
        if (isShooting)
        {
            trajectoryLine.enabled = false;
            return;
        }

        if (canShoot)
        {
            HandleInput();
            trajectoryLine.enabled = currentAngle < restingAngle;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentAngle = Mathf.Max(minAngle, currentAngle - rotationSpeed * Time.deltaTime);
            UpdateArmRotation();
            UpdateTrajectory();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentAngle = Mathf.Min(restingAngle, currentAngle + rotationSpeed * Time.deltaTime);
            UpdateArmRotation();
            UpdateTrajectory();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float newRotation = currentRotationAngle - rotationSpeed * Time.deltaTime;
            currentRotationAngle = Mathf.Clamp(newRotation, -maxRotationAngle, maxRotationAngle);
            transform.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
            UpdateTrajectory();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float newRotation = currentRotationAngle + rotationSpeed * Time.deltaTime;
            currentRotationAngle = Mathf.Clamp(newRotation, -maxRotationAngle, maxRotationAngle);
            transform.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
            UpdateTrajectory();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootSequence_IE());
        }
    }

    public void ShootBall()
    {
        StartCoroutine(ShootSequence_IE());
    }
    private void UpdateTrajectory()
    {
        Vector3 velocity = GetLanchDirection() * GetFinalForce();
        Vector3 position = shootPoint.position;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * trajectoryTimeStep;
            Vector3 point = CalculateTrajectoryPoint(position, velocity, time);
            trajectoryLine.SetPosition(i, point);
        }
    }

    private Vector3 GetLanchDirection()
    {
        Vector3 launchDirection = transform.forward + direction * 0.5f;
        launchDirection.Normalize();
        return launchDirection;
    }

    private float GetFinalForce()
    {
        float pullbackRange = restingAngle - minAngle;
        float currentPullback = restingAngle - currentAngle;
        float powerPercentage = currentPullback / pullbackRange;
        float finalForce = baseShootingForce * (1 + powerPercentage * forceFactor);
        print(finalForce);
        return finalForce; // final force based on how much the arm is down
    }

    private Vector3 CalculateTrajectoryPoint(Vector3 startPosition, Vector3 startVelocity, float time)
        => startPosition + startVelocity * time + 0.5f * Physics.gravity * time * time;  // d = vt + 1/2gt^2

    private void UpdateArmRotation() => catapultArm.localRotation = Quaternion.Euler(currentAngle, 0, 0);

    private IEnumerator ShootSequence_IE()
    {
        if (!canShoot) yield break;

        canShoot = false;
        isShooting = true;
        trajectoryLine.enabled = false;

        // snap to shooting angle
        float startAngle = currentAngle;
        float elapsed = 0f;
        float snapDuration = 0.1f;

        while (elapsed < snapDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / snapDuration;
            currentAngle = Mathf.Lerp(startAngle, shootAngle, t);
            UpdateArmRotation();
            yield return null;
        }

        currentAngle = shootAngle;
        UpdateArmRotation();

        // spawn  projectile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = -GetLanchDirection() * GetFinalForce(); //?? fixme
        }
        else { print("FFFFFFFFFFFFuuuuuu"); }

        yield return new WaitForSeconds(0.1f);

        // back to initial pos
        elapsed = 0f;
        float returnDuration = 0.3f;

        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / returnDuration;
            currentAngle = Mathf.Lerp(shootAngle, restingAngle, t);
            UpdateArmRotation();
            yield return null;
        }

        currentAngle = restingAngle;
        UpdateArmRotation();

        isShooting = false;
        canShoot = true;
    }
}