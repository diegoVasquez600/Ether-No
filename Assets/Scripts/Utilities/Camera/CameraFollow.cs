using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El objeto al que sigue la c�mara
    public Vector3 offset;    // Offset de la c�mara respecto al personaje
    public float smoothSpeed = 0.08f;  // Velocidad de suavizado

    void LateUpdate()
    {
        offset = new Vector3(1f, 2f, -5f);
        Vector3 desiredPosition = target.position + target.rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Hacer que la c�mara mire siempre al objetivo
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed);
    }
}