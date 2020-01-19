using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePlayerScript : Photon.PunBehaviour
{
    public float runspeed = 3;
    public float rotatespeed = 60;
    public float shottime = 1;
    public float playerhp = 3;
    public float enemymutekitime = 2;
    public float itemmutekitime = 7;
    public float mutekitime;
    public float stoptime = 5;
    public float power = 1;
    public bool muteki;
    public bool stop;
    public bool keycol;
    public bool UPkey;
    public bool DOWNkey;
    public bool RIGHTkey;
    public bool LEFTkey;
    public int mutekiplay;
    public int stopplay;
    public GameObject scoretext;
    public GameObject main;
    public GameObject canon;
    public GameObject catL;
    public GameObject catR;
    public GameObject freezeimage;
    public GameObject getsound;
    public GameObject damagesound;
    public GameObject deadsound;
    public GameObject systemse;
    public Camera backcamera;
    public Camera realcamera;

    float mutekitimer;
    float cleartimer;
    float stoptimer;
    float shottimer;
    float deadtimer;
    bool dead;
    int refcounter = 0;
    int bullifecounter = 1;
    int itemnumber;
    int setitem;
    int itemsetcount;
    int[] itemsetnumber = new int[3];
    OnlineBulletScript bulletscript;
    MyManagerScript MMS;
    Renderer renm;
    Renderer renc;
    Renderer renl;
    Renderer renr;
    Rigidbody rb;
    BoxCollider bc;

    void Start()
    {
        if (this.photonView.isMine)
        {
            rb = this.GetComponent<Rigidbody>();
            bc = this.GetComponent<BoxCollider>();
            renm = main.GetComponent<Renderer>();
            renc = canon.GetComponent<Renderer>();
            renl = catL.GetComponent<Renderer>();
            renr = catR.GetComponent<Renderer>();
            MMS = GameObject.Find("MyManager").GetComponent<MyManagerScript>();

            for (int i = 1; i <= 3; i++)
            {
                if (PlayerPrefs.GetInt("ItemSet" + i.ToString()) != 0)
                {
                    itemsetnumber[itemsetcount] = PlayerPrefs.GetInt("ItemSet" + i.ToString());
                    itemsetcount++;
                }
            }
        }

    }

    void Update()
    {
        if (this.photonView.isMine)
        {
            if (MMS.finish)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }

            if (backcamera == null)
            {
                backcamera = this.transform.Find("Main").transform.Find("BackCamera(Clone)").gameObject.GetComponent<Camera>();
            }
            if (realcamera == null)
            {
                realcamera = this.transform.Find("Main").transform.Find("RealCamera(Clone)").gameObject.GetComponent<Camera>();
            }
            MMS.myHP = playerhp;

            if (muteki)
            {
                cleartimer += Time.deltaTime;
                if (cleartimer > 0.2)
                {
                    renm.enabled = !renm.enabled;
                    renc.enabled = renm.enabled;
                    renl.enabled = renm.enabled;
                    renr.enabled = renm.enabled;
                    cleartimer = 0;
                }
                mutekitimer += Time.deltaTime;
                if (mutekitimer > mutekitime)
                {
                    renm.enabled = true;
                    renc.enabled = true;
                    renl.enabled = true;
                    renr.enabled = true;
                    muteki = false;
                    cleartimer = 0;
                    mutekitimer = 0;
                    mutekitime = 0;
                }
            }
            if (stop)
            {
                stoptimer += Time.deltaTime;
                if (stoptimer > stoptime)
                {
                    freezeimage.SetActive(false);
                    stop = false;
                    stoptimer = 0;
                }
            }

            if (playerhp > 0)
            {
                if (keycol)
                {
                    KeyMove();
                }
                else
                {
                    Move();
                    if (UPkey)
                    {
                        rb.velocity = transform.forward * runspeed;
                    }
                    if (DOWNkey)
                    {
                        rb.velocity = -transform.forward * runspeed;
                    }
                    if (RIGHTkey)
                    {
                        transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
                    }
                    if (LEFTkey)
                    {
                        transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
                    }
                    if (!UPkey && !DOWNkey)
                    {
                        rb.velocity = new Vector3(0, rb.velocity.y, 0);
                    }
                }
                Shot();
                if (Input.GetKeyDown("c") || MMS.cameraon)
                {
                    CameraWork();
                    MMS.cameraon = false;
                }
            }
            else
            {
                MMS.myHP = 0;
                MMS.myscore -= 30;
                Instantiate(deadsound);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    void KeyMove()
    {
        if (Input.GetKey("up"))
        {
            rb.velocity = transform.forward * runspeed;
        }
        if (Input.GetKey("down"))
        {
            rb.velocity = -transform.forward * runspeed;
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
        }
        if (!Input.GetKey("up") && !Input.GetKey("down"))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    public void Move()
    {
        if (MMS.mymove == 1)
        {
            UPkey = true;
            MMS.mymove = 0;
        }
        if (MMS.mymove == 2)
        {
            DOWNkey = true;
            MMS.mymove = 0;
        }
        if (MMS.mymove == 3)
        {
            UPkey = false;
            MMS.mymove = 0;
        }
        if (MMS.mymove == 4)
        {
            DOWNkey = false;
            MMS.mymove = 0;
        }
        if (MMS.myrotate == 1)
        {
            RIGHTkey = true;
            MMS.myrotate = 0;
        }
        if (MMS.myrotate == 2)
        {
            LEFTkey = true;
            MMS.myrotate = 0;
        }
        if (MMS.myrotate == 3)
        {
            RIGHTkey = false;
            MMS.myrotate = 0;
        }
        if (MMS.myrotate == 4)
        {
            LEFTkey = false;
            MMS.myrotate = 0;
        }
    }

    public void CameraWork()
    {
        backcamera.GetComponent<Camera>().depth = backcamera.GetComponent<Camera>().depth % 2 + 1;
        realcamera.GetComponent<Camera>().depth = realcamera.GetComponent<Camera>().depth % 2 + 1;
    }

    void Shot()
    {
        shottimer += Time.deltaTime;
        if (shottimer > shottime)
        {
            GameObject bulletcopy = PhotonNetwork.Instantiate("OnlineBullet", this.transform.position + this.transform.forward * 1.9f, this.transform.rotation, 0);
            bulletscript = bulletcopy.GetComponent<OnlineBulletScript>();
            bulletscript.reflimit = refcounter;
            bulletscript.bullife = bullifecounter;
            bulletscript.bulletpower = power;
            shottimer = 0;
        }
    }

    public void PowerUP()
    {
        Instantiate(getsound);
        if (itemsetcount == 0)
        {
            itemnumber = 0;
        }
        else
        {
            itemnumber = itemsetnumber[Random.Range(0, itemsetcount)];
        }
        if (itemnumber == 0)
        {
            MMS.WriteMessage("スコア+50!");
        }
        if (itemnumber == 1)
        {
            playerhp = Mathf.Min(3, playerhp + 2);
            MMS.WriteMessage("体力回復!");
        }
        if (itemnumber == 2)
        {
            runspeed *= 1.1f;
            rotatespeed *= 1.1f;
            MMS.WriteMessage("機動力UP!");
        }
        if (itemnumber == 3)
        {
            power += 0.5f;
            MMS.WriteMessage("攻撃力UP!");
        }
        if (itemnumber == 4)
        {
            shottime *= 0.9f;
            MMS.WriteMessage("連射力UP!");
        }
        if (itemnumber == 5)
        {
            bullifecounter++;
            MMS.WriteMessage("貫通力UP!");
        }
        if (itemnumber == 6)
        {
            refcounter++;
            MMS.WriteMessage("反発力UP!");
        }
        if (itemnumber == 7)
        {
            mutekiplay++;
            MMS.WriteMessage("無敵回数UP!");
        }
        if (itemnumber == 8)
        {
            stopplay++;
            MMS.WriteMessage("静止回数UP!");
        }
    }

    public void ItemPlay(int setnumber)
    {
        if (setnumber == 1)
        {
            setitem = PlayerPrefs.GetInt("ItemSet1");
        }
        if (setnumber == 2)
        {
            setitem = PlayerPrefs.GetInt("ItemSet2");
        }
        if (setnumber == 3)
        {
            setitem = PlayerPrefs.GetInt("ItemSet3");
        }
        if (setitem == 7)
        {
            ItemMuteki();
        }
        if (setitem == 8)
        {
            ItemStop();
        }
    }

    void ItemMuteki()
    {
        if (mutekiplay > 0)
        {
            if (!muteki)
            {
                mutekiplay--;
                muteki = true;
                mutekitime = itemmutekitime;
            }
        }
    }

    void ItemStop()
    {
        if (stopplay > 0)
        {
            if (!stop)
            {
                freezeimage.SetActive(true);
                stopplay--;
                stop = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.photonView.isMine)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                float power = collision.gameObject.GetComponent<OnlineBulletScript>().bulletpower;
                if (!muteki)
                {
                    muteki = true;
                    mutekitime = enemymutekitime;
                    if (!collision.gameObject.GetComponent<PhotonView>().isMine)
                    {
                        playerhp -= power;
                    }
                    if (playerhp > 0)
                    {
                        Instantiate(damagesound);
                    }
                }
            }
            if(collision.gameObject.tag == "Enemy")
            {
                if (!muteki)
                {
                    muteki = true;
                    mutekitime = enemymutekitime;
                    playerhp--;
                    if (playerhp > 0)
                    {
                        Instantiate(damagesound);
                    }
                }
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playerhp);
            stream.SendNext(muteki);
        }
        else
        {
            playerhp = (float)stream.ReceiveNext();
            muteki = (bool)stream.ReceiveNext();
        }
    }
}
