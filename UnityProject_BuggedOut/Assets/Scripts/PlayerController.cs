using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    public float speedBase = 0.1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.MovePosition(rb.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedBase);
    }

}
