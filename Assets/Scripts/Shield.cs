using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int hitsToDestroy = 3;
    public bool protection = false;
    [SerializeField] private GameObject[] shieldBase;

    private void OnEnable()
    {
        hitsToDestroy = 3;
        for (int i = 0; i < shieldBase.Length; i++)      // activate all the shield 
        {
            shieldBase[i].SetActive(true);
        }

        protection = true;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < shieldBase.Length; i++)
        {
            shieldBase[i].SetActive(i < hitsToDestroy);
        }
    }


    private void DamageShield()
    {
        hitsToDestroy -= 1;
        if (hitsToDestroy <= 0)
        {
            hitsToDestroy = 0;
            protection = false;
            gameObject.SetActive(false);
        }
        UpdateUI();
    }

    public void RepairShield()
    {
        hitsToDestroy = 3;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))      // if the enemy is destroyed, the shield will be damaged
        {
            if (collision.CompareTag("Boss"))        // if the enemy is a boss, the shield will be destroyed
            {
                hitsToDestroy = 0;
                DamageShield();
                return;
            }
            enemy.TakeDamage(1000);      // make sure the enemy is destroyed
            DamageShield();
        }
        else 
        { 
            Destroy(collision.gameObject);
            DamageShield();
        }
    }
}
