using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.2f;
    private Vector3 startPosition;
    private float objectHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the planet downward every frame
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Convert world position to viewport (0–1)
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // Calculate offset based on object height and camera size
        float viewportOffset = objectHeight / (Camera.main.orthographicSize * 2f);

        // If the planet is fully below the screen -> respawn
        if (viewPos.y < -viewportOffset)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Get camera top and horizontal range
        Camera cam = Camera.main;
        float cameraTop = cam.transform.position.y + cam.orthographicSize;
        float cameraWidth = cam.orthographicSize * cam.aspect;

        // Randomize X position within camera width range
        float randomX = Random.Range(-cameraWidth, cameraWidth);

        // Move planet above the top of the screen (no visible jump)
        transform.position = new Vector3(randomX, cameraTop + objectHeight, 0);
    }
}
