using UnityEngine;

namespace Maieron
{
    public class BulletHellSpawner : MonoBehaviour
    {
        [SerializeField]
        private int numOfColumns = 1;

        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private Sprite texture = null;

        [SerializeField]
        private Color color = Color.white;

        [SerializeField]
        private float lifeTime = 5f;

        [SerializeField]
        private float fireRate = 0.5f;

        [SerializeField]
        private float size = 0.5f;

        [SerializeField]
        private Material mat = null;

        [SerializeField]
        private float spinSpeed = 0f;

        private float time = 0f;

        private void Awake()
        {
            Summon();
        }

        private void FixedUpdate()
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, time * spinSpeed);
        }

        private void Summon()
        {
            var angle = 360f / numOfColumns;
            var parentTrans = transform;

            for (var i = 0; i < numOfColumns; i++)
            {
                var go = new GameObject("Particle System");
                var trans = go.transform;

                trans.Rotate(angle * i, 90, 0);
                trans.parent = parentTrans;
                trans.position = parentTrans.position;

                var ps = go.AddComponent<ParticleSystem>();

                ps.Stop();
                go.GetComponent<ParticleSystemRenderer>().material = mat;

                var mainModule = ps.main;
                mainModule.startSpeed = speed;
                mainModule.maxParticles = 10000;
                mainModule.duration = 0f;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

                var emission = ps.emission;
                emission.enabled = false;

                var shape = ps.shape;
                shape.enabled = true;
                shape.shapeType = ParticleSystemShapeType.Sprite;
                shape.sprite = null;

                var textureAni = ps.textureSheetAnimation;
                textureAni.enabled = true;
                textureAni.mode = ParticleSystemAnimationMode.Sprites;
                textureAni.AddSprite(texture);
            }

            InvokeRepeating(nameof(DoEmit), 0f, fireRate);
        }

        private void DoEmit()
        {
            foreach (var child in transform)
            {
                var ps = ((Transform)child).GetComponent<ParticleSystem>();
                var emitParams = new ParticleSystem.EmitParams
                {
                    startSize = size,
                    startColor = color,
                    startLifetime = lifeTime,
                };

                ps.Emit(emitParams, 1);
            }
        }
    }
}
