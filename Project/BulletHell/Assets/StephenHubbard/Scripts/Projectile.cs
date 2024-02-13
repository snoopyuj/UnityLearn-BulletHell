using UnityEngine;

namespace StephenHubbard
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float lifeTime = 5f;

        private float moveSpeed = 0f;
        private Transform trans = null;

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        private void Awake()
        {
            trans = transform;
        }

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            MoveProjectile();
        }

        private void MoveProjectile()
        {
            trans.position += moveSpeed * Time.deltaTime * trans.right;
        }
    }
}