using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{ 
    void Start() {
        StartCoroutine(Timer());
    } 

    void Update() {
        
    }
     IEnumerator Timer()
     {
        TimerBeginning:

        //left
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,140));
        yield return new WaitForSeconds(10);
        
        //blink
        transform.localRotation = Quaternion.Euler(new Vector3(100,100,0));
        GameObject.Find("EyeD").SetActive(true);
        yield return new WaitForSeconds(1);
        GameObject.Find("EyeD").SetActive(false);

        //right
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,-140));
        yield return new WaitForSeconds(10);

        goto TimerBeginning;
     }
}

