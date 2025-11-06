using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float worldspeed;
    public int score;
    
    void Awake () {
        if (Instance != null){
            Destroy (gameObject);
        } else
        {
            Instance = this;
        }
    }
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }
    
    public void Pause(){
        if(UIController.Instance.pausePanel.activeSelf== false){
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    public void QuitGame(){
        Application.Quit();
    }
    
    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    
    public void GameOver(){
        PlayerPrefs.SetInt("Score", score);
        StartCoroutine(ShowGameOverScreen());
    }

    public void AddPoint(int point){
        score += point;
        UIController.Instance.UpdateScore(score);
    }
   
    IEnumerator ShowGameOverScreen ()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
