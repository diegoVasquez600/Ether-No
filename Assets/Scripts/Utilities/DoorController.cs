using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // Referencia a la puerta
    public Vector3 openPositionOffset = new Vector3(0f, 5f, 0f); // Desplazamiento para abrir la puerta (hacia arriba)
    public float openSpeed = 5f; // Velocidad de apertura
    public KeyCode openKey = KeyCode.X; // Tecla para abrir la puerta
    public string playerTag = "Player"; // Tag del jugador

    private Vector3 closedPosition; // Posición inicial (cerrada)
    private Vector3 targetPosition; // Posición objetivo
    private Collider doorCollider; // Collider de la puerta
    private bool isOpen = false; // Estado de la puerta
    private bool playerNearby = false; // ¿Está el jugador cerca?

    void Start()
    {
        // Guardar la posición inicial de la puerta
        closedPosition = door.localPosition;
        targetPosition = closedPosition;

        // Obtener el collider de la puerta
        doorCollider = door.GetComponent<Collider>();

        // Validar que el collider exista
        if (doorCollider == null)
        {
            print("No se encontró un Collider en el objeto puerta. Por favor, verifica que el objeto 'door' tenga un componente Collider.");
        }
    }

    void Update()
    {
        // Solo permitir abrir/cerrar si el jugador está cerca
        if (playerNearby && Input.GetKeyDown(openKey))
        {
            // Alternar el estado de la puerta
            isOpen = !isOpen;
            targetPosition = isOpen ? closedPosition + openPositionOffset : closedPosition;

            // Activar/desactivar el collider según el estado de la puerta
            if (doorCollider != null)
            {
                doorCollider.enabled = !isOpen; // Desactivar si está abierta
                print($"Collider de la puerta {(isOpen ? "desactivado" : "activado")}");
            }
        }

        // Suavizar el movimiento hacia la posición objetivo
        door.localPosition = Vector3.Lerp(door.localPosition, targetPosition, Time.deltaTime * openSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador tocó la puerta
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = true;
            print("Jugador cerca de la puerta.");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el jugador dejó de tocar la puerta
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = false;
            print("Jugador se alejó de la puerta.");
        }
    }
}
