using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    public float vitesse = 1000f; // La vitesse de rotation (modifiable dans l'Inspector)

    void Update()
    {
        // Fait tourner l'objet sur son axe Y (le vert)
        transform.Rotate(0, vitesse * Time.deltaTime, 0);
    }
}