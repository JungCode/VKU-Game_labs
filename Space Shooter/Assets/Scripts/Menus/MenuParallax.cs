using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float backgroundImageWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageWidth = sprite.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

       if(Mathf.Abs(transform.position.y) - backgroundImageWidth >= 0){
            transform.position = new Vector3(transform.position.x, 0);
       }
    }
}
