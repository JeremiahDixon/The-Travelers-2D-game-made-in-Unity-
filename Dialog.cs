using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typingSpeed;
    public GameObject continueButton;

    private int index;

    void Start()
    {
        StartCoroutine(Type());
        PlayerController.setSpeed(0);
    }

    void Update()
    {
        if(index == sentences.Length-1){
            PlayerController.setSpeed(3);
        }

        if(textDisplay.text == sentences[index]){
            continueButton.SetActive(true);
        }
    }


    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence(){

        continueButton.SetActive(false);
        if(index < sentences.Length - 1){

            index++;
            textDisplay.text = "";
            StartCoroutine(Type());

        }
        else{
            index = 0;
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
	
}
