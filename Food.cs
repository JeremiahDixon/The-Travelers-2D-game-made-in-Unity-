using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public int healAmount;
    private int playerCurrentHealth = PlayerController.getHealth();
    private int playerMaxHealth = PlayerController.getMaxHealth();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCurrentHealth = PlayerController.getHealth();
            playerMaxHealth = PlayerController.getMaxHealth();
            if (playerCurrentHealth < playerMaxHealth)
            {
                playerCurrentHealth += healAmount;
                if (playerCurrentHealth > playerMaxHealth)
                {
                    PlayerController.setHealthToMax();
                }
                else
                {
                    PlayerController.setHealth(playerCurrentHealth);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
