using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player_Controller player_Controller;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject platform_Spawner;
    [SerializeField]
    private Floor_Controller fc;

    [SerializeField]
    private Canvas pauseMenu;
    [SerializeField]
    private Canvas deathScreen;
    [SerializeField]
    private TMP_Text deathScreenScore;
    [SerializeField]
    private TMP_Text deathScreenHighScore;
    [SerializeField]
    private Canvas gameOverlay;


    private bool isPaused = false;
    private bool floorIsDeadly = false;

    [SerializeField] private int highestPlatformReached = 0;
    [SerializeField] private int allTimeHS;



    void Start()
    {
        floorIsDeadly = false;
        
        player_Controller = player.GetComponent<Player_Controller>();
        pauseMenu.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(false);
        gameOverlay.gameObject.SetActive(true);
        allTimeHS = PlayerPrefs.GetInt("Highest Score");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
        

    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        player_Controller.enabled = false;
        pauseMenu.gameObject.SetActive(true);
    }
    public void UnpauseGame()
    {
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        player_Controller.enabled = true;
    }
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
    public void PlayerDied()
    {
        
        Time.timeScale = 0.05f;
        deathScreen.gameObject.SetActive(true);
        player_Controller.enabled = false;

        if (PlayerPrefs.GetInt("Highest Score") < highestPlatformReached)
        {
            PlayerPrefs.SetInt("Highest Score", highestPlatformReached);
        }
        deathScreenScore.text = "ur Score: \n" + highestPlatformReached;
        deathScreenHighScore.text = "High Score: \n" + PlayerPrefs.GetInt("Highest Score");

        gameOverlay.gameObject.SetActive(false);


    }

    public void PlatformReached(int index)
    {
        if(index > highestPlatformReached)
        {
            highestPlatformReached = index;
        }
        if(highestPlatformReached > 4)
        {
            SetFloorIsDanger();
        }
    }

    public int GetCurrentScore()
    {
        return highestPlatformReached;
    }
    public void SetFloorIsDanger()
    {
        floorIsDeadly = true;
        fc.SetIsLava();
    }
}
