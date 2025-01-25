using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // Referencia a la puerta
    public Vector3 openPositionOffset = new Vector3(0f, 5f, 0f); // Desplazamiento para abrir la puerta (hacia arriba)
    public float openSpeed = 5f; // Velocidad de apertura
    public KeyCode openKey = KeyCode.X; // Tecla para abrir la puerta
    public string playerTag = "Player"; // Tag del jugador

    private Vector3 closedPosition; // Posici�n inicial (cerrada)
    private Vector3 targetPosition; // Posici�n objetivo
    private Collider doorCollider; // Collider de la puerta
    private bool isOpen = false; // Estado de la puerta
    private bool playerNearby = false; // �Est� el jugador cerca?

    void Start()
    {
        // Guardar la posici�n inicial de la puerta
        closedPosition = door.localPosition;
        targetPosition = closedPosition;

        // Obtener el collider de la puerta
        doorCollider = door.GetComponent<Collider>();

        // Validar que el collider exista
        if (doorCollider == null)
        {
            print("No se encontr� un Collider en el objeto puerta. Por favor, verifica que el objeto 'door' tenga un componente Collider.");
        }
    }

    void Update()
    {
        // Solo permitir abrir/cerrar si el jugador est� cerca
        if (playerNearby && Input.GetKeyDown(openKey))
        {
            // Alternar el estado de la puerta
            isOpen = !isOpen;
            targetPosition = isOpen ? closedPosition + openPositionOffset : closedPosition;

            // Activar/desactivar el collider seg�n el estado de la puerta
            if (doorCollider != null)
            {
                doorCollider.enabled = !isOpen; // Desactivar si est� abierta
                print($"Collider de la puerta {(isOpen ? "desactivado" : "activado")}");
            }
        }

        // Suavizar el movimiento hacia la posici�n objetivo
        door.localPosition = Vector3.Lerp(door.localPosition, targetPosition, Time.deltaTime * openSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador toc� la puerta
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = true;
            print("Jugador cerca de la puerta.");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el jugador dej� de tocar la puerta
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = false;
            print("Jugador se alej� de la puerta.");
        }
    }
}
