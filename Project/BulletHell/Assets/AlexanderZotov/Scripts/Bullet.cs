using UnityEngine;

namespace AlexanderZotov
{
    public class Bullet : MonoBehaviour
    {
        private Vector2 moveDirection = Vector2.zero;
        private float moveSpeed = 0f;

        public void SetMoveDirection(Vector2 dir)
        {
            moveDirection = dir;
        }

        private void OnEnable()
        {
            Invoke(nameof(Destroy), 3f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Start()
        {
            moveSpeed = 5f;
        }

        private void Update()
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}