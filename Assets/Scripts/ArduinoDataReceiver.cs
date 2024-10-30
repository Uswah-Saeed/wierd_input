using UnityEngine;
using System.IO.Ports;

public class ArduinoDataReader : MonoBehaviour
{
    
    // Define the serial port and baud rate
    SerialPort serialPort = new SerialPort("COM12", 9600); // Adjust "COM3" to your Arduino port

    // Variables to store the sensor values
    private float accelX, accelY, accelZ;
    private int magX, magY, magZ;

    void Start()
    {
        // Open the serial port
        serialPort.Open();
        serialPort.ReadTimeout = 1000; // Set a timeout for reading
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

                    // Parse magnetometer values
                    magX = int.Parse(values[8]);
                    magY = int.Parse(values[9]);
                    magZ = int.Parse(values[10]);

                    // Output parsed data
                    Debug.Log($"Accelerometer [mg]: X={accelX} Y={accelY} Z={accelZ}");
                    Debug.Log($"Magnetometer [mGauss]: X={magX} Y={magY} Z={magZ}");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading data: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port when the application ends
        if (serialPort.IsOpen)
            serialPort.Close();
    }
}
