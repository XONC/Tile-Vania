using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float clambSpeed;
    [SerializeField] Vector2 deathPlay = new Vector2(10f, 10f);
    [SerializeField] GameObject buttet;
    [SerializeField] Transform gun;
    [SerializeField] int coin;

    Rigidbody2D rb2D;
    Vector2 moveInput;
    Animator animator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFooterCollider;
    GameSession gameSession;

    float gravityScaleAtStart;
    public bool isAlive = true;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFooterCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        gravityScaleAtStart = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; };
        Run();
        ClamLadding();
        FilpSprite();
        Die();
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dieing");
            rb2D.velocity = deathPlay;
            gameSession = FindObjectOfType<GameSession>();
            gameSession.ProcessPlayerDeath();
        }
    }
    void Run()
    {
        if (!isAlive) { return; };
        Vector2 playVector = new Vector2(moveInput.x * moveSpeed, rb2D.velocity.y);
        rb2D.velocity = playVector;
    }
    void ClamLadding()
    {
        if (!isAlive) { return; };
        if (!myFooterCollider.IsTouchingLayers(LayerMask.GetMask("Clamber")))
        {
            rb2D.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClamber", false);
            return;
        };

        Vector2 clambVector = new Vector2(rb2D.velocity.x, moveInput.y * clambSpeed);
        Debug.Log(clambVector);
        rb2D.velocity = clambVector;
        rb2D.gravityScale = 0;

        bool climberHasVerticalSpeed = Mathf.Abs(rb2D.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClamber", climberHasVerticalSpeed);
    }
    void FilpSprite()
    {
        if (!isAlive) { return; };
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb2D.velocity.x), 1, 1);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; };
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; };
        if (!myFooterCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            rb2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void OnFire(InputValue value)
    {
        if (!isAlive) { return; };
        Instantiate(buttet, gun.position, transform.rotation);

    }



}
