using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    public Text healthText;

    void Update()
    {
        healthText.text = "Health: " + PlayerController.getHealth() + "/" + PlayerController.getMaxHealth();
    }

}
