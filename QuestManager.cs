using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager {

    string quest1 = "Incompleted";
    string quest2 = "Incompleted";
    string quest3 = "Incompleted";

    public QuestManager(){
        PlayerController.CompleteQuest += completeQuest;
    }

    public void completeQuest(string quest){
        switch(quest){
            case "Obtain a Key":
                quest1 = "Completed";
                break;
            case "Level Up":
                quest2 = "Completed";
                break;
            case "Attack an Enemy":
                quest3 = "Completed";
                break;
            default:
                break;
        }
    }

    public string getQuest1(){
        return quest1;
    }

    public string getQuest2()
    {
        return quest2;
    }

    public string getQuest3()
    {
        return quest3;
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
