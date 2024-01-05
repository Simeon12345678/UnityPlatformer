using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField]
    GameObject needlePrefab;

    [SerializeField]
    Transform ledgeCheckRight;

    [SerializeField]
    Transform ledgeCheckLeft;

    [SerializeField]
    Transform AggroLocation;

    [SerializeField]
    float aggroRange = 10f;

    [SerializeField]
    float speed = 8;

    [SerializeField]
    float groundRadius = 0.1f;

    [SerializeField]
    float timeBetweenShots = 2f;
    float timeSinceLastShot = 0;

    [SerializeField]
    int health = 100;

    [SerializeField]
    SpriteRenderer spriteComponant;

    [SerializeField]
    LayerMask player;

    [SerializeField]
    LayerMask ledgeLayer;

    Vector2 movement = new Vector2(1, 0);


    void Start()
    {

    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IseeYou();
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

    private void IseeYou()
    {
        Collider2D[] lineOfSight = Physics2D.OverlapCircleAll(AggroLocation.position, aggroRange, player);

        foreach (Collider2D player in lineOfSight)
        {
            movement = Vector2.zero;
            Shoot();
        }
    }

    private void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot > timeBetweenShots)
        {
            timeSinceLastShot = 0;
            Instantiate(needlePrefab, AggroLocation.transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(ledgeCheckRight.position, MakeGroundCheckSize());
        Gizmos.DrawWireCube(ledgeCheckLeft.position, MakeGroundCheckSize());
        Gizmos.DrawWireSphere(AggroLocation.position, aggroRange);
    }


    private Vector3 MakeGroundCheckSize() => new Vector3(2.5f, groundRadius);
}
