using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Border_Controller : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Transform target;
    void Update()
    { 
        transform.position = new Vector3(offset.x, target.position.y, offset.z);

    }
    /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
        {
            Vector2 linearVelocity = collision.rigidbody.linearVelocity;
            Debug.Log(collision);
            linearVelocity.x = -linearVelocity.x;
            collision.rigidbody.linearVelocity = linearVelocity;
        }
    }*/
}
