using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerManager
{
    public Color playerColor;
    public Transform spawnPoint;
    [HideInInspector] public int playerNumber;
    [HideInInspector] public string coloredPlayerText;
    [HideInInspector] public GameObject playerInstance;

    private PlayerScript playerMovement;
    private GrabAndThrowScript playerThrowingObject;
    private PlayerHealth playerHealth;
    [SerializeField] GameObject canvasObject;

    public void Setup()
    {
        playerMovement = playerInstance.GetComponent<PlayerScript>();
        playerThrowingObject = playerInstance.GetComponentInChildren<GrabAndThrowScript>();
        playerHealth = playerInstance.GetComponent<PlayerHealth>();

        playerMovement.playerNumber = playerNumber;
        playerThrowingObject.playerNumber = playerNumber;
        playerHealth.playerNumber = playerNumber;

        coloredPlayerText =
            "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNumber + "</color>";

        MeshRenderer[] renderers = playerInstance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public void DisableControl()
    {
        playerMovement.enabled = false;
        playerThrowingObject.enabled = false;

        canvasObject.SetActive(false);
    }


    public void EnableControl()
    {
        playerMovement.enabled = true;
        playerThrowingObject.enabled = true;

        canvasObject.SetActive(true);
    }

    public void Reset()
    {
        playerInstance.transform.position = spawnPoint.position;
        playerInstance.transform.rotation = spawnPoint.rotation;

        playerInstance.SetActive(false);
        playerInstance.SetActive(true);
    }
}

