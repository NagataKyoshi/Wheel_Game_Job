using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public enum PlayerState
    {
        Prepare,
        Playing
    }

    public PlayerState playerState;

    private void Awake()
    {
        playerState = PlayerState.Prepare;
    }

    private void Update()
    {
        if (playerState == PlayerState.Prepare)
        {
            startScreen.SetActive(true);
        }
    }
}
