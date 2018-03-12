using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 3;
    private float currentHealth;

    public Slider sliderObject;
   
    private bool isPlayerDead;

    public int playerNumber;

    private void Start()
    {
        GameObject panel = GameObject.Find("HealthPanel" + playerNumber);
        sliderObject = panel.GetComponentInChildren<Slider>();
    }

    // Use this for initialization
    void OnEnable()
    {
        currentHealth = startingHealth;
        isPlayerDead = false;
    }
	
	public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

        //Kill the player if they reach zero health
        if(currentHealth <= 0 && !isPlayerDead)
        {
            PlayerDeath();
        }
    }

    private void UpdateHealthUI()
    {
        // Adjust the value of the slider.
        sliderObject.value = currentHealth;
    }

    void PlayerDeath()
    {
        isPlayerDead = true;
        gameObject.SetActive(false);
    }
}
