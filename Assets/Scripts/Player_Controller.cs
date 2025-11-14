using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] 
    protected Rigidbody2D rb;   
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int moveDirection = 0;

    //Variables for jumping stuff
    [SerializeField]
    private float jumpPower = 0;
    [SerializeField]
    private float maxJumpPower;
    [SerializeField]
    private float minJumpPower;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private float jumpChargeVariable;
    [SerializeField]
    private Vector2 mousePos2D;
    //for double jump (maybe)
    [SerializeField]
    private int jumpCounter;

    //Raycast stuff
    [SerializeField]
    private float rayLength = 0.5f;
    private RaycastHit2D hit;
    

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
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Platform"))
            {
                this.isGrounded = true;
            }
        }
    }*/

    void FixedUpdate()
    { 
        Vector2 origin = (Vector2)transform.position + new Vector2(0, -1.1f);
        hit = Physics2D.Raycast(origin, Vector2.down, rayLength);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
            if (hit.collider.CompareTag("Platform"))
            {
                isGrounded = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green);
            isGrounded = false;
        }

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
