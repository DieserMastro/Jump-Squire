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
    private float minJumpPower;
    [SerializeField]
    private int jumpCounter;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private int moveDirection = 0;
    [SerializeField]
    private float jumpChargeVariable;
    [SerializeField]
    private Vector2 mousePos2D;
    

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
         
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // distance from camera to world plane
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos2D = new Vector2(worldPos.x, worldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 2.0f), Vector2.down);
        if (hit)
        {
            Debug.DrawRay(transform.position, Vector2.down * 3.0f, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * 3.0f, Color.red);
        }
        if (Input.GetMouseButton(0) && isGrounded)
        {
            ChargeJump();
        }
        if (Input.GetMouseButtonUp(0) && isGrounded)
        {
            PlayerJump(mousePos2D);
        }

        if (Input.GetKey(KeyCode.A) != Input.GetKey(KeyCode.D) && isGrounded)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1;
            }
            else 
            {
                moveDirection = 1;
            }
            PlayerMove(moveDirection);
        }
        else
        {
            moveDirection = 0;
        }
        
    }
    private void FixedUpdate()
    {
        
    }
    private void PlayerMove(int direction)
    {
        float move = direction * moveSpeed * Time.deltaTime;
        this.transform.Translate(move, 0, 0);
    }
    private void PlayerJump(Vector2 mousePos)
    {
        Debug.Log("Jump");
        Vector2 impulse = CalculateMouseDirection(mousePos) * jumpPower;
        this.rb.AddForce(impulse, ForceMode2D.Impulse);
        jumpPower = 0;
    }
    private void ChargeJump()
    {
        
        jumpPower += jumpChargeVariable * Time.deltaTime;
        if (jumpPower > maxJumpPower)
        {
            jumpPower = maxJumpPower;
        }
        else if (jumpPower < minJumpPower)
        {
            jumpPower = minJumpPower;
        }
        
    }
    private Vector2 CalculateMouseDirection(Vector2 mousePos)
    {
        Vector2 direction = new Vector2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
        direction.Normalize();
        Debug.Log(direction);
        return direction;
        
    }
}
