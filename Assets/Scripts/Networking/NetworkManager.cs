using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

	public delegate void OnConnectedAction();
	public event OnConnectedAction OnConnectedSucess;

	private string _roomName = "";
	public string RoomName { get { return _roomName; } }

	private string _isPlayingOnline = "";
	public string IsPlayingOnline { get { return _isPlayingOnline; } }

	#region MONOBIHAVIOUR_METHODS
	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}
	#endregion


	public void Connect()
	{
		if (!PhotonNetwork.IsConnected)
		{
			// #Critical, we must first and foremost connect to Photon Online Server.
			PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = "1";
		}
	}
	#region PUN_CALLBACKS
	public void CreateOrJoinRoom(string roomName)
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.IsVisible = false;
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
	}

	public override void OnJoinedRoom()
	{
		OnConnectedSucess?.Invoke();
		_roomName = PhotonNetwork.CurrentRoom.Name;
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		base.OnJoinRoomFailed(returnCode, message);
		Debug.LogError("[Netowking] Failed Joining room error code: " + returnCode + ", message:" + message);
	}
	#endregion

}
