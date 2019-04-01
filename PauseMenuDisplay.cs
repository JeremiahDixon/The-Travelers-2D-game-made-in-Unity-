using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDisplay : MonoBehaviour {

    QuestManager questManager = new QuestManager();

    public Text quest1Text;
    public Text quest2Text;
    public Text quest3Text;

    void Update()
    {
        quest1Text.text = "Obtain a Key: " + questManager.getQuest1();
        quest2Text.text = "Level Up: " + questManager.getQuest2();
        quest3Text.text = "Attack an Enemy: " + questManager.getQuest3();
    }
}
