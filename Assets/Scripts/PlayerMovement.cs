using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 16f;

    [Header("Components")]
    public Rigidbody2D rb;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private float moveInput;
    private bool isGrounded;

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // DETECTION DU SOL
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // SAUT
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // REMPLACEMENT ICI : velocity -> linearVelocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // SAUT VARIABLE
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            // ET ICI AUSSI
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    void FixedUpdate()
    {
        // ET LÀ
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}