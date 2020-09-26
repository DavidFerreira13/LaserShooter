using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartMenu(){
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().resetGame();
    }
    public void LoadInstructions()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadGameOver()
    {
        StartCoroutine(LoadWithDelayBeforeDeath());
    }

    private IEnumerator LoadWithDelayBeforeDeath()
    {
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}