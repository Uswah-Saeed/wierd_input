using System.IO.Ports;
using UnityEngine;

public class ArduinoDataReceiver : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM12", 9600);  // Replace "COM3" with your port name
    public float accelX, accelY, accelZ;
    public float magX, magY, magZ;

    void Start()
    {
        // Open the serial port
        if (!serialPort.IsOpen)
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;  // Set read timeout
        }
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                ParseData(data);
            }
            catch (System.Exception)
            {
                // Handle exceptions or timeouts here
            }
        }
    }

    void ParseData(string data)
    {
        // Expected format: "| ADXL345 Acc[mg]: x y z | Mag[mGauss]: mx my mz |"
        if (data.Contains("ADXL345 Acc[mg]:") && data.Contains("Mag[mGauss]:"))
        {
            // Split and parse the values
            string[] parts = data.Split('|');

            // Parse accelerometer values
            string[] accData = parts[1].Split(' ');
            accelX = float.Parse(accData[3]);
            accelY = float.Parse(accData[4]);
            accelZ = float.Parse(accData[5]);

            // Parse magnetometer values
            string[] magData = parts[2].Split(' ');
            magX = float.Parse(magData[3]);
            magY = float.Parse(magData[4]);
            magZ = float.Parse(magData[5]);

            // Display in Unity Console (for testing)
            Debug.Log($"Accelerometer - X: {accelX}, Y: {accelY}, Z: {accelZ}");
            Debug.Log($"Magnetometer - X: {magX}, Y: {magY}, Z: {magZ}");
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port when Unity application stops
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
