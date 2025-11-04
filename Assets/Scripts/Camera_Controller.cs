using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 5f, -10f);
    [SerializeField]
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Transform target;

    private void Update()
    {
        Vector2 newPos = new Vector2(0, target.position.y);
        Vector3 targetPosition = (Vector3)newPos + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
