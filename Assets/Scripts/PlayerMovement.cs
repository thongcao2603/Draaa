
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rd;
    private BoxCollider2D boxCollider2D;
    private Animator myAnimator;

    private float wallJumpCountdown;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        myAnimator.SetBool("isRun", Input.GetAxis("Horizontal") != 0);
        myAnimator.SetBool("isGrounded", IsGrounded());

        if (wallJumpCountdown > 0.2f)
        {

            Run(Input.GetAxis("Horizontal"));

            if (OnWall() && !IsGrounded())
            {
                rd.gravityScale = 0;
                rd.velocity = Vector2.zero;
            }
            else
            {
                rd.gravityScale = 3;

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else wallJumpCountdown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rd.velocity = new Vector2(rd.velocity.x, jumpPower);
            myAnimator.SetTrigger("jump");
        }
        else if (OnWall() && !IsGrounded())
        {

            if (Input.GetAxis("Horizontal") == 0)
            {
                rd.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                rd.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCountdown = 0;

        }


    }

    private void Run(float direction)
    {
        rd.velocity = new Vector2(direction * moveSpeed, rd.velocity.y);

        FlipPlayerWhenMoving(direction);
    }

    private void FlipPlayerWhenMoving(float direction)
    {
        if (direction > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0,
                new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return raycastHit.collider != null;
    }

}
