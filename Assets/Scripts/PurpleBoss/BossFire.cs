using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : BossBaseState
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRate;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Shooting Points")]
    [SerializeField] private Transform[] firePoints;

    public override void RunState()
    {
        StartCoroutine(RunFireState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunFireState()
    {
        float shootTimer = 0;
        float fireStateTimer = 0;
        float fireStateExitTime = Random.Range(5f, 10f);
        Vector2 targetPos = new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));
        while (fireStateTimer < fireStateExitTime)
        {
            if (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                targetPos = new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootRate)
            {
                Shoot();
                shootTimer = 0;
            }

            yield return new WaitForEndOfFrame();
            fireStateTimer += Time.deltaTime;
        }

        //int randomPick = Random.Range(0, 2);
        //if (randomPick == 0)
        //{
        //    bossController.ChangeState(BossState.fire);
        //}
        //else
        //{
        //    bossController.ChangeState(BossState.special);
        //}

        bossController.ChangeState(BossState.special);
    }

    private void Shoot()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            Instantiate(bulletPrefab, firePoints[i].position, Quaternion.identity);
        }
    }
}
