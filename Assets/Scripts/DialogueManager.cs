using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{
    #region variables
    public Text NameText, DialogueText; //variable para el cuadro de dialogo nombre y contenido
    public Animator animator;
    public GameObject btnoption; //no sé que hice acá :'c
    public Options options; //Script Options
    public string OpGroup; //Variable donde se guardara el N° que identifica al frupo de opciones a mostrar
    private Queue<string> sentences; //aquí es donde irán todos los contenidos del dialogo
    private bool NextSentence = true;
    private int IsOpenID = Animator.StringToHash("IsOpen"); //Id reference to animator IsOpen
    #endregion
     

    // Start is called before the first frame update
    void Awake(){

        btnoption = GameObject.FindGameObjectWithTag("Option"); //
    }
    void Start(){
        
        sentences = new Queue<string>(); //Queue es como un array que tiene un sitema FIFO (First-in first-out)
    }

    public void Update(){
        if (NextSentence == true && Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")){

            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, string opGroup){

        OpGroup = opGroup;
        NextSentence = true;
        animator.SetBool(IsOpenID, true); //activa la animacion de entrada de la TextBox
        NameText.text = dialogue.name;  //Aquí va el nombre del personaje que habla que esté en el script "dialogue"
        Debug.Log("Start conversation with "+ dialogue.name);
        sentences.Clear(); //Limpia la variable sentences por si antes tenía algo

        foreach (string sentence in dialogue.sentences){ //Cada elemento que hay almacenado en "sentences" del script dialogue

            sentences.Enqueue(sentence); //es agregado al final de la cola de sentence
            }

        

        DisplayNextSentence();

    }

    public void DisplayNextSentence(){
        
        
        if (sentences.Count == 0){
            //cuando "sentences" queda vacio llama al metodo "EndDialogue"
            EndDialogue();
            return;
        }

        
        string sentence = sentences.Dequeue(); //quita el primer valor agregado y se lo asigna a "sentence"

        StopAllCoroutines();  //Si TypeSenrence ya estaba corriendo antes esto lo detendrá
        StartCoroutine(TypeSentence(sentence)); //Comienza la corutina TypeSentence
    }

    IEnumerator TypeSentence(string sentence){

            //Vacía el "DialogueText", separa cada letra de sentence y se lo agrega a "DialogueText"
            DialogueText.text = "";
            foreach (char letter in sentence.ToCharArray()){

                DialogueText.text += letter;
                yield return null;
                
            }

        }

    public void EndDialogue(){

        //Fin del dialogo
        NextSentence = false;
        animator.SetBool(IsOpenID,false); //activa la animacion de salida de la TextBox
        btnoption.SetActive(true);
        options = btnoption.GetComponent<Options>();
        options.Option(OpGroup);


    }
}