using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    
    //character name is talking
    public string name; //aquí se almacena el nombre del que habla

    [TextArea(3, 20)]
    public string[] sentences; //aquí se guarda todos los textos del dialogo de un personaje


}
