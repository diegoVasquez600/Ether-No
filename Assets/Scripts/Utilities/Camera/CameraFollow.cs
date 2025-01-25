using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El objeto al que sigue la cámara
    public Vector3 offset = new Vector3(1f, 2f, -5f);  // Offset inicial de la cámara respecto al objetivo
    public float smoothSpeed = 0.08f;  // Velocidad de suavizado para el seguimiento
    public float mouseSensitivity = 100f;  // Sensibilidad de rotación de la cámara

    private float rotationX = 0f;  // Acumulador para la rotación horizontal
    private float rotationY = 0f;  // Acumulador para la rotación vertical

    void LateUpdate()
    {
        // Detectar si el botón derecho del mouse está presionado
        if (Input.GetMouseButton(1)) // Botón derecho del mouse
        {
            // Movimiento del mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Acumular rotaciones y limitar el eje Y
            rotationX += mouseX;
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, -45f, 45f);  // Limitar la rotación vertical para evitar giros extremos
        }

        // Calcular rotación de la cámara
        Quaternion cameraRotation = Quaternion.Euler(rotationY, rotationX, 0f);

        // Seguir al objetivo con el offset, aplicando suavizado
        Vector3 desiredPosition = target.position + cameraRotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // La cámara siempre mira al objetivo
        transform.LookAt(target.position);
    }
}
