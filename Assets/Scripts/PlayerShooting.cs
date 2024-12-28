using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LaserBullet laserBullet;
    [SerializeField] private float shootingInterval;
    private float intervalReset;

    [SerializeField] private AudioSource audioSource;

    [Header("Basic Attack")]
    [SerializeField] private Transform basicShootPoint;

    [Header("Upgrade Attack")]
    [SerializeField] private Transform leftCanonPoint;
    [SerializeField] private Transform secondLeftCanonPoint;
    [SerializeField] private Transform rightCanonPoint;
    [SerializeField] private Transform secondRightCanonPoint;

    [Header("Upgrade Rotation Attack")]
    [SerializeField] private Transform leftRotationCanonPoint;
    [SerializeField] private Transform rightRotationCanonPoint;

    private int upgradeLevel = 0;

    private ObjectPool<LaserBullet> laserBulletPool;

    private void Awake()
    {
        laserBulletPool = new ObjectPool<LaserBullet>(CreateObjPool, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyPoolObj, true, 10, 30);
    }

    private void Start()
    {
        intervalReset = shootingInterval;
    }

    private LaserBullet CreateObjPool()
    {
        LaserBullet bullet = Instantiate(laserBullet, transform.position, transform.rotation);
        bullet.SetPool(laserBulletPool);
        return bullet;
    }

    private void OnTakeBulletFromPool(LaserBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(LaserBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObj(LaserBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval <= 0 )
        {
            Shoot();
            shootingInterval = intervalReset;
        }
    }

    public void IncreaseUpgrade(int increaseAmount)
    {
        upgradeLevel += increaseAmount;
        if (upgradeLevel > 4)
            upgradeLevel = 4;
    }

    public void DecreaseUpgrade(int decreaseAmount)
    {
        upgradeLevel -= decreaseAmount;
        if (upgradeLevel < 0)
            upgradeLevel = 0;
    }

    private void Shoot()
    {
        audioSource.Play();

        switch (upgradeLevel)
        {
            case 0:
                laserBulletPool.Get().transform.position = basicShootPoint.position;
                break;
            case 1:
                laserBulletPool.Get().transform.position = leftCanonPoint.position;
                laserBulletPool.Get().transform.position = rightCanonPoint.position;
                break;
            case 2:
                laserBulletPool.Get().transform.position = basicShootPoint.position;

                laserBulletPool.Get().transform.position = leftCanonPoint.position;
                laserBulletPool.Get().transform.position = rightCanonPoint.position;
                break;
            case 3:
                laserBulletPool.Get().transform.position = basicShootPoint.position;

                laserBulletPool.Get().transform.position = leftCanonPoint.position;
                laserBulletPool.Get().transform.position = rightCanonPoint.position;
                laserBulletPool.Get().transform.position = secondLeftCanonPoint.position;
                laserBulletPool.Get().transform.position = secondRightCanonPoint.position;
                break;
            case 4:
                laserBulletPool.Get().transform.position = basicShootPoint.position;

                laserBulletPool.Get().transform.position = leftCanonPoint.position;
                laserBulletPool.Get().transform.position = rightCanonPoint.position;
                laserBulletPool.Get().transform.position = secondLeftCanonPoint.position;
                laserBulletPool.Get().transform.position = secondRightCanonPoint.position;

                LaserBullet bulletOne = laserBulletPool.Get();
                bulletOne.transform.position = leftRotationCanonPoint.position;
                bulletOne.transform.rotation = leftRotationCanonPoint.rotation;
                bulletOne.SetDirectionAndRotation();

                LaserBullet bulletTwo = laserBulletPool.Get();
                bulletTwo.transform.position = rightRotationCanonPoint.position;
                bulletTwo.transform.rotation = rightRotationCanonPoint.rotation;
                bulletTwo.SetDirectionAndRotation();
                break;
            default:
                Debug.Log("Something is wrong, we dont have this case!!!");
                break;
        }
    }
}
