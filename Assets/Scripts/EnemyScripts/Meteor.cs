using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class Meteor : Enemy
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float speed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private ScriptableObjExample powerUpSpawner;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = Vector3.down * speed;
    }

    private void Update()
    {
        transform.Rotate(0, 0 , rotateSpeed * Time.deltaTime);
    }

    public override void HurtSequence()
    {
        // do st
    }

    public override void DeathSequence()
    {
        base.DeathSequence(); // call all code from parent
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        if (powerUpSpawner != null )
            powerUpSpawner.SpawnPowerUp(transform.position);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.CompareTag("Player"))
        {
            PlayerStats player = otherColl.GetComponent<PlayerStats>();
            player.PlayerTakeDamage(damage);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
