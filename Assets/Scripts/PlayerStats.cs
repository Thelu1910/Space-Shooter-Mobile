using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private Image healthFill;

    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private Animator anim;
    private bool canPlayAnim = true;

    [SerializeField] private Shield shield;

    private PlayerShooting playerShooting;
    private int decreaseAmount = 1;

    private void Start()
    {
        health = maxHealth;
        healthFill.fillAmount = health/maxHealth;
        EndGameManager.endManager.gameOver = false;
        playerShooting = GetComponent<PlayerShooting>();
    }

    public void PlayerTakeDamage(float damage)
    {
        if (shield.protection)          // if the shield is active, the player will not take damage
            return;

        health -= damage;
        healthFill.fillAmount = health / maxHealth;    // update the health bar

        playerShooting.DecreaseUpgrade(decreaseAmount);     // decrease the upgrade level shooting of the player

        if (canPlayAnim)
        {
            anim.SetTrigger("Damage");
            StartCoroutine(AntiSpamAnimation());
        }

        if (health <= 0)     // if the player health is less than 0, the game is over, play explosion animation 
        {
            EndGameManager.endManager.gameOver = true;
            EndGameManager.endManager.StartResolveSequece();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void AddHealth(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthFill.fillAmount = health / maxHealth;
    }

    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnim = true;
    }
}
