using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    private ObjectPool<LaserBullet> referenceBullet;

    private void OnEnable()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetPool(ObjectPool<LaserBullet> pool)
    {
        referenceBullet = pool;
    }

    public void SetDirectionAndRotation()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
        if (gameObject.activeSelf)
            referenceBullet.Release(this);
        // Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        // Destroy(gameObject);
        if (gameObject.activeSelf)
            referenceBullet.Release(this);
    }
}
