using UnityEngine;

public class Point : MonoBehaviour
{
    // reference to sprite renderer
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
