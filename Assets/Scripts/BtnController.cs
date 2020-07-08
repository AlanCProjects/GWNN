using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    
    public Text Text; //Text showing in the button
    public string TextToShow; //String receiving text to display
    public string Conversation;
    public string NextNode; //Value the next node to continue
    public string CharRep;
    public Characters Dialogue;
    public GameObject Character, Options;

    public void Start(){

        Text.text = TextToShow; //Show text in the button
        
    }

    public void SendNextNode(){

        //Set the new node to start the next dialogue
        Options = GameObject.FindGameObjectWithTag("Option");
        Character = GameObject.FindGameObjectWithTag("Character");
        Dialogue = Character.GetComponent<Characters>();
        Dialogue.Dialogue(CharRep, Conversation, NextNode);

        foreach(Transform child in Options.transform){

            //Kill all Option's child front her >:D
            child.gameObject.SetActive(false);
        }
        
    }
    

}
