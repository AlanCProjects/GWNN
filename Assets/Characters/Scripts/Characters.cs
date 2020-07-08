using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Characters : MonoBehaviour{
    
    public Dialogue DiaChar; //Crate new objet "Dialogue"
    private int AppearID = Animator.StringToHash("appear"); //ID of animation "appear"
    private int VisibleID = Animator.StringToHash("visible"); //ID of animation "visible"
    // Start is called before the first frame update
    void Start(){

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update(){
        
    }

    IEnumerator StartGame(){

        gameObject.GetComponent<Animator>().SetBool(VisibleID, false); //Change opacity 100% to 0% (in a frame)
        yield return new WaitForSeconds(3); //wait 3 seconds
        gameObject.GetComponent<Animator>().SetBool(AppearID, true); //Animation with transiton to opacity 0% to 100%
        gameObject.GetComponent<Animator>().SetBool(VisibleID, true); //Go to idle state 
        yield return new WaitForSeconds(2); //Wait 2 seconds
        Dialogue("Ghost", "00", "00"); //Start the game with first dialogue


    }
    public void Dialogue(string name, string scene, string node){

        List<string> diaList = new List<string>(); //List where keep lines clear csv files
        string[] allDialogue = File.ReadAllLines("./Assets/Characters/Dialogues/"+name+".csv"); //Read and save csv file
        string[] dialogue; //Keep text to show in dialogue of character
        string opGroup = null; //Id group options

        foreach (var list in allDialogue){ //Line by line is save in list "diaList"

            var text = list.Split(';');
            if (text[0] == scene && text[1] == node){ //Chek if the lines are correspondent to the actual dialogue
                
                diaList.Add(text[3]); //Add the text content to diaList (text to show in the dialogue)
                if (text[2] != "XX"){

                    opGroup = text[2];
                }



            }else if(diaList.Count != 0){ //If the list have an atrivute and don't found line
            
                break; //break the foreach
            }
        }

        dialogue = diaList.ToArray(); //Change the list to array
        DiaChar.name=name; //Set name to object DiaCharacter
        DiaChar.sentences = dialogue; //Set array whit the dialogues to DiaCharacter
        FindObjectOfType<DialogueManager>().StartDialogue(DiaChar, opGroup); //Call method StartDialogue() of DialogueManeger.cs

    }
}
