using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecial : BossBaseState
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTimeShooting;
    [SerializeField] private GameObject specialBullet;
    [SerializeField] private Transform shootingPoint;
    private Vector2 targetPos;

    protected override void Start()
    {
        targetPos = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.9f));
    }

    public override void RunState()
    {
        StartCoroutine(RunSpecialState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunSpecialState() {
        while (Vector2.Distance(transform.position, targetPos) > 0.1f) { 
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Instantiate(specialBullet, shootingPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(waitTimeShooting);
        bossController.ChangeState(BossState.fire);
    }
}
