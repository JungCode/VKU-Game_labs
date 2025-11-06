using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    [SerializeField] private TMP_Text scoreToDisplay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       int lastScore = PlayerPrefs.GetInt("Score", 0);
        setScoreToDisplay(lastScore); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setScoreToDisplay(int score){
        scoreToDisplay.text = "Score: " + score.ToString();
    }
}
