using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyDisplay : MonoBehaviour {

    public Text keyText;

    void Update()
    {
        keyText.text = "x " + PlayerController.getkeys();
    }
}
