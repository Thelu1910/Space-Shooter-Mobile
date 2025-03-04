using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Camera mainCam;
    private float maxLeft;
    private float maxRight;
    private float yPos;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemy;

    private float enemyTimer;
    [Space(15)]
    [SerializeField] private float enemySpawnTime;

    [Header("BOSS")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private WinCondition winCondition;

    void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    private void Update()
    {
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= enemySpawnTime)
        {
            int randomPick = Random.Range(0, enemy.Length);
            Instantiate(enemy[randomPick], new Vector3(Random.Range(maxLeft, maxRight), yPos, 0), Quaternion.identity);
            enemyTimer = 0;
        }
    }

    private IEnumerator SetBoundaries()
    {
        // give time for cinemachine to adjust camera
        // yield return new WaitForEndOfFrame();        //  wait end frame

        yield return new WaitForSeconds(0.4f);         // use this for old phone

        maxLeft = mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
        yPos = mainCam.ViewportToWorldPoint(new Vector2(0, 1.1f)).y;
    }

    private void OnDisable()
    {
        if (winCondition.canSpawnBoss == false)
            return;

        if (bossPrefab != null)
        {
            Vector2 spawnBossPos = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
            Instantiate(bossPrefab, spawnBossPos, Quaternion.identity);
        }
    }
}
