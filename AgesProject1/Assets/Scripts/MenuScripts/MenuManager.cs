using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    Text playerOneText;
    Text playerTwoText;
    Text playerThreeText;
    Text playerFourText;

    int numberOfPlayersReady = 0;

    bool playerOneIsReady;
    bool playerTwoIsReady;
    bool playerThreeIsReady;
    bool playerFourIsReady;

    int numberOfReadyPlayers;

    bool enoughPlayersReady;

    string promptText;
    string readyText;

    Button startGameButton;

    private void Awake()
    {
        playerOneText = GameObject.Find("Player1Text").GetComponent<Text>();
        playerTwoText = GameObject.Find("Player2Text").GetComponent<Text>();
        playerThreeText = GameObject.Find("Player3Text").GetComponent<Text>();
        playerFourText = GameObject.Find("Player4Text").GetComponent<Text>();

        promptText = "Press A to Join";
        readyText = "Ready!";

        startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
    }

    // Use this for initialization
    void Start () {
        playerOneText.text = promptText;
        playerTwoText.text = promptText;
        playerThreeText.text = promptText;
        playerFourText.text = promptText;

        startGameButton.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        CheckForPlayerInput();
        UpdateReadyAmount();
	}

    private void CheckForPlayerInput()
    {
        if (Input.GetButtonDown("Jump1"))
        {
            if (!playerOneIsReady)
            {
                playerOneText.text = readyText;
                playerOneIsReady = true;
            }
            else
            {
                playerOneText.text = promptText;
                playerOneIsReady = false;
            }
        }
        if (Input.GetButtonDown("Jump2"))
        {
            if (!playerTwoIsReady)
            {
                playerTwoText.text = readyText;
                playerTwoIsReady = true;
            }
            else
            {
                playerTwoText.text = promptText;
                playerTwoIsReady = false;
            }
        }
        if (Input.GetButtonDown("Jump3"))
        {
            if (!playerThreeIsReady)
            {
                playerThreeText.text = readyText;
                playerThreeIsReady = true;
            }
            else
            {
                playerThreeText.text = promptText;
                playerThreeIsReady = false;
            }
        }
        if (Input.GetButtonDown("Jump4"))
        {
            if (!playerFourIsReady)
            {
                playerFourText.text = readyText;
                playerFourIsReady = true;
            }
            else
            {
                playerFourText.text = promptText;
                playerFourIsReady = false;
            }
        }

    }

    private void UpdateReadyAmount()
    {
        //Get the number of players that are ready
        numberOfReadyPlayers =
            Convert.ToInt32(playerOneIsReady) +
            Convert.ToInt32(playerTwoIsReady) +
            Convert.ToInt32(playerThreeIsReady) +
            Convert.ToInt32(playerFourIsReady);

        if (numberOfReadyPlayers == 4)
            enoughPlayersReady = true;
        else
            enoughPlayersReady = false;

        if (enoughPlayersReady)
            startGameButton.interactable = true;
        else
            startGameButton.interactable = false;
    }

}
