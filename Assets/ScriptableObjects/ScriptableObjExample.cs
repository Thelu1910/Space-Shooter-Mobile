using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PowerUpSpawner", fileName = "Spawner")]

public class ScriptableObjExample : ScriptableObject
{
    public int spwnThreshold;            // range 0-100 , 0 = never spawn, 100 = always spawn, 30 = 70% chance
    public GameObject[] powerUp;

    public void SpawnPowerUp(Vector3 spwnPos)
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance > spwnThreshold)
        {
            int randomPowerUp = Random.Range(0, powerUp.Length);
            Instantiate(powerUp[randomPowerUp], spwnPos, Quaternion.identity);
        }
    }
}
