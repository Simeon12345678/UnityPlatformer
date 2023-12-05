using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 8;

    [SerializeField]
    Transform ledgeCheckRight;

    [SerializeField]
    Transform ledgeCheckLeft;

    [SerializeField]
    LayerMask ledgeLayer;

    [SerializeField]
    float groundRadius = 0.1f;

    [SerializeField]
    int health = 100;

    [SerializeField]
    SpriteRenderer spriteComponant;

    Vector2 movement = new Vector2(1, 0);

    void Start()
    {

    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        // i gave up on trying to transform the position of the ledge gameobject while making it work and instead just made one for right and left.

        transform.Translate(movement * speed * Time.deltaTime);

        bool isGroundedRight = Physics2D.OverlapCircle(ledgeCheckRight.position, groundRadius, ledgeLayer);
        bool isGroundedLeft = Physics2D.OverlapCircle(ledgeCheckLeft.position, groundRadius, ledgeLayer);

        if (!isGroundedRight)
        {
            spriteComponant.flipX = true;
            movement.x = -movement.x;
        }
        if (!isGroundedLeft)
        {
            spriteComponant.flipX = false;
            movement.x = -movement.x;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(ledgeCheckRight.position, MakeGroundCheckSize());
        Gizmos.DrawWireCube(ledgeCheckLeft.position, MakeGroundCheckSize());
    }

    private Vector3 MakeGroundCheckSize() => new Vector3(2.5f, groundRadius);
}
