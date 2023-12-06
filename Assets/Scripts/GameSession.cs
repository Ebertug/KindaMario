using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour{ 
    
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI  livesText;
    [SerializeField] TextMeshProUGUI  scoreText;
    public int score=0;



    void Awake(){
        
        int numGameSession= FindObjectsOfType<GameSession>().Length;
        if (numGameSession>1){

            Destroy(gameObject);
        }
        else{

            DontDestroyOnLoad(gameObject);
            
            
        }
    }


    void Start() {
        
        livesText.text=playerLives.ToString();
        scoreText.text=score.ToString();
    }
    
    public void ProcessPlayerDeath(){
        
        
        if (playerLives > 1){
            
            TakeLife();
        }
        else{
            ResetGameSession();
        }
    }
    public void AddToScore(int addScore){
        score=score+addScore;
        scoreText.text=score.ToString();
    }

    private void TakeLife(){
        
        playerLives = playerLives-1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text=playerLives.ToString();
    }

    private void ResetGameSession(){

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
