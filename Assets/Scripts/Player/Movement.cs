using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 720f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo
        isGrounded = Physics.CheckSphere(transform.position, 0.3f, groundLayer);

        // Manejar el salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Actualizar la animación
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // Obtener las entradas del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular la dirección de movimiento
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Aplicar movimiento horizontal de forma segura
        if (moveDirection != Vector3.zero)
        {
            Vector3 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            //NO FUNCIONA
            // Rotar el personaje hacia la dirección de movimiento
            //Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            //rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime));
        }
    }

    private void UpdateAnimation()
    {
        bool isMoving = moveDirection.magnitude > 0;
        animator.SetBool("isMoving", isMoving);
    }
}
