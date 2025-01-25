using UnityEngine;

public class BoxLidController : MonoBehaviour
{
    public Transform lid; // Referencia al hijo que es la tapa
    public Vector3 openRotation = new Vector3(-90f, 0f, 0f); // Rotación para abrir la tapa
    public float openSpeed = 5f; // Velocidad de apertura
    public KeyCode openKey = KeyCode.X; // Tecla para abrir la tapa
    public string playerTag = "Player"; // Tag player

    private Quaternion closedRotation; // Rotación inicial (cerrada)
    private Quaternion targetRotation; // Rotación objetivo
    private bool isOpen = false; // Estado de la tapa
    private bool playerNearby = false; // ¿Está el jugador cerca?

    void Start()
    {
     
        // Guardar la rotación inicial de la tapa
        closedRotation = lid.localRotation;
        targetRotation = closedRotation;
    }

    void Update()
    {
        // Solo permitir abrir/cerrar si el jugador está cerca
        if (playerNearby && Input.GetKeyDown(openKey))
        {
            // Alternar el estado de la tapa
            isOpen = !isOpen;
            targetRotation = isOpen ? Quaternion.Euler(openRotation) : closedRotation;
        }

        // Suavizar la rotación hacia el objetivo
        lid.localRotation = Quaternion.Lerp(lid.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador tocó el objeto
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el jugador dejó de tocar el objeto
        if (collision.collider.CompareTag(playerTag))
        {
            playerNearby = false;
        }
    }
}
