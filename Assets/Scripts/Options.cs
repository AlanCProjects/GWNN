using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Options : MonoBehaviour{

    public string NumOption; //Number to identify button
    public GameObject BtnOptionPrefab; //Prefab of button options
    private Dictionary<float, float[]> positions = new Dictionary<float, float[]>();
    private List<string> optionInfo = new List<string>(); //List with text to be displayed in the button
    private List<string> Node = new List<string>(); //List with the nodes correspondig a cada button
    private List<string> Conversation = new List<string>(); //Conversation corresponding
    private List<string> ChRep = new List<string>(); //Character's name to reply


    public void Option(string opGroup){ 
        string[] allOptions = File.ReadAllLines("./Assets/Characters/Dialogues/Player.txt"); //Read and open file "Player.scv"
        int meter = 0;
    
        foreach (var list in allOptions){
            //Clear elemnets of allOptions variable and split elementes
            var text = list.Split(';');
            
            if (text[0] == opGroup){
                
                ++ meter;
                Conversation.Add(text[1]);
                Node.Add(text[2]);
                ChRep.Add(text[3]);
                optionInfo.Add(text[4]);
            }
            
        }

        switch(meter){
            
            case 1:

                positions.Add(0, new float[]{290.0f, 190.0f}); //add coordenates button
                ButtonCreator(0);
                break;
            
            case 2:

                positions.Add(0, new float[]{290.0f, 209.0f});
                positions.Add(1, new float[]{2900.0f, 1680.0f});
                ButtonCreator(1);
                break;
            
            case 3:

                positions.Add(0, new float[]{290.0f, 230.0f});
                positions.Add(1, new float[]{290.0f, 198.0f});
                positions.Add(2, new float[]{290.0f, 165.0f});
                ButtonCreator(2);
                break;

            case 4:

                positions.Add(0, new float[]{290.0f, 241.0f});
                positions.Add(1, new float[]{290.0f, 209.0f});
                positions.Add(2, new float[]{290.0f, 176.0f});
                positions.Add(3, new float[]{290.0f, 144.0f});
                ButtonCreator(3);
                break;
        }


    }

    public void ButtonCreator(int repeat){
        GameObject NewBtn; // GameObject to keep the prefab of GameObject BtnOption
        BtnController BtnControl; //Script BtnControl

        for(int i = 0; i <= repeat; ++i){

            NewBtn = Instantiate(BtnOptionPrefab, new Vector3(290.0f,positions[i][1],0),
            Quaternion.identity); //Instantatie a prefabs BtnOptionPrefab
            NewBtn.transform.parent = transform; //To do the prefab be child of GameObject Options
            BtnControl= NewBtn.GetComponent<BtnController>();
            BtnControl.TextToShow = optionInfo[i]; //Set text to the variable TextToShow of BtnController (script)
            BtnControl.CharRep = ChRep[i]; //Send character's name to reply to script BtnController
            BtnControl.Conversation = Conversation[i]; //Send de number conversation  to script BtnController
            BtnControl.NextNode = Node[i]; //Send the node of conversation to script BtnController
        }

        positions.Clear();
        Conversation.Clear();
        Node.Clear();
        ChRep.Clear();
        optionInfo.Clear();

    }

  
}
