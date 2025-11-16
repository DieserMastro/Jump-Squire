using Unity.VisualScripting;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{

    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private GameObject quitButton;

    public void PlayGame()
    {
        if (playButton == null)
        {
            return;
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("devLevel");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();
    }
}
