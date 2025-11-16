using System.Runtime;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Game_Manager gm;

    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int moveDirection = 0;

    [SerializeField]
    private Animator animator;
    private AudioSource audioSource;


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
    [SerializeField]
    private float rayOffSet;
    private RaycastHit2D hit;
    

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gm = gameObject.GetComponentInParent<Game_Manager>();
        audioSource = gameObject.GetComponent<AudioSource>();
         
    }

    void Start()
    {
        //animator.SetBool("isCharging", false);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // distance from camera to world plane
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos2D = new Vector2(worldPos.x, worldPos.y);
        if (Input.GetMouseButton(0) && isGrounded)
        {
            animator.SetBool("isCharging", true);
            ChargeJump();
        }
        if (Input.GetMouseButtonUp(0) && isGrounded)
        {
            animator.SetBool("isCharging", false);
            PlayerJump(mousePos2D);
        }

        /*if (Input.GetKey(KeyCode.A) != Input.GetKey(KeyCode.D) && isGrounded)
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
        }   */
    }

    void FixedUpdate()
    { 
        Vector2 origin = (Vector2)transform.position + new Vector2(0, rayOffSet);
        hit = Physics2D.Raycast(origin, Vector2.down, rayLength);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
            Debug.Log(hit.collider);
            if (hit.collider.CompareTag("Platform"))
            {
                isGrounded = true;
            }
            else if (hit.collider.CompareTag("Damage"))
            {
                AmDead();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green);
            isGrounded = false;
        }

    }
    /*private void PlayerMove(int direction)
    {
        float move = direction * moveSpeed * Time.deltaTime;
        this.transform.Translate(move, 0, 0);
    }*/
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag("Damage"))
                {
                    AmDead();
                }
        }
    }
    public void AmDead()
    {
        audioSource.Play();
        gm.PlayerDied();
    }
}
