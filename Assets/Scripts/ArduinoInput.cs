using UnityEngine;
using System.IO.Ports;
using System.Collections;

public class ArduinoInput : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM12", 9600); // Adjust "COM12" to your Arduino port
    public Vector3 acceleration;  // Stores X, Y, Z accelerometer values
    public Vector3 magnetometer;  // Stores X, Y, Z magnetometer values
    private float previousY;      // Stores previous Y acceleration value

    public Rigidbody objectToShoot;  // Rigidbody to apply force to
    private Transform initialPosition;

    public float rotationScale = 30f;
    public Transform shaftTransform;
    public float interpolationDuration = 0.1f; // Time for smooth transition between points

    private Coroutine rotationCoroutine;

    void Start()
    {
        initialPosition = objectToShoot.transform;

        if (!serialPort.IsOpen)
        {
            serialPort.Open();
            serialPort.ReadTimeout = 1000;
            Debug.Log("Serial port opened.");
        }

        previousY = 0f;
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                string[] values = data.Split(' ');

                if (values.Length >= 11)
                {
                    // Parse accelerometer data
                    acceleration.x = float.Parse(values[0]);
                    acceleration.y = float.Parse(values[1]);
                    acceleration.z = float.Parse(values[2]);

                    // Parse magnetometer data
                    magnetometer.x = float.Parse(values[3]);
                    magnetometer.y = float.Parse(values[4]);
                    magnetometer.z = float.Parse(values[5]);

                    // Log values
                    Debug.Log($"Acc: X = {acceleration.x}, Y = {acceleration.y}, Z = {acceleration.z} | Mag: X = {magnetometer.x}, Y = {magnetometer.y}, Z = {magnetometer.z}");

                    // Start smooth rotation based on magnetometer.y
                    StartSmoothRotation(magnetometer.y);

                    // Detect sudden change in Y-axis for shooting functionality
                    DetectSuddenChange();
                }
                else
                {
                    Debug.LogWarning("Unexpected data format.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading data: " + e.Message);
            }
        }
    }

    void StartSmoothRotation(float magY)
    {
        // Stop any existing rotation coroutine to avoid overlapping
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }

        // Start a new coroutine for smooth rotation
        rotationCoroutine = StartCoroutine(SmoothRotateCoroutine(previousY, magY));
    }

    IEnumerator SmoothRotateCoroutine(float startY, float endY)
    {
        float startRotationY = startY * rotationScale;
        float endRotationY = endY * rotationScale;
        Quaternion startRotation = Quaternion.Euler(startRotationY, 0, 0);
        Quaternion endRotation = Quaternion.Euler(endRotationY, 0, 0);

        float elapsedTime = 0f;

        while (elapsedTime < interpolationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / interpolationDuration;
            shaftTransform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        shaftTransform.rotation = endRotation;
    }

    void DetectSuddenChange()
    {
        Debug.Log($"Previous Y: {previousY}, Current Y: {acceleration.y}");

        if (previousY >= 6 && previousY <= 12 && acceleration.y > 10)
        {
            Debug.Log("Detected a jump on Y-axis.");
            ApplyImpulseForce();
        }

        previousY = acceleration.y;
    }

    void ApplyImpulseForce()
    {
        if (objectToShoot != null)
        {
            Debug.Log("Applying impulse force to the object!");
            objectToShoot.AddForce(Vector3.up * 50f, ForceMode.Impulse);
            Invoke("ResetObject", 5);
        }
        else
        {
            Debug.LogWarning("No object assigned to shoot.");
        }
    }

    void ResetObject()
    {
        objectToShoot.transform.position = initialPosition.position;
        objectToShoot.linearVelocity = Vector3.zero;  // Reset velocity to prevent drifting
    }

    void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
            serialPort.Close();
        Debug.Log("Serial port closed.");
    }
}
