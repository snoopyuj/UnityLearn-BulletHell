using UnityEngine;

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
    }

    private void Update()
    {
        var oriPos = trans.position;
        var newPos = Vector3.Lerp(trans.position, cam.ScreenToWorldPoint(Input.mousePosition), lerpRatio);

        newPos.z = oriPos.z;

        trans.position = newPos;
    }
}