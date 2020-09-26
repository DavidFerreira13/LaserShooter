using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        setUpSingleton();
    }

    private void setUpSingleton()
    {
        if(FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public int getScore()
    {
        return score;
    }
    
    public void addToScore(int scoreValue)
    {
        this.score += scoreValue;
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }
}
