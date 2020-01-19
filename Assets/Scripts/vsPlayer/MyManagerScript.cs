using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyManagerScript : Photon.PunBehaviour
{
    public int myscore;
    public int mymove;
    public int myrotate;
    public int maxscore;
    public string maxname;
    public float myHP;
    public float gametime = 50;
    public bool start;
    public bool finish;
    public bool playeron;
    public bool playergo;
    public bool playerdeath;
    public bool cameraon;
    public GameObject okbutton;
    public GameObject leaveroombutton;
    public GameObject backcamera;
    public GameObject realcamera;
    public GameObject gamesetpanel;
    public Text leaveroombuttontext;
    public Text messagetext;
    public Text scoretext;
    public Text winnertext;
    public Slider HPslider;

    int playernumber;
    float starttimer;
    float revivetimer;
    float texttimer;
    float finishtimer;
    float spawntimer=25;
    bool leave;
    bool texton;
    GameObject myplayer;

    void Start()
    {
        playernumber = PlayerPrefs.GetInt("PlayerNumber");
        PhotonNetwork.Instantiate("BattleManager", transform.position, Quaternion.identity, 0);
    }
    
    void Update()
    {
        if (playernumber == 1)
        {
            if (!start)
            {
                starttimer += Time.deltaTime;
                if (starttimer > 3)
                {
                    start = true;
                }
            }
        }
        if (playeron && !playergo)
        {
            PlayerGenerate();
            playergo = true;
        }
        gametime -= Time.deltaTime;

        if (texton)
        {
            texttimer += Time.deltaTime;
            if (texttimer > 5)
            {
                texton = false;
                texttimer = 0;
                messagetext.text = "";
            }
        }

        HPslider.value = myHP;
        scoretext.text = myscore.ToString();

        if (myHP <= 0)
        {
            revivetimer += Time.deltaTime;
            if (revivetimer > 5)
            {
                PlayerGenerate();
                revivetimer = 0;
                myHP = 3;
            }
        }

        if (finish)
        {
            finishtimer += Time.deltaTime;
            if (finishtimer > 3)
            {
                winnertext.text = maxname + maxscore.ToString();
                gamesetpanel.SetActive(true);
            }
        }

        if (playernumber == 1)
        {
            if (!finish)
            {
                spawntimer += Time.deltaTime;
            }
            if (spawntimer > 30)
            {
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(0, 1, 24), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(0, 1, 40), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(0, 1, 64), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(8, 1, 0), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(16, 1, 32), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(16, 1, 48), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(24, 1, 16), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(24, 1, 72), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(32, 1, 0), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(32, 1, 56), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(36, 1, 36), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(40, 1, 16), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(40, 1, 72), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(48, 1, 0), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(48, 1, 56), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(56, 1, 24), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(56, 1, 40), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(64, 1, 72), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(72, 1, 8), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(72, 1, 32), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                PhotonNetwork.InstantiateSceneObject("OnlineEnemy", new Vector3(72, 1, 40), Quaternion.Euler(0, Random.Range(0, 360), 0), 0, null);
                spawntimer = 0;
            }
        }
    }

    void PlayerGenerate()
    {
        if (playernumber == 1)
        {
            myplayer = PhotonNetwork.Instantiate("OnlinePlayer" + playernumber.ToString(), new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0), 0);
        }
        else if (playernumber == 2)
        {
            myplayer = PhotonNetwork.Instantiate("OnlinePlayer" + playernumber.ToString(), new Vector3(72, 1, 72), Quaternion.Euler(0, 180, 0), 0);
        }
        else if (playernumber == 3)
        {
            myplayer = PhotonNetwork.Instantiate("OnlinePlayer" + playernumber.ToString(), new Vector3(72, 1, 0), Quaternion.Euler(0, 270, 0), 0);
        }
        else if (playernumber == 4)
        {
            myplayer = PhotonNetwork.Instantiate("OnlinePlayer" + playernumber.ToString(), new Vector3(0, 1, 72), Quaternion.Euler(0, 90, 0), 0);
        }
        Debug.Log("Generate!");
        GameObject back = Instantiate(backcamera);
        GameObject real = Instantiate(realcamera);
        back.transform.parent = myplayer.transform.Find("Main");
        back.GetComponent<Camera>().depth = 2;
        real.transform.parent = myplayer.transform.Find("Main");
        real.GetComponent<Camera>().depth = 1;
        if (playernumber == 1)
        {
            back.transform.position = myplayer.transform.position + new Vector3(0, 6, -6.5f);
            real.transform.position = myplayer.transform.position + new Vector3(0, 0.53f, 0.53f);
            back.transform.rotation = Quaternion.Euler(30, 0, 0);
            real.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playernumber == 2)
        {
            back.transform.position = myplayer.transform.position + new Vector3(0, 6, 6.5f);
            real.transform.position = myplayer.transform.position + new Vector3(0, 0.53f, -0.53f);
            back.transform.rotation = Quaternion.Euler(30, 180, 0);
            real.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (playernumber == 3)
        {
            back.transform.position = myplayer.transform.position + new Vector3(6.5f, 6, 0);
            real.transform.position = myplayer.transform.position + new Vector3(-0.53f, 0.53f, 0);
            back.transform.rotation = Quaternion.Euler(30, 270, 0);
            real.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (playernumber == 4)
        {
            back.transform.position = myplayer.transform.position + new Vector3(-6.5f, 6, 0);
            real.transform.position = myplayer.transform.position + new Vector3(0.53f, 0.53f, 0);
            back.transform.rotation = Quaternion.Euler(30, 90, 0);
            real.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void PlayerMove(int n)
    {
        mymove = n;
    }

    public void PlayerRotate(int n)
    {
        myrotate = n;
    }

    public void PlayerCameraWork()
    {
        cameraon = true;
    }

    public void LeaveRoomButton()
    {
        if (leave == true)
        {
            okbutton.SetActive(false);
            leaveroombuttontext.text = "退室";
            leave = false;
        }
        else
        {
            okbutton.SetActive(true);
            leaveroombuttontext.text = "キャンセル";
            leave = true;
        }
    }

    public void WriteMessage(string message)
    {
        messagetext.text = message;
        texton = true;
        texttimer = 0;
    }

    public void OutOfRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("ルームから退室しました");
        SceneManager.LoadScene(8);
    }
}
