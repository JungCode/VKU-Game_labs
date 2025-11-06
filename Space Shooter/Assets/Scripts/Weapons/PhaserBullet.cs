using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0f, PhaserWeapon.Instance.speed * Time.deltaTime, 0f);
        if (transform.position.y > 9)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle")){
            Destroy(gameObject);
        }
    }
}
