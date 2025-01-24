using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;            // Velocidad de movimiento
    public float maxSpeed = 10f;       // Velocidad máxima
    public float jumpForce = 5f;       // Fuerza del salto
    public float rotationSpeed = 720f; // Velocidad de rotación
    public LayerMask groundLayer;      // Capa que define el suelo

    private Rigidbody rb;              // Rigidbody
    private Vector3 moveDirection;     // Dirección de movimiento
    private bool isGrounded;           // Indica si el jugador está en el suelo
    private Vector3 lastMoveDirection; // Ultimo movimiento guardado (para que no rote por siempre)

    void Start()
    {
        // Obtener el componente Rigidbody del personaje
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        

        // Rotar el personaje hacia la dirección de movimiento
        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Verificar si el jugador está en el suelo
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer);

        // Manejar el salto
        if (isGrounded && Input.GetButtonDown("Jump")) // Barra espaciadora por defecto
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Obtener las entradas del teclado
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");     

        // Calcular la dirección de movimiento relativa al mundo
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Aplicar movimiento horizontal
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = moveDirection * speed;

        // Mantener la velocidad vertical del Rigidbody (para caídas y saltos)
        targetVelocity.y = currentVelocity.y;

        // Limitar la velocidad horizontal máxima
        if (new Vector3(targetVelocity.x, 0f, targetVelocity.z).magnitude > maxSpeed)
        {
            targetVelocity = targetVelocity.normalized * maxSpeed;
            targetVelocity.y = currentVelocity.y; // Asegurar que la velocidad vertical no se modifique
        }

        rb.linearVelocity = targetVelocity;
    }
}