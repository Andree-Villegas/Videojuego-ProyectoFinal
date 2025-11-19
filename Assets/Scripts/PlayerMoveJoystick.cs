using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveJoystick : MonoBehaviour
{
    [Header("Configuración de Controles")]
    public Joystick joystick;

    [Header("Configuración del Jugador")]
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb2D;
    private float moveInput;

    [Header("Detección de Suelo")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // CORRECCIÓN PARA UNITY 6:
        // Usamos FindFirstObjectByType en lugar de FindObjectOfType
        if (joystick == null) joystick = Object.FindFirstObjectByType<Joystick>();
    }

    void Update()
    {
        // 1. LÓGICA DE MOVIMIENTO
        if (joystick != null && (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f))
        {
            // Prioriza el joystick si se está tocando
            moveInput = joystick.Horizontal;
        }
        else
        {
            // Si no, usa el teclado
            moveInput = Input.GetAxisRaw("Horizontal");
        }

        // 2. LÓGICA DE GIRO (FLIP)
        if (moveInput > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < -0.1f) transform.localScale = new Vector3(-1, 1, 1);

        // 3. ANIMACIONES
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // MOVIMIENTO FÍSICO
        rb2D.linearVelocity = new Vector2(moveInput * speed, rb2D.linearVelocity.y);

        // DETECCIÓN DE SUELO
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    // FUNCIÓN PARA EL BOTÓN DE SALTO
    public void Jump()
    {
        if (isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            animator.SetBool("IsGrounded", false);
        }
    }

    // DIBUJO DEL GIZMO (Círculo rojo en los pies)
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("pinchos"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}