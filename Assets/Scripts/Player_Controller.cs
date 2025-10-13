using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] 
    protected Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower = 0;
    [SerializeField]
    private float maxJumpPower;
    [SerializeField]
    private int jumpCounter;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private Vector2 mousePos;
    [SerializeField]
    private int moveDirection = 0;
    [SerializeField]
    private float jumpChargeVariable;

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
                
                playerJump(mousePos);
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
    private void playerJump(Vector2 mousePos)
    {
        Vector2 impulseForce = mousePos * jumpPower;
        rb.AddForce(impulseForce, ForceMode2D.Impulse);
        jumpPower = 0;
    }
    private void chargeJump()
    {
        
        jumpPower += jumpChargeVariable;
        if(jumpPower > maxJumpPower)
        {
            jumpPower = maxJumpPower;
        }
    }
}
