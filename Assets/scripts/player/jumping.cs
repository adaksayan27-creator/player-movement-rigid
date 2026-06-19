using UnityEngine;

public class jumping : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpcooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField]private AudioClip jump;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GetComponent<health>().getdead()) return;

        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (wallJumpcooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (iswall() && !isjump())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                 SoundManager.instance.PlaySound(jump);
            }
                
        }
        else
        {
            wallJumpcooldown += Time.deltaTime;
        }

        // Animation
        anim.SetBool("running", horizontalInput != 0);
        anim.SetBool("jump", !isjump());
    }

    private void Jump()
    {
        if (isjump())
        {
            
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jumping");
        }
        else if (iswall() && !isjump())
        {
            if (transform.localScale.x == 1)
                body.linearVelocity = new Vector2(-speed, jumpPower);
            else
                body.linearVelocity = new Vector2(speed, jumpPower);

            wallJumpcooldown = 0;
        }
    }

    private bool isjump()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return raycastHit.collider != null;
    }

    private bool iswall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            new Vector2(transform.localScale.x, 0),
            0.1f,
            wallLayer
        );

        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isjump() && !iswall();
    }
}