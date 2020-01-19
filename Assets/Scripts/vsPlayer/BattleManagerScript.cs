using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagerScript : Photon.PunBehaviour
{
    public int PN;
    public int scorepoint;
    public float playerhp;
    public float timertime = 300;
    public string myname;
    public bool gamestart;

    MyManagerScript MMS;
    Text statustext;
    Text timertext;
    Slider HPslider;

    void Start()
    {
        if (this.photonView.isMine)
        {
            PN = PlayerPrefs.GetInt("PlayerNumber");
            myname = PlayerPrefs.GetString("Name");
        }
        MMS = GameObject.Find("MyManager").GetComponent<MyManagerScript>();
        GameObject.Find("MainCanvas").transform.Find(PN.ToString() + "PStatus").gameObject.SetActive(true);
        statustext = GameObject.Find(PN.ToString() + "PStatus").transform.Find("Text").gameObject.GetComponent<Text>();
        HPslider = GameObject.Find(PN.ToString() + "PStatus").transform.Find("HPBar").gameObject.GetComponent<Slider>();
        statustext.text = PN.ToString() + "P:" + myname + ":" + scorepoint.ToString();
        if (PN == 1)
        {
            timertext = GameObject.Find("TimerText").GetComponent<Text>();
        }
    }
    
    void Update()
    {
        if (gamestart)
        {
            MMS.playeron = true;
        }

        if (!GameObject.Find("MainCanvas").transform.Find(PN.ToString() + "PStatus").gameObject.activeInHierarchy)
        {
            GameObject.Find("MainCanvas").transform.Find(PN.ToString() + "PStatus").gameObject.SetActive(true);
        }

        if (this.photonView.isMine)
        {
            if (PN == 1)
            {
                gamestart = MMS.start;
                timertime = MMS.gametime;
            }
            scorepoint = MMS.myscore;
            playerhp = MMS.myHP;
        }

        if (!GameObject.Find("MainCanvas").transform.Find(PN.ToString() + "PStatus").gameObject.activeInHierarchy)
        {
            GameObject.Find("MainCanvas").transform.Find(PN.ToString() + "PStatus").gameObject.SetActive(true);
        }

        statustext.text = PN.ToString() + "P:" + myname + ":" + scorepoint.ToString();
        HPslider.value = playerhp;
        if (PN == 1)
        {
            if (timertime > 0)
            {
                timertext.text = Mathf.FloorToInt(timertime / 60).ToString() + ":" + (Mathf.FloorToInt(timertime) % 60).ToString("00");
            }
            else
            {
                MMS.finish = true;
            }
        }

        if (MMS.finish)
        {
            if (MMS.maxscore <= scorepoint)
            {
                MMS.maxscore = scorepoint;
                MMS.maxname = myname;
            }
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
        if (!MMS.finish)
        {
            for (int i = 1; i <= 4; i++)
            {
                GameObject.Find("MainCanvas").transform.Find(i.ToString() + "PStatus").gameObject.SetActive(false);
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(PN);
            stream.SendNext(scorepoint);
            stream.SendNext(myname);
            stream.SendNext(gamestart);
            stream.SendNext(playerhp);
            stream.SendNext(timertime);
        }
        else
        {
            PN = (int)stream.ReceiveNext();
            scorepoint = (int)stream.ReceiveNext();
            myname = (string)stream.ReceiveNext();
            gamestart = (bool)stream.ReceiveNext();
            playerhp = (float)stream.ReceiveNext();
            timertime = (float)stream.ReceiveNext();
        }
    }
}
