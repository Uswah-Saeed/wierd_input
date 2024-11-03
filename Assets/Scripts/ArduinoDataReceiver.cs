using UnityEngine;
using System.IO.Ports;

public class ArduinoDataReader : MonoBehaviour
{
    // Define the serial port and baud rate
    SerialPort serialPort = new SerialPort("COM12", 9600); // Adjust "COM12" to your Arduino port

    // Variables to store the sensor values
    private float accelX, accelY, accelZ;
    private int magX, magY, magZ;

    // Adjust rotation scale and reference to the Unity shaft object
    public float rotationScale = 30f; // Amplify the rotation based on accelY range
    public Transform shaftTransform;

    void Start()
    {
        // Open the serial port
        serialPort.Open();
        serialPort.ReadTimeout = 1000; // Set a timeout for reading
        Debug.Log("Serial port opened.");
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                // Read data from Arduino
                string data = serialPort.ReadLine();

                // Parse data assuming the format is "| ADXL345 Acc[mg]: x y z | Mag[mGauss]: x y z |"
                string[] values = data.Split(' ');

                // Ensure the received data is in the expected format
                if (values.Length >= 11)
                {
                    // Parse accelerometer values
                    accelX = float.Parse(values[3]);
                    accelY = float.Parse(values[4]);
                    accelZ = float.Parse(values[5]);

                    Debug.Log($"Raw accelY: {accelY}");

                    // Rotate shaft immediately based on accelY
                    ApplyDirectRotation();

                    // Output parsed data for debugging
                    Debug.Log($"Accelerometer [mg]: X={accelX} Y={accelY} Z={accelZ}");
                    Debug.Log($"Magnetometer [mGauss]: X={magX} Y={magY} Z={magZ}");
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

    void ApplyDirectRotation()
    {
        // Directly apply rotation based on accelY, scaled up for visibility
        float targetRotationY = accelY * rotationScale;
        shaftTransform.rotation = Quaternion.Euler(targetRotationY,0, 0);

        Debug.Log($"Applied Rotation Y: {targetRotationY}"); // Check the actual rotation applied
    }

    void OnApplicationQuit()
    {
        // Close the serial port when the application ends
        if (serialPort.IsOpen)
            serialPort.Close();
        Debug.Log("Serial port closed.");
    }
}
