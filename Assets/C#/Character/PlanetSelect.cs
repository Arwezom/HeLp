using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlanetSelect : MonoBehaviour
{
    public GameObject[] worlds;
    public int selectedWorld;
    public TMP_Text Name;

    private void Awake()
    {
        Load();
        selectedWorld = PlayerPrefs.GetInt("selectedWorld", 0);
        foreach (GameObject player in worlds)
            player.SetActive(false);

        worlds[selectedWorld].SetActive(true);
    }

    public void Next()
    {
        worlds[selectedWorld].SetActive(false);
        selectedWorld++;
        if(selectedWorld == worlds.Length)
        {
            selectedWorld = 0;
        }

    
        worlds[selectedWorld].SetActive(true);
        PlayerPrefs.SetInt("selectedWorld", selectedWorld);
        Save();
    }
    public void Back()
    {
        worlds[selectedWorld].SetActive(false);
        selectedWorld--;
        if(selectedWorld == -1)
        {
            selectedWorld = worlds.Length-1;
        }

    
        worlds[selectedWorld].SetActive(true);
        PlayerPrefs.SetInt("selectedWorld", selectedWorld);
        Save();
    }
    public void SceneSwap(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    private void Load()
    {
        selectedWorld = PlayerPrefs.GetInt("selectedWorld");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("selectedWorld", selectedWorld);
    }

}
