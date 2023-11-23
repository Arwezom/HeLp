using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEyes : MonoBehaviour
{
    public Transform Player;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Watch");
        transform.LookAt(Player);
    }

}
