using UnityEngine;

public class PhaserWeapon : MonoBehaviour
{
    public static PhaserWeapon Instance;

    [SerializeField] private GameObject prefab;

    [SerializeField] public float speed = 10f;
    [SerializeField] public int damage = 1;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        Vector3 spawnPos = transform.position + transform.up * 0.1f;
        Instantiate(prefab, spawnPos, transform.rotation);
    }
}
