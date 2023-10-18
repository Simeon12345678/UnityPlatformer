using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    float speed = 8;

    void Start()
    {
        
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movementX = new Vector2(moveX, 0);

        Vector2 movement = movementX;

        transform.Translate(movement * speed * Time.deltaTime);
    }
}
