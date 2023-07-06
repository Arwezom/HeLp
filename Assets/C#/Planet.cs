using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Planet : MonoBehaviour
{
   public GameObject[] worlds;
    public int selectedWorld;
   private void Awake()
    {
        Load();
        selectedWorld = PlayerPrefs.GetInt("selectedWorld", 0);
        foreach (GameObject player in worlds)
            player.SetActive(false);

        worlds[selectedWorld].SetActive(true);
    }
    
    private void Load()
    {
        selectedWorld = PlayerPrefs.GetInt("selectedWorld");
    }


}
