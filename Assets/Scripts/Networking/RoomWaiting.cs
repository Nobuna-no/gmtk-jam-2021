using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomWaiting : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private int numberPlayers;
    // Start is called before the first frame update
    private void Start()
    {
        PlayerNumbering.OnPlayerNumberingChanged += PlayerNumbering_OnPlayerNumberChanged;
    }

	private void OnDestroy()
	{
        PlayerNumbering.OnPlayerNumberingChanged -= PlayerNumbering_OnPlayerNumberChanged;
    }

    private void PlayerNumbering_OnPlayerNumberChanged()
    {
        numberPlayers = PlayerNumbering.SortedPlayers.Length;
        text.text = "Waiting in room..." + NetworkManager.Instance.RoomName + " " + numberPlayers + "/2 Players";
    }
}
