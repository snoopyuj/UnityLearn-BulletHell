using UnityEngine;

namespace AlexanderZotov
{
    public class ShipMovement : MonoBehaviour
    {
        private float moveSpeed = 0f;
        private bool moveRight = false;

        private void Start()
        {
            moveSpeed = 2f;
            moveRight = true;
        }

        private void Update()
        {
            var trans = transform;

            if (trans.position.x > 7f)
            {
                moveRight = false;
            }
            else if (trans.position.x < -7f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                trans.position = new Vector2(
                    trans.position.x + moveSpeed * Time.deltaTime,
                    trans.position.y);
            }
            else
            {
                trans.position = new Vector2(
                    trans.position.x - moveSpeed * Time.deltaTime,
                    trans.position.y);
            }
        }
    }
}