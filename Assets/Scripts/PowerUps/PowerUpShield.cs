using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] private AudioClip powerUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShieldActivator playerShieldActivator = collision.GetComponent<PlayerShieldActivator>();
            playerShieldActivator.ActivateShield();

            AudioSource.PlayClipAtPoint(powerUpSound, transform.position, 1f);
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
