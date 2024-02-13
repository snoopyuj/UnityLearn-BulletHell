using System.Collections;
using UnityEngine;

namespace StephenHubbard
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab = null;

        [SerializeField]
        private float bulletMoveSpeed = 5f;

        [SerializeField]
        private int burstCount = 1;

        [SerializeField]
        private int projectilesPerBurst = 1;

        [SerializeField]
        [Range(0f, 359f)]
        private float angleSpread = 0f;

        [SerializeField]
        private float startingDistance = 0.1f;

        [SerializeField]
        private float timeBetweenBursts = 0.5f;

        [SerializeField]
        private float restTime = 1f;

        [SerializeField]
        private bool isStagger = false;

        [SerializeField]
        private bool isOscillate = false;

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

            TargetConeOfInfluence(out float startAngle, out float curAngle, out float angleStep, out float endAngle);

            var timeBetweenProjectiles = (isStagger) ? timeBetweenBursts / projectilesPerBurst : 0f;

            for (var i = 0; i < burstCount; i++)
            {
                if (!isOscillate || i % 2 == 1)
                {
                    TargetConeOfInfluence(out startAngle, out curAngle, out angleStep, out endAngle);
                }
                else
                {
                    (curAngle, endAngle, startAngle) = (endAngle, startAngle, curAngle);
                    angleStep = -angleStep;
                }

                for (var j = 0; j < projectilesPerBurst; j++)
                {
                    var pos = FindBulletSpawnPos(curAngle);
                    var newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);

                    newBullet.transform.right = newBullet.transform.position - trans.position;

                    if (newBullet.TryGetComponent(out Projectile projectile))
                    {
                        projectile.SetMoveSpeed(bulletMoveSpeed);
                    }

                    curAngle += angleStep;

                    if (isStagger)
                    {
                        yield return new WaitForSeconds(timeBetweenProjectiles);
                    }
                }

                curAngle = startAngle;

                if (!isStagger)
                {
                    yield return new WaitForSeconds(timeBetweenBursts);
                }
            }

            yield return new WaitForSeconds(restTime);

            isShooting = false;
        }

        private void TargetConeOfInfluence(out float startAngle, out float curAngle, out float angleStep, out float endAngle)
        {
            var targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = trans.position.z;

            var targetDir = targetPos - trans.position;
            var targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            startAngle = targetAngle;
            endAngle = targetAngle;
            curAngle = targetAngle;
            angleStep = 0f;

            if (angleSpread != 0)
            {
                var halfAngleSpread = angleSpread / 2;

                angleStep = angleSpread / (projectilesPerBurst - 1);
                startAngle = targetAngle - halfAngleSpread;
                endAngle = targetAngle + halfAngleSpread;
                curAngle = startAngle;
            }
        }

        private Vector2 FindBulletSpawnPos(float curAngle)
        {
            var pos = trans.position;
            var x = pos.x + startingDistance * Mathf.Cos(curAngle * Mathf.Deg2Rad);
            var y = pos.y + startingDistance * Mathf.Sin(curAngle * Mathf.Deg2Rad);

            return new Vector2(x, y);
        }
    }
}