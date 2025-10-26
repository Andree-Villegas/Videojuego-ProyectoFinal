using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public float speed = 5;
    private Rigidbody2D rb2D; //Accede al componenete de físicas en unity
    private float move;
    public float jumpforce = 4;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;
    private Animator animator;

    private int coins;
    public TMP_Text textCoins;

    void Start()

    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()

    {
        move = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        if (Input.GetButtonDown("Jump") && isGrounded)

        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpforce);
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);

    }


    private void FixedUpdate()

    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Moneda"))
        {
            Destroy(collision.gameObject);
            coins++;
            textCoins.text = coins.ToString();
        }
    }

}