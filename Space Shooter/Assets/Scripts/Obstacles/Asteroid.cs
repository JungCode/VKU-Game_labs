using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    // components
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // sprites
    [SerializeField] private Sprite[] sprites;

    // materials for damage effect
    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private GameObject destroyEffect;
    
    // take damage
    [SerializeField] private int lives;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        float pushX = Random.Range(-0.5f, 0.5f);
        float pushY = Random.Range(-0.5f, 0);

        rb.linearVelocity = new Vector2(pushX, pushY);
    }

    // Update is called once per frame
    void Update()
    {
        float moveY = GameManager.Instance.worldspeed * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) {
            spriteRenderer.material = whiteMaterial;
            StartCoroutine("ResetMaterial");
            lives--;
            if(lives <= 0){
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

}
