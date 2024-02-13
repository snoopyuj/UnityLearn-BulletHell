using UnityEngine;

namespace StephenHubbard
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField]
        private float lerpRatio = 0.1f;

        private Transform trans = null;
        private Camera cam = null;

        private void Awake()
        {
            trans = transform;
            cam = Camera.main;

            Invoke(nameof(EnableMono), 3f);
            enabled = false;
        }

        private void Update()
        {
            var oriPos = trans.position;
            var newPos = Vector3.Lerp(trans.position, cam.ScreenToWorldPoint(Input.mousePosition), lerpRatio);

            newPos.z = oriPos.z;

            trans.position = newPos;
        }

        private void EnableMono()
        {
            enabled = true;
        }
    }
}