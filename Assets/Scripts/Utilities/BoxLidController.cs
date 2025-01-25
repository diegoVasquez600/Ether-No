using UnityEngine;

public class BoxLidController : MonoBehaviour
{
    public Transform lid; // Referencia al hijo que es la tapa
    public Vector3 openRotation = new Vector3(-90f, 0f, 0f); // Rotaci�n para abrir la tapa
    public float openSpeed = 5f; // Velocidad de apertura
    public KeyCode openKey = KeyCode.X; // Tecla para abrir la tapa
    public string playerTag = "Player"; // Tag player

    private Quaternion closedRotation; // Rotaci�n inicial (cerrada)
    private Quaternion targetRotation; // Rotaci�n objetivo
    private bool isOpen = false; // Estado de la tapa
    private bool playerNearby = false; // �Est� el jugador cerca?

    void Start()
    {
     
        // Guardar la rotaci�n inicial de la tapa
        closedRotation = lid.localRotation;
        targetRotation = closedRotation;
    }

    void Update()
    {
        // Solo permitir abrir/cerrar si el jugador est� cerca
        if (playerNearby && Input.GetKeyDown(openKey))
        {
            // Alternar el estado de la tapa
            isOpen = !isOpen;
            targetRotation = isOpen ? Quaternion.Euler(openRotation) : closedRotation;
        }

        // Suavizar la rotaci�n hacia el objetivo
        lid.localRotation = Quaternion.Lerp(lid.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador toc� el objeto
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el jugador dej� de tocar el objeto
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = false;
        }
    }
}
