﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] float startGameDelay = 3f;
    [SerializeField] float goMessageDelay = 1f;
    [SerializeField] float endGameDelay = 3f;
    //public CameraControl cameraControl

    [SerializeField] Text gameMessageText;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] PlayerManager[] allPlayers;

    private WaitForSeconds startWaitTime;
    private WaitForSeconds goMessageDisplayTime;
    private WaitForSeconds endWaitTime;
    private PlayerManager gameWinner;

    // Use this for initialization
    void Start () {
        startWaitTime = new WaitForSeconds(startGameDelay);
        goMessageDisplayTime = new WaitForSeconds(goMessageDelay);
        endWaitTime = new WaitForSeconds(endGameDelay);

        SpawnAllPlayers();
        //SetCameraTargets();
    }

    private void SpawnAllPlayers()
    {
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].playerInstance =
                Instantiate(playerPrefab, allPlayers[i].spawnPoint.position, allPlayers[i].spawnPoint.rotation) as GameObject;
            allPlayers[i].playerNumber = i + 1;
            allPlayers[i].Setup();
        }
    }

    //private void SetCameraTargets()
    //{
    //    Transform[] targets = new Transform[allPlayers.Length];

    //    for (int i = 0; i < targets.Length; i++)
    //    {
    //        targets[i] = allPlayers[i].playerInstance.transform;
    //    }

    //    cameraControl.m_Targets = targets;
    //}

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStarting());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator GameStarting()
    {
        ResetAllPlayers();
        DisableAllPlayers();

        //m_CameraControl.SetStartPositionAndSize();

        gameMessageText.text = "Ready?";

        yield return startWaitTime;

        gameMessageText.text = "Fight!";
    }


    private IEnumerator GamePlaying()
    {
        EnableAllPlayers();

        gameMessageText.text = string.Empty;

        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }


    private IEnumerator GameEnding()
    {
        DisableAllPlayers();
        gameWinner = null;
        gameWinner = GetGameWinner();

        string message = EndMessage();
        gameMessageText.text = message;

        yield return endWaitTime;
    }


    private bool OnePlayerLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (allPlayers[i].playerInstance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }


    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (allPlayers[i].playerInstance.activeSelf)
                return allPlayers[i];
        }

        return null;
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if (gameWinner != null)
            message = gameWinner.coloredPlayerText + " WINS!";

        return message;
    }


    private void ResetAllPlayers()
    {
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].Reset();
        }
    }


    private void EnableAllPlayers()
    {
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].EnableControl();
        }
    }


    private void DisableAllPlayers()
    {
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].DisableControl();
        }
    }

}
