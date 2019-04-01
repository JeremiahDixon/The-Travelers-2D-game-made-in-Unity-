using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialog : MonoBehaviour {

    public Transform player;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typingSpeed;
    public GameObject continueButton;
    private bool talking = false;

    private int index;

    /*void Start()
    {
        StartCoroutine(Type());
    }*/

    void Update()
    {
        if(!talking)
        {
            if (Vector2.Distance(transform.position, player.position) < 0.9f && Input.GetKey(KeyCode.Z)){
                talking = true;
                PlayerController.setSpeed(0);
                StartCoroutine(Type());
            }
        }

        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if (index == sentences.Length - 1)
        {
            PlayerController.setSpeed(3);
        }

    }


    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (talking)
        {
            continueButton.SetActive(false);
            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(Type());

            }
            else
            {
                textDisplay.text = "";
                continueButton.SetActive(false);
                talking = false;
                index = 0;
            }
        }
    }


}
