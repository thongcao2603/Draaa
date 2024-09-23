
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rd;
    private Animator myAnimator;
    private float direction;

    private bool isGrounded;

    [SerializeField] private int maxJump;
    [SerializeField] private int countJump;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        direction = Input.GetAxis("Horizontal");
        Run(direction);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        AnimChange(direction);
    }

    private void Jump()
    {
        if (countJump >= maxJump)
        {
            return;
        }
        else
        {
            countJump++;
            rd.velocity = new Vector2(rd.velocity.x, moveSpeed);
            myAnimator.SetTrigger("jump");
        }
        isGrounded = false;
    }

    private void Run(float direction)
    {
        rd.velocity = new Vector2(direction * moveSpeed, rd.velocity.y);
        AdjustHeroFacingDirect(direction);
    }

    private void AdjustHeroFacingDirect(float direction)
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

    private void AnimChange(float direction)
    {
        myAnimator.SetBool("isRun", direction != 0);
        myAnimator.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            countJump = 0;
        }
    }

}
