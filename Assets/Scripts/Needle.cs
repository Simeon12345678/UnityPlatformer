using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField]
    float speed = 12;

    Vector2 movement = new Vector2(1, 0);

    float time = 0;

    void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);

        time = Time.deltaTime;
        if (time >= 5f)
        {
            print("dshdhfd");
            Destroy(this.gameObject);
            time = 0;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            print("made it here");
            Destroy(this.gameObject);
        }
    }
}
