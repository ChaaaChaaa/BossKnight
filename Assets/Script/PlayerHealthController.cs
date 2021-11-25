using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController singletonPlayerHealthController;
    public int currentHealth;
    public int maxHealth;

    [SerializeField] private _Slider hpSlider;

    public float damageInvincLength = 1f;
    private float invincCount;

    private void Awake()
    {
        singletonPlayerHealthController = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        hpSlider.SetSlider(maxHealth, currentHealth);
        // UIController.singletonUIController.healthSlider.maxValue = maxHealth;
        // UIController.singletonUIController.healthSlider.value = currentHealth;
        // UIController.singletonUIController.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if (invincCount <= 0)
            {
                PlayerController.singletonPlayerController.bodySpriteRenderer.color = new Color(
                    PlayerController.singletonPlayerController.bodySpriteRenderer.color.r,
                    PlayerController.singletonPlayerController.bodySpriteRenderer.color.g,
                    PlayerController.singletonPlayerController.bodySpriteRenderer.color.b,1f);  
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            currentHealth--;
            invincCount = damageInvincLength;
            PlayerController.singletonPlayerController.bodySpriteRenderer.color = new Color(
                PlayerController.singletonPlayerController.bodySpriteRenderer.color.r,
                PlayerController.singletonPlayerController.bodySpriteRenderer.color.g,
                PlayerController.singletonPlayerController.bodySpriteRenderer.color.b,.5f);

            if (currentHealth <= 0)
            {
                PlayerController.singletonPlayerController.gameObject.SetActive(false);
                // UIController.singletonUIController.deathScreen.SetActive(true);
            }

            hpSlider.Decrease(1);
            // UIController.singletonUIController.healthSlider.value = currentHealth;
            // UIController.singletonUIController.healthText.text =
            //     currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void MakeInvincible()
    {
        invincCount = damageInvincLength;
        PlayerController.singletonPlayerController.bodySpriteRenderer.color = new Color(
            PlayerController.singletonPlayerController.bodySpriteRenderer.color.r,
            PlayerController.singletonPlayerController.bodySpriteRenderer.color.g,
            PlayerController.singletonPlayerController.bodySpriteRenderer.color.b);
    }
}