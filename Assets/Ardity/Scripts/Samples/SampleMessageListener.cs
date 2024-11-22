/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{

    public static float msgValue;
    public static float msg2Value;
    [SerializeField] CatapultRotator rotator;
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        string[] values = msg.Split(',');
        if (float.TryParse(values[0], out float value))
        {
            msgValue = value*-1;
        }

        if (float.TryParse(values[1], out float value2))
        {
            msg2Value = value2;
        }
        Debug.Log("Message arrived: " + msg);
       
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
