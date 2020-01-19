using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManagerScript : Photon.PunBehaviour
{
    public GameObject[] connectings;
    public GameObject noroomname;
    public InputField roomname;
    public Text createroomtext;
    public Text preparebuttontext;
    public Image preparebuttonimage;
    public Sprite onbutton;
    public Sprite offbutton;
    public bool gamestandby;
    public bool playerprepared;
    public int playernumber;
    public string playername;

    string _gameVersion = "3.0";
    RoomOptions roomoption;

    void Start()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
            Debug.Log("Photonに接続しました。");
        }
        if (PhotonNetwork.connected)
        {
            Debug.Log("既にPhotonに接続しています。");
            PageShift(1);
        }
    }

    void Update()
    {
        
    }

    public void PageShift(int n)
    {
        for(int i = 0; i < connectings.Length; i++)
        {
            connectings[i].SetActive(false);
        }
        connectings[n].SetActive(true);
    }

    public override void OnConnectedToPhoton()
    {
        base.OnConnectedToPhoton();
        Debug.Log("ConnectedToPhoton!");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("JoinedLobby!");
        if (connectings[0].activeInHierarchy)
        {
            PageShift(1);
        }
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        base.OnFailedToConnectToPhoton(cause);
        Debug.Log("FailedToConnectToPhoton!");
        PageShift(2);
    }

    public void Disconnection()
    {
        Debug.Log("DisconnectedToPhoton!");
        PhotonNetwork.Disconnect();
    }

    public void FriendRoomEnter()
    {
        if (roomname.text == "")
        {
            noroomname.SetActive(true);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomname.text);
        }
    }
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(roomname.text + "ルームに入室しました");
        playernumber = PhotonNetwork.playerList.Length;
        playername = PlayerPrefs.GetString("Name");
        Debug.Log(playernumber);
        PageShift(5);
        PhotonNetwork.Instantiate("RoomManager", transform.position, Quaternion.identity, 0);
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
        Debug.Log(roomname.text + "ルームの入室に失敗しました");
        createroomtext.text = "\"" + roomname.text + "\"\nというルームは存在しませんでした。\nルームを作成しますか？";
        PageShift(4);
    }

    public void FriendRoomCreate()
    {
        PhotonNetwork.CreateRoom(roomname.text, new RoomOptions() { MaxPlayers = 4, IsVisible = false }, null);
        Debug.Log(roomname.text + "ルームを作成しました");
    }

    public void FriendRoomOut()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void RandomRoomEnter()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4, IsVisible = true }, null);
        Debug.Log("ランダムルームを作成しました");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log(roomname.text + "ルームを退室しました。");
        PageShift(3);
        playerprepared = false;
        preparebuttonimage.sprite = offbutton;
        preparebuttontext.text = "準備完了";
        roomname.text = "";
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
    }

    public void PlayerPreparation()
    {
        if (playerprepared)
        {
            playerprepared = false;
            preparebuttonimage.sprite = offbutton;
            preparebuttontext.text = "準備完了";
        }
        else
        {
            playerprepared = true;
            preparebuttonimage.sprite = onbutton;
            preparebuttontext.text = "キャンセル";
        }
    }

    public void StartGame()
    {
        PhotonNetwork.room.IsOpen = false;
        gamestandby = true;
    }
}
