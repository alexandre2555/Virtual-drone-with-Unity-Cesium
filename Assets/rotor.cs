using UnityEngine;
using System.Collections;

public class rotor : MonoBehaviour
{

    // I changed this to public so you can edit it in the Inspector
    public float power = 5.0f;

    public bool counterclockwise = false;

    public bool animationActivated = true; // Set to true by default

    // Removed Start() and Rigidbody because they caused conflicts

    void Update()
    {
        if (animationActivated)
        {
            // Keeping the same math logic as before: power * 700
            float currentRotation = power * 700 * Time.deltaTime;

            // Handle direction
            if (counterclockwise)
            {
                currentRotation = -currentRotation;
            }

            // Rotating on Y axis (Green) as we confirmed it works for you
            // (0, Y, 0)
            transform.Rotate(0, 0, currentRotation);
        }
    }

    // Removed FixedUpdate() to prevent the drone from flying away uncontrollably
}