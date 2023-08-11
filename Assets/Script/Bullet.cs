using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Rigidbody2D rb2D;
    float xSpeed;
    PlayerController playerController;
    GameSession gameSession;
    void Awake()
    {

    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        xSpeed = playerController.transform.localScale.x * bulletSpeed;
    }
    void Update()
    {
        rb2D.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            gameSession = FindObjectOfType<GameSession>();
            gameSession.SetKillCount();
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
