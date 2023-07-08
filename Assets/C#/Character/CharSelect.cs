using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedCharacter;
    public TMP_Text Name;

    private void Awake()
    {
        Load();
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
        player.SetActive(false);

        skins[selectedCharacter].SetActive(true);
    }

    public void Next()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if(selectedCharacter == skins.Length)
        {
            selectedCharacter = 0;
        }

    
        skins[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        Save();
    }
    public void Back()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter == -1)
        {
            selectedCharacter = skins.Length-1;
        }

    
        skins[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        Save();
    }
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    private void Load()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

}
