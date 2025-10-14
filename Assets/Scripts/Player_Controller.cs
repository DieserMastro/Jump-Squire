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
    private GameObject jumpRange;

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
        

        if (Input.GetMouseButton(0) && isGrounded)
        {
            chargeJump();
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("space GetKeyUp");
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z; // distance from camera to world plane
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);
            playerJump(worldPos2D);
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
        Debug.Log("Jump");
        Vector2 impulse = calculateImpulseDirection(mousePos) * jumpPower;
        this.rb.AddForce(impulse, ForceMode2D.Impulse);
        jumpPower = 0;
    }
    private void chargeJump()
    {
        
        jumpPower += jumpChargeVariable;
        if (jumpPower > maxJumpPower)
        {
            jumpPower = maxJumpPower;
        }
        else if (jumpPower < minJumpPower)
        {
            jumpPower = minJumpPower;
        }
        
    }
    private Vector2 calculateImpulseDirection(Vector2 mousePos)
    {
        Vector2 direction = new Vector2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
        direction.Normalize();
        Debug.Log(direction);
        return direction;
        
    }
}
