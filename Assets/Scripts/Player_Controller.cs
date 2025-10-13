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
    private bool isGrounded = true;
    [SerializeField]
    private Vector2 mousePos;
    [SerializeField]
    private int moveDirection = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            chargeJump();
            if (Input.GetKeyUp(KeyCode.Space))
            {
                
                playerJump(jumpPower, mousePos);
            }
        }
        if (Input.GetKey(KeyCode.A) != Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1;
            }
            else 
            {
                moveDirection = 1;
            }
            playerMove(moveDirection);
        }
        else
        {
            moveDirection = 0;
        }
        
    }
    private void playerMove(int direction)
    {
        float move = direction * moveSpeed * Time.deltaTime;
        this.transform.Translate(move, 0, 0);
    }
    private void playerJump(float jumpPower, Vector2 mousePos)
    {

    }
    private float chargeJump()
    {
        return 0;
    }
}
