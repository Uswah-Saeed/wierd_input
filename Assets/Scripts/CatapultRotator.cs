using UnityEngine;

public class CatapultRotator : MonoBehaviour
{
    public  Transform catapult; // Assign the catapult GameObject in the inspector
    public float minMagnetometerValue = 230f;
    public float maxMagnetometerValue = 150f;
    public float minRotation = 15f;
    public float maxRotation = -15f;
    public float rotationSpeed = 5f; // Adjust the speed of interpolation

    private float targetRotation;

    void Update()
    {
        // Smoothly interpolate the rotation towards the target rotation
        float currentRotation = Mathf.LerpAngle(catapult.localEulerAngles.y, targetRotation, Time.deltaTime * rotationSpeed);
        catapult.localEulerAngles = new Vector3(catapult.localEulerAngles.x, currentRotation, catapult.localEulerAngles.z);

        // Set the target rotation based on the static magnetometer value
        targetRotation = Map(SampleMessageListener.msg2Value, minMagnetometerValue, maxMagnetometerValue, minRotation, maxRotation);
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Clamp the input value to avoid out of range
        value = Mathf.Clamp(value, fromMin, fromMax);
        return toMin + (toMax - toMin) * ((value - fromMin) / (fromMax - fromMin));
    }
}
