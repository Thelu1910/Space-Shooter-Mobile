using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private float timer;
    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawner;
    [SerializeField] private bool hasBoss;
    public bool canSpawnBoss = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (EndGameManager.endManager.gameOver == true)
            return;

        timer += Time.deltaTime;
        if (timer >= possibleWinTime)
        {
            if (hasBoss == false)   // if there is no boss, the game will end
            {
                EndGameManager.endManager.StartResolveSequece();
            }
            else                    // if there is a boss, the boss will be spawned
            {
                canSpawnBoss = true; 
            }

            for (int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(false);
            }

            gameObject.SetActive(false);
            // create a function that will check if the player can survived the last spawned enemy/meteor
                // win or lose screen
                    // GAME MANAGER
        }
    }
}
