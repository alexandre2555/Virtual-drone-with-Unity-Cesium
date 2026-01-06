using UnityEngine;
using System.Collections;

public class DroneMovementScript : MonoBehaviour
{

    Rigidbody ourDrone;

    void Awake()
    {
        ourDrone = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerwe();

        ourDrone.AddRelativeForce(Vector3.up * upForce);

        ourDrone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, tiltAmountSideways)
        );
    }

    public float upForce;
    void MovementUpDown()
    {
        if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f))
        {
            if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
            {
                ourDrone.linearVelocity = ourDrone.linearVelocity;
            }
            if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L))
            {
                ourDrone.linearVelocity = new Vector3(ourDrone.linearVelocity.x, Mathf.Lerp(ourDrone.linearVelocity.y, 0, Time.deltaTime * 5), ourDrone.linearVelocity.z);
                upForce = 281;
            }
            if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)))
            {
                ourDrone.linearVelocity = new Vector3(ourDrone.linearVelocity.x, Mathf.Lerp(ourDrone.linearVelocity.y, 0, Time.deltaTime * 5), ourDrone.linearVelocity.z);
                upForce = 110;
            }
            if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
            {
                upForce = 410;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }


        if (Input.GetKey(KeyCode.I))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        else if (Input.GetKey(KeyCode.K))
        {
            upForce = -200;
        }
        else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

// Variables priv�es pour avancer (visibles en bas de l'image 1)
private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float titltVelocityForward; // Note : Faute de frappe "titlt" copi�e de l'image

    void MovementForward()
    {
        // Code visible sur l'image 2
        if (Input.GetAxis("Vertical") != 0)
        {
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);

            // Calcul de l'inclinaison douce (SmoothDamp)
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref titltVelocityForward, 0.1f);
        }
    }

    private float wantedYRotation;
    private float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;
    void Rotation()
    {
        if (Input.GetKey(KeyCode.J))
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.L))
        {
            wantedYRotation += rotateAmountByKeys;
        }
        // Lissage de la rotation (SmoothDamp)
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    }

    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues()
    {
        // Cas 1 : On bouge en Diagonale (Avant + C�t�) -> Vitesse max 10
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.linearVelocity = Vector3.ClampMagnitude(ourDrone.linearVelocity, Mathf.Lerp(ourDrone.linearVelocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        // Cas 2 : On bouge seulement en Avant/Arri�re -> Vitesse max 10
        // J'ai corrig� "Verical" en "Vertical" ici
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.linearVelocity = Vector3.ClampMagnitude(ourDrone.linearVelocity, Mathf.Lerp(ourDrone.linearVelocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        // Cas 3 : On bouge seulement sur les C�t�s -> Vitesse max 5 (plus lent)
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.linearVelocity = Vector3.ClampMagnitude(ourDrone.linearVelocity, Mathf.Lerp(ourDrone.linearVelocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        // Cas 4 : On ne touche � RIEN -> FREINAGE (SmoothDamp vers zero)
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.linearVelocity = Vector3.SmoothDamp(ourDrone.linearVelocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmoutVelocity;
    void Swerwe()
    { 
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -20 * Input.GetAxis("Horizontal"), ref tiltAmoutVelocity, 0.1f);
        }
        else
        {
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmoutVelocity, 0.1f);
        }
    }

}