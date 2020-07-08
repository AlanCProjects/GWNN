using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Options : MonoBehaviour{
    
    [System.Serializable]

    public class Pool{
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }

    #region Varialbes
    public string NumOption; //Number to identify button
    public GameObject BtnOptionPrefab; //Prefab of button options
    private Dictionary<float, float[]> positions = new Dictionary<float, float[]>(); //positions to set the buttons
    private List<string> optionInfo = new List<string>(); //List with text to be displayed in the button
    private List<string> Node = new List<string>(); //List with the nodes correspondig a cada button
    private List<string> Conversation = new List<string>(); //Conversation corresponding
    private List<string> ChRep = new List<string>(); //Character's name to reply
    public List <Pool> pools; //Pool's list
    public Dictionary<string, Queue<GameObject>> poolDictionary; //Save the pool's objects
    #endregion

    void Start(){

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools){
            Queue<GameObject> objctPool = new Queue<GameObject>();


            for (int i = 0; i < pool.Size; i++){
                
                GameObject obj = Instantiate(pool.Prefab);
                obj.transform.SetParent(transform, false);
                obj.SetActive(false);
                objctPool.Enqueue(obj);

            }
            poolDictionary.Add(pool.Tag, objctPool);
        }
    }

    public void Option(string opGroup){ 
        string[] allOptions = File.ReadAllLines("./Assets/Characters/Dialogues/Player.csv"); //Read and open file "Player.scv"
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
                BtnSpawnFromPool(0, "Button");
                break;
            
            case 2:

                positions.Add(0, new float[]{290.0f, 209.0f});
                positions.Add(1, new float[]{2900.0f, 1680.0f});
                BtnSpawnFromPool(1, "Button");
                break;
            
            case 3:

                positions.Add(0, new float[]{290.0f, 230.0f});
                positions.Add(1, new float[]{290.0f, 198.0f});
                positions.Add(2, new float[]{290.0f, 165.0f});
                BtnSpawnFromPool(2, "Button");
                break;

            case 4:

                positions.Add(0, new float[]{290.0f, 241.0f});
                positions.Add(1, new float[]{290.0f, 209.0f});
                positions.Add(2, new float[]{290.0f, 176.0f});
                positions.Add(3, new float[]{290.0f, 144.0f});
                BtnSpawnFromPool(3, "Button");
                break;
        }


    }


    public void BtnSpawnFromPool(int repeat, string tag){
        GameObject ObjToSpawn;
        BtnController btnControl;

        if(!poolDictionary.ContainsKey(tag)){
            Debug.LogWarning("Pool whit tag"+tag+"doesn't exist ");
            return;
        }

        for (int i = 0; i <= repeat; i++){

            ObjToSpawn =  poolDictionary[tag].Dequeue(); //Get a object of the pool
            ObjToSpawn.SetActive(true); //Active the object 
            ObjToSpawn.transform.position = new Vector2(290.0f, positions[i][1]); //Set a position
            btnControl = ObjToSpawn.GetComponent<BtnController>(); //I don't know what i do here
            btnControl.TextToShow = optionInfo[i]; //Set text to the variable TextToShow of BtnController (script)
            btnControl.CharRep = ChRep[i]; //Send character's name to reply to script BtnController
            btnControl.Conversation = Conversation[i]; //Send de number conversation  to script BtnController
            btnControl.NextNode = Node[i]; //Send the node of conversation to script BtnController
            poolDictionary[tag].Enqueue(ObjToSpawn); //give back the object to the pool
        }
        #region ClearLists
        positions.Clear();
        Conversation.Clear();
        Node.Clear();
        ChRep.Clear();
        optionInfo.Clear();
        #endregion
    }

  
}
