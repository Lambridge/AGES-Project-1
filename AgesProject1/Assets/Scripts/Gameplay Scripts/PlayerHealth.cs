using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 3;
    private float currentHealth;

    public Slider sliderObject;
    public Image sliderFillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;


    private bool isPlayerDead;

    public int playerNumber;

    private void Start()
    {
        GameObject panel = GameObject.Find("HealthPanel" + playerNumber);
        sliderObject = panel.GetComponentInChildren<Slider>();
        sliderFillImage = sliderObject.GetComponentInChildren<Image>();
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
        // Adjust the value and colour of the slider.
        sliderObject.value = currentHealth;
        //sliderFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }

    void PlayerDeath()
    {
        isPlayerDead = true;
        gameObject.SetActive(false);
    }
}
