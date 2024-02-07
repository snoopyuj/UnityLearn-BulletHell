using UnityEngine;

namespace AlexanderZotov
{
    public class FireBullets : MonoBehaviour
    {
        [SerializeField]
        private int bulletsAmount = 10;

        [SerializeField]
        private float startAngle = 90f;

        [SerializeField]
        private float endAngle = 270f;

        private Vector2 bulletMoveDirection = Vector2.zero;

        private void Start()
        {
            InvokeRepeating(nameof(Fire), 0f, 2f);
        }

        private void Fire()
        {
            var angleStep = (endAngle - startAngle) / bulletsAmount;
            var angle = startAngle;
            var trans = transform;

            for (var i = 0; i < bulletsAmount + 1; ++i)
            {
                var bulDirX = trans.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                var bulDirY = trans.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
                var bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                var bulDir = (bulMoveVector - trans.position).normalized;
                var bul = BulletPool.bulletPoolInstance.GetBullet();

                bul.transform.SetPositionAndRotation(trans.position, trans.rotation);
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

                angle += angleStep;
            }
        }
    }
}