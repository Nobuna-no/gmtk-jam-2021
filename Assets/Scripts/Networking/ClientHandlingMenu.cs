using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientHandlingMenu : MonoBehaviour
{
    [SerializeField] private string roomWaitingScene = "RoomWaiting";
    [SerializeField] private string OfflineScene = "Offline";
    [SerializeField] private GameObject roomNameInput;
	

	#region MONOBEHAVIOUR_METHODS
	private void Awake()
	{
		roomNameInput.SetActive(false);
	}

	private void OnDestroy()
	{
		NetworkManager.Instance.OnConnectedSucess -= NetworkManager_OnConnectedSuccess;
	}

	#endregion

	public void OpenRoomNameInput()
	{
		NetworkManager.Instance.Connect(); 
		roomNameInput.SetActive(true);
	}

	private void NetworkManager_OnConnectedSuccess()
	{
		SceneManager.LoadScene(roomWaitingScene);
	}

	public void OnRoomNameSubmit(InputField inputField)
	{
		NetworkManager.Instance.OnConnectedSucess += NetworkManager_OnConnectedSuccess;
		NetworkManager.Instance.CreateOrJoinRoom(inputField.text);
	}	



}
