using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] 
    protected Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private int jumpCounter;
    [SerializeField]
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
