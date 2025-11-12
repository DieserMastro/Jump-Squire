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
    private Canvas pauseMenu;
    private bool isPaused = false;
    
    void Start()
    {
        player_Controller = player.GetComponent<Player_Controller>();
        pauseMenu.gameObject.SetActive(false);
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
}
