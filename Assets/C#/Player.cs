using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
   public GameObject[] skins;
    public int selectedCharacter;
   private void Awake()
    {
        Load();
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[selectedCharacter].SetActive(true);
    }
    
    private void Load()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
    }


}
