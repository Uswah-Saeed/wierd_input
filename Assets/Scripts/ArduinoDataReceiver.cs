using UnityEngine;
using System.IO.Ports;
using System.Collections;

public class ArduinoDataReader : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM12", 9600); // Adjust "COM12" to your Arduino port
    private float accelY;
    private float prevAccelY;
    public Vector3 acceleration;
    private float previousY;
    public float rotationScale = 30f;
    public Transform shaftTransform;
    public float interpolationDuration = 0.1f; // Time for smooth transition between points

    private Coroutine rotationCoroutine;

    public Rigidbody objectToShoot;  // Rigidbody to apply force to
    private Transform initialPosition;
    private float thresholdY = 10f;  // Define a threshold for Y acceleration to trigger shooting

    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 1000;
        Debug.Log("Serial port opened.");

        // Store the initial position of the object to shoot
        if (objectToShoot != null)
        {
            initialPosition = objectToShoot.transform;
        }
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
                   
                    acceleration.y = float.Parse(values[4]);
                   // Debug.Log($"Raw accelY: {accelY}");

                    // Start smooth rotation coroutine when new data arrives
                  

                    // Check for shooting condition
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

    

   
    
    void ResetObject()
    {
        if (objectToShoot != null)
        {
            objectToShoot.transform.position = initialPosition.position;  // Reset position
            objectToShoot.linearVelocity = Vector3.zero;  // Reset velocity to prevent drifting
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
            serialPort.Close();
        Debug.Log("Serial port closed.");
    }

    void DetectSuddenChange()
    {
        //Debug.Log($"Previous Y: {previousY}, Current Y: {acceleration.y}");

        if (previousY >= 6 && previousY <= 12 && acceleration.y < 10)
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
            objectToShoot.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            
        }
        else
        {
            Debug.LogWarning("No object assigned to shoot.");
        }
    }
}
