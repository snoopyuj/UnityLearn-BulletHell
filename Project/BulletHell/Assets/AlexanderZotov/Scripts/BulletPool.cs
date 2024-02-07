using System.Collections.Generic;
using UnityEngine;

namespace AlexanderZotov
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool bulletPoolInstance = null;

        [SerializeField]
        private GameObject pooledBullet = null;

        private bool notEnoughBulletsInPool = true;
        private List<GameObject> bullets = null;

        private void Awake()
        {
            bulletPoolInstance = this;
        }

        private void Start()
        {
            bullets = new();
        }

        public GameObject GetBullet()
        {
            if (bullets.Count > 0)
            {
                foreach (var bullet in bullets)
                {
                    if (!bullet.activeInHierarchy)
                    {
                        return bullet;
                    }
                }
            }

            if (notEnoughBulletsInPool)
            {
                var bul = Instantiate(pooledBullet);

                bul.SetActive(false);
                bullets.Add(bul);

                return bul;
            }

            return null;
        }
    }
}