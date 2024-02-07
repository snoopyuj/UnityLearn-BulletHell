using UnityEngine;

namespace AlexanderZotov
{
    public class FireSpiral : MonoBehaviour
    {
        private float angle = 0f;
        private Vector2 bulletMoveDirection = Vector2.zero;

        private void Start()
        {
            InvokeRepeating(nameof(Fire), 0f, 0.1f);
        }

        private void Fire()
        {
            var trans = transform;
            var bulDirX = trans.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            var bulDirY = trans.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            var bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            var bulDir = (bulMoveVector - trans.position).normalized;
            var bul = BulletPool.bulletPoolInstance.GetBullet();

            bul.transform.SetPositionAndRotation(trans.position, trans.rotation);
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += 10f;

            if (angle >= 360f)
            {
                angle = 0f;
            }
        }
    }
}