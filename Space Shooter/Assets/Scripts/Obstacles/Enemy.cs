using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // reference to sprite renderer
    private SpriteRenderer spriteRenderer;

    // materials for damage effect
    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private GameObject destroyEffect;
    
    // take damage
    [SerializeField] private int lives;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
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
