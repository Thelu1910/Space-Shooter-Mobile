using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : MonoBehaviour
{
    protected Camera mainCam;

    protected float maxLeft;
    protected float maxRight;
    protected float maxDown;
    protected float maxUp;

    protected BossController bossController;

    private void Awake()
    {
        mainCam = Camera.main;
        bossController = GetComponent<BossController>();
    }

    protected virtual void Start()
    {
        maxLeft = mainCam.ViewportToWorldPoint(new Vector2(0.3f, 0)).x;
        maxRight = mainCam.ViewportToWorldPoint(new Vector2(0.7f, 0)).x;
        maxDown = mainCam.ViewportToWorldPoint(new Vector2(0, 0.6f)).y;
        maxUp = mainCam.ViewportToWorldPoint(new Vector2(0, 0.9f)).y;
    }

    public virtual void RunState()
    {
        
    }

    public virtual void StopState()
    {
        StopAllCoroutines();
    }
}
