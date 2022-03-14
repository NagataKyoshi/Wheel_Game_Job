using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject finishScreen;
    public static GameManager inst;
    public enum PlayerState
    {
        Prepare,
        Playing,
        Finish
    }

    public PlayerState playerState;

    private void Awake()
    {
        playerState = PlayerState.Prepare;
        inst = this;
    }

    private void Update()
    {
        if (playerState == PlayerState.Prepare)
        {
            startScreen.SetActive(true);
        }
        else
        {
            startScreen.SetActive(false);
        }
        if (playerState == PlayerState.Finish)
        {
            finishScreen.SetActive(true);
        }
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
