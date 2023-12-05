using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Animations;

public class playerController : MonoBehaviour
{
    [SerializeField]
    float speed = 8;

    [SerializeField]
    float jumpForce = 100;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundRadius = 0.1f;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    SpriteRenderer spriteComponant;

    [SerializeField]
    Animator animator;

    bool mayJump = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movementX = new Vector2(moveX, 0);

        Vector2 movement = movementX;

        transform.Translate(movement * speed * Time.deltaTime);

        // animator.SetFloat("Horizontal", moveX);


        if (moveX > 0f)
        {
            animator.Play("AnimationForward");
            spriteComponant.flipX = false;

        }
        else if (moveX < 0f)
        {
            spriteComponant.flipX = true;
            animator.Play("AnimationForward");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.Play("Attack");
        }
        else
        {

            animator.Play("AnimationIdling");
        };
        

        // bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, MakeGroundCheckSize(), 0, groundLayer);

        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && isGrounded == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 jump = Vector2.up * jumpForce;

            rb.AddForce(jump);

            mayJump = false;
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            mayJump = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, MakeGroundCheckSize());
    }

    private Vector3 MakeGroundCheckSize() => new Vector3(2.5f, groundRadius);
}
