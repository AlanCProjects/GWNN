using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    private int StartGameID = Animator.StringToHash("StartGame");
    // Start is called before the first frame update
     void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(AnimStart());
    }

    IEnumerator AnimStart(){

        gameObject.GetComponent<Animator>().SetBool(StartGameID, true);
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Animator>().SetBool(StartGameID, false);
    }


}
