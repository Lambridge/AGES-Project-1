using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPanel : MonoBehaviour {

    public int PlacementNumber { get; set; }

    Text textToUpdate;
    string[] placementText = { "??", "1st", "2nd", "3rd", "4th" };

	// Use this for initialization
	void Awake () {
        textToUpdate = gameObject.GetComponentInChildren<Text>();
	}

    private void Start()
    {
        UpdatePlacementText();
    }

    private void UpdatePlacementText()
    {
        switch (PlacementNumber)
        {
            case 1:
                textToUpdate.text = placementText[1];
                break;
            case 2:
                textToUpdate.text = placementText[2];
                break;
            case 3:
                textToUpdate.text = placementText[3];
                break;
            case 4:
                textToUpdate.text = placementText[4];
                break;
            default:
                textToUpdate.text = placementText[0];
                break;
        }
    }

}
