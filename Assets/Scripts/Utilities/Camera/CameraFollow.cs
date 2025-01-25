using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El objeto al que sigue la c�mara
    public Vector3 offset = new Vector3(1f, 2f, -5f);  // Offset inicial de la c�mara respecto al objetivo
    public float smoothSpeed = 0.08f;  // Velocidad de suavizado para el seguimiento
    public float mouseSensitivity = 100f;  // Sensibilidad de rotaci�n de la c�mara

    private float rotationX = 0f;  // Acumulador para la rotaci�n horizontal
    private float rotationY = 0f;  // Acumulador para la rotaci�n vertical

    void LateUpdate()
    {
        // Detectar si el bot�n derecho del mouse est� presionado
        if (Input.GetMouseButton(1)) // Bot�n derecho del mouse
        {
            // Movimiento del mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Acumular rotaciones y limitar el eje Y
            rotationX += mouseX;
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, -45f, 45f);  // Limitar la rotaci�n vertical para evitar giros extremos
        }

        // Calcular rotaci�n de la c�mara
        Quaternion cameraRotation = Quaternion.Euler(rotationY, rotationX, 0f);

        // Seguir al objetivo con el offset, aplicando suavizado
        Vector3 desiredPosition = target.position + cameraRotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // La c�mara siempre mira al objetivo
        transform.LookAt(target.position);
    }
}
