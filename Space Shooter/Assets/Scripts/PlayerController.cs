using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // components
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // materials for damage effect
    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material glowMaterial;

    [SerializeField] private float moveSpeed = 1f;

    // health
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    // destruction effect
    [SerializeField] private GameObject destroyEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        UIController.Instance.UpdateHealthSlider(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if(Time.timeScale > 0){
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
        }
        
        playerDirection = new Vector2(moveX, moveY).normalized;
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle")){
            TakeDamage(1);
        }
        if(collision.gameObject.CompareTag("Point")){
            GetPoint();
        }
    }
    
    private void GetPoint()
    {
        GameManager.Instance.AddPoint(1);
        
        spriteRenderer.material = glowMaterial;
        StartCoroutine(ResetMaterial());
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);

        spriteRenderer.material = whiteMaterial;
        StartCoroutine(ResetMaterial());
        
        if(health <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            GameManager.Instance.GameOver();
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }
}