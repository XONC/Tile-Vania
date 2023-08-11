using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb2D;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D boxCollider;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {

    }
    void Update()
    {

        Vector2 moveVector = new Vector2(moveSpeed, rb2D.velocity.y);
        rb2D.velocity = moveVector;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FilpSprite();
    }

    void FilpSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb2D.velocity.x)), 1f);
    }
}
