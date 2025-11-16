using Unity.VisualScripting;
using UnityEngine;

public class Platform_Controller : MonoBehaviour
{

    [SerializeField]
    private Game_Manager gm;

    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    int direction = 1;
    [SerializeField]
    private bool playerReached = false;

    public int index;
    Vector2 startPos;
    void Start()
    {
        gm = transform.parent.GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = direction * moveSpeed * Time.deltaTime;
        this.transform.Translate(move, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == null) return;
        if (collision.collider.CompareTag("Wall")) { direction *= -1; }
        if(collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.transform.parent = transform;
            if(!playerReached)
            {
                playerReached = true;
                gm.PlatformReached(index);
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform.parent;
        }
    }
    public bool GetPlayerReached()
    {
        return playerReached;
    }
    public void modifySpeed(float modifier)
    {
        this.moveSpeed *= modifier;
    }
    public void setIndex(int index)
    {
        this.index = index;
    }
}
