using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Overlay_Manager : MonoBehaviour
{

    [SerializeField]
    private Game_Manager gm;

    [SerializeField]
    private TMP_Text currentScoreText;

    [SerializeField]
    private int currentScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = gameObject.GetComponentInParent<Game_Manager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = gm.GetCurrentScore();
        currentScoreText.text = currentScore.ToString();
    }
}
