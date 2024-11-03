using UnityEngine;

public class ShaftRotator : MonoBehaviour
{
    [SerializeField] CatapultRotator catapultScript;
    public Transform shaft;               // Reference to the shaft object you want to rotate
    public float smoothingSpeed = 5f;     // Normal smoothing factor for interpolation
    public float shootSmoothingSpeed = 20f; // Increased smoothing factor when shooting
    private Vector3 targetRotation = Vector3.zero;
    [SerializeField]
    CatapultController controller;
    // Define your accelerometer value range and rotation angle range
    private float accelerometerMin = 2.39f; // Minimum accelerometer value
    private float accelerometerMax = 10f;    // Maximum accelerometer value
    private float rotationMin = 50f;         // Minimum rotation angle
    private float rotationMax = -10f;        // Maximum rotation angle

    private float lastValue = 0f;           // Store the last accelerometer value
    private bool isShooting = false;        // Flag to check if shooting is happening

    void Update()
    {
        if (shaft != null)
        {
            // Get the current value from the accelerometer
            float currentValue = SampleMessageListener.msgValue;

            // Calculate the mapped rotation angle based on accelerometer value
            float rotationAngle = Map(currentValue, accelerometerMin, accelerometerMax, rotationMin, rotationMax);
            targetRotation = new Vector3(rotationAngle, catapultScript.catapult.localEulerAngles.y, 0);

            // Determine the current smoothing speed based on whether we're shooting
            float currentSmoothingSpeed = isShooting ? shootSmoothingSpeed : smoothingSpeed;

            // Smoothly interpolate the shaft's rotation towards the target rotation
            Quaternion currentRotation = shaft.rotation;
            Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
            shaft.rotation = Quaternion.Lerp(currentRotation, targetQuaternion, currentSmoothingSpeed * Time.deltaTime);

            // Check for the condition to call the Shoot function
            if ((currentValue >= -1 && currentValue <= 3 && lastValue > 4) || (currentValue >= -1 && currentValue <= 3 && lastValue < -10))
            {
                Shoot(); // Call the Shoot function
            }

            // Update lastValue for the next frame
            lastValue = currentValue;
        }
    }

    // Maps a value from one range to another
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Clamp value to the input range
        value = Mathf.Clamp(value, fromMin, fromMax);

        // Calculate the scaled value
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    // Function to be called when the condition is met
    private void Shoot()
    {
        Debug.Log("Shoot function called!");
        // Increase rotation speed temporarily
        isShooting = true;

        // Optionally, start a coroutine to reset the shooting state after a short duration
        Invoke(nameof(ResetShoot), 0.5f); // Adjust the time as needed
        controller.ShootBall();
        // Placeholder for shooting logic
      

       
    }

    // Function to reset shooting state
    private void ResetShoot()
    {
        isShooting = false;
    }
}
