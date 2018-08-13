using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResults : MonoBehaviour {

    [SerializeField] Text[] playerTextObjects;
    [SerializeField] string[] textResults = { "1st", "2nd", "3rd", "4th" };
    [SerializeField] int[] playerPlacements = { 0, 0, 0, 0 };

	// Use this for initialization
	void Start () {
        //UpdateTextValues();
    }
	
    void UpdateTextValues()
    {
        //Used to set which text object is to be updated
        int textToUpdateValue = 0;
        //Update each placement panel one by one
        foreach (int i in playerPlacements)
        {
            //The text object to be updated
            Text textToUpdate = playerTextObjects[textToUpdateValue];
            //Display the right text for each player placement
            switch (i)
            {
                case 1:
                    textToUpdate.text = textResults[0];
                    break;
                case 2:
                    textToUpdate.text = textResults[1];
                    break;
                case 3:
                    textToUpdate.text = textResults[2];
                    break;
                case 4:
                    textToUpdate.text = textResults[3];
                    break;
                default:
                    //For testing only
                    //Should never occur naturally
                    textToUpdate.text = "??";
                    break;
            }
            //Switch the text object to be updated
            textToUpdateValue++;
        }
    }
}
