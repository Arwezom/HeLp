using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharManager : MonoBehaviour
{
    public CharDatabase characterDB;
    public Text nameText;
    public SpriteRenderer artworksprite;
    private int selectedOption;

    //Start
    void Start()
    {
        UpdateCharacter(selectedOption);

        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
    }

    
    //Next
    public void NextOption()
    {
        selectedOption++;
        
        if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
    }


    //Back
    public void BackOption()
    {
        selectedOption--;
        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }


    //Update
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworksprite.sprite = character.Skin;
        nameText.text =character.Name;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    //Change Scene
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

}
