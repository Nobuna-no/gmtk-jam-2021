using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuHandling : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject multiplayerControlSelection;
    [SerializeField] private string levelToLoad = "2-Gameplay";
    [SerializeField] private ToggleSwitch multiP1Control;
    [SerializeField] private ToggleSwitch multiP2Control;
    [SerializeField] private ToggleSwitch soloP1Control;

    // Start is called before the first frame update
    void Awake()
    {
        mainMenu.SetActive(true);
        multiplayerControlSelection.SetActive(false);
    }

    public void OpenMultiplayerControls()
	{
        mainMenu.SetActive(false);
        multiplayerControlSelection.SetActive(true);
    }

    public void OpenSoloControls()
    {
        Play(0);
    }

    public void Play(int gameMode)
	{
        if (gameMode == 0)
            PlayerPrefs.SetInt("P1Controls", 0);
        else
		{
            PlayerPrefs.SetInt("P1Controls", multiP1Control.IsA ? 1 : 2);
            PlayerPrefs.SetInt("P2Controls", multiP2Control.IsA ? 0 : 1);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene(levelToLoad);
	}
}
