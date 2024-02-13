using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private float bulletMoveSpeed = 5f;

    [SerializeField]
    private int burstCount = 1;

    [SerializeField]
    private float timeBetweenBursts = 0.5f;

    [SerializeField]
    private float restTime = 1f;

    private Transform trans = null;
    private Camera cam = null;
    private bool isShooting = false;

    private void Awake()
    {
        trans = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        if (isShooting)
        {
            return;
        }

        StartCoroutine(ShootRoutine());
    }   

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        for (var i = 0; i < burstCount; i++)
        {
            var targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = trans.position.z;

            var targetDir = targetPos - trans.position;
            var newBullet = Instantiate(bulletPrefab, trans.position, Quaternion.identity);

            newBullet.transform.right = targetDir;

            if (newBullet.TryGetComponent(out Projectile projectile))
            {
                projectile.SetMoveSpeed(bulletMoveSpeed);
            }

            yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(restTime);

        isShooting = false;
    }
}