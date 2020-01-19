using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManagerScript : Photon.PunBehaviour
{
    public int playernumber;
    public bool playerprepared;
    public string playername;
    public bool gamestart;

    PhotonManagerScript PMS;
    GameObject PM;
    float scenetimer;
    bool playerout;
    int count;

    void Start()
    {
        if (this.photonView.isMine)
        {
            PM = GameObject.Find("PhotonManager");
            PMS = PM.GetComponent<PhotonManagerScript>();
            playernumber = PMS.playernumber;
            playername = PMS.playername;
            Debug.Log("Null");
        }
    }
    
    void Update()
    {
        if(!GameObject.Find("Rooms").transform.Find(playernumber + "Ps").gameObject.activeInHierarchy)
        {
            GameObject.Find("Rooms").transform.Find(playernumber + "Ps").gameObject.SetActive(true);
        }
        GameObject.Find("Rooms").transform.Find(playernumber + "Ps").Find("NameText").GetComponent<Text>().text = playername;

        if (playerprepared)
        {
            GameObject.Find("Rooms").transform.Find(playernumber + "Ps").Find("Preparetext").GetComponent<Text>().text = "準備完了";
        }
        else
        {
            GameObject.Find("Rooms").transform.Find(playernumber + "Ps").Find("Preparetext").GetComponent<Text>().text = "準備中";
        }

        if (gamestart)
        {
            scenetimer += Time.deltaTime;
            if (scenetimer > 1)
            {
                Destroy(GameObject.Find("TitleBGM(Clone)"));
                SceneManager.LoadScene(9);
            }
        }

        if (this.photonView.isMine)
        {
            //スタートボタンのこと
            if (playernumber == 1)
            {
                for (int i = 1; i <= PhotonNetwork.playerList.Length; i++)
                {
                    if (GameObject.Find("Rooms").transform.Find(i + "Ps").Find("Preparetext").GetComponent<Text>().text == "準備完了")
                    {
                        count++;
                    }
                }
                if (count == PhotonNetwork.playerList.Length && PhotonNetwork.playerList.Length > 1)
                {
                    GameObject.Find("Rooms").transform.Find("StartButton").gameObject.SetActive(true);
                }
                else
                {
                    GameObject.Find("Rooms").transform.Find("StartButton").gameObject.SetActive(false);
                }
                count = 0;
            }
            else
            {
                GameObject.Find("Rooms").transform.Find("StartButton").gameObject.SetActive(false);
            }

            //誰か抜けたときプレイヤーをずらす
            if (playernumber != 1 && playerout)
            {
                for(int i = 1; i < playernumber; i++)
                {
                    if (!GameObject.Find("Rooms").transform.Find(i + "Ps").gameObject.activeInHierarchy && playerout)
                    {
                        PMS.playernumber--;
                        playernumber = PMS.playernumber;
                        playerout = false;
                    }
                }
            }

            //いないプレイヤーを非表示に
            if (PhotonNetwork.playerList.Length != 4)
            {
                for (int i = PhotonNetwork.playerList.Length + 1; i <= 4; i++)
                {
                    GameObject.Find("Rooms").transform.Find(i + "Ps").gameObject.SetActive(false);
                }
            }

            //準備とスタートを取得
            playerprepared = PMS.playerprepared;

            if (playernumber == 1)
            {
                gamestart = PMS.gamestandby;
            }
            PlayerPrefs.SetInt("PlayerNumber", playernumber);
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
        for (int i = 1; i <= 4; i++)
        {
            GameObject.Find("Rooms").transform.Find(i + "Ps").gameObject.SetActive(false);
        }
        playerout = true;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playernumber);
            stream.SendNext(playername);
            stream.SendNext(playerprepared);
            stream.SendNext(gamestart);
            Debug.Log("IsWriteing");
        }
        else
        {
            playernumber = (int)stream.ReceiveNext();
            playername = (string)stream.ReceiveNext();
            playerprepared = (bool)stream.ReceiveNext();
            gamestart = (bool)stream.ReceiveNext();
            Debug.Log("IsNotWriteing");
        }
    }
}
