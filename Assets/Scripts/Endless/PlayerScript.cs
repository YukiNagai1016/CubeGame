using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public float runspeed = 3;
    public float rotatespeed = 60;
    public float shottime = 1;
    public float playerhp = 10;
    public float enemymutekitime = 2;
    public float itemmutekitime = 7;
    public float mutekitime;
    public float stoptime = 5;
    public float power = 1;
    public bool muteki;
    public bool stop;
    public bool pause;
    public bool keycol;
    public bool UPkey;
    public bool DOWNkey;
    public bool RIGHTkey;
    public bool LEFTkey;
    public int mutekiplay;
    public int stopplay;
    public GameObject titlebutton;
    public GameObject pausebutton;
    public GameObject pausetext;
    public GameObject bullet;
    public GameObject scoretext;
    public GameObject main;
    public GameObject canon;
    public GameObject catL;
    public GameObject catR;
    public GameObject freezeimage;
    public GameObject getsound;
    public GameObject damagesound;
    public GameObject deadsound;
    public GameObject gameoverpanel;
    public GameObject systemse;
    public Camera backcamera;
    public Camera realcamera;
    public Camera fieldcamera;
    public Slider hpslider;
    public Text messagetext;
    public Text pausebuttontext;
    public Text hearttext;
    public Text gameovertimertext;

    float texttimer;
    float mutekitimer;
    float cleartimer;
    float stoptimer;
    float shottimer;
    float deadtimer;
    float gameovertimer;
    bool texton;
    bool revived;
    bool dead;
    bool gameover;
    int refcounter = 0;
    int bullifecounter = 1;
    int itemnumber;
    int setitem;
    int itemsetcount;
    int[] itemsetnumber = new int[3];
    int gameovertime = 15;
    BulletScript bulletscript;
    ScoreScript scorescript;
    Renderer renm;
    Renderer renc;
    Renderer renl;
    Renderer renr;
    Rigidbody rb;
    BoxCollider bc;
    
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        bc = this.GetComponent<BoxCollider>();
        renm = main.GetComponent<Renderer>();
        renc = canon.GetComponent<Renderer>();
        renl = catL.GetComponent<Renderer>();
        renr = catR.GetComponent<Renderer>();
        scorescript = scoretext.GetComponent<ScoreScript>();

        for(int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("ItemSet" + i.ToString()) != 0)
            {
                itemsetnumber[itemsetcount] = PlayerPrefs.GetInt("ItemSet" + i.ToString());
                itemsetcount++;
            }
        }
    }
	
	void Update ()
    {

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

        hpslider.value = playerhp;
        if (!gameover)
        {
            if (playerhp > 0)
            {
                if (keycol)
                {
                    KeyMove();
                }
                else
                {
                    if (!keycol)
                    {
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
                }
                Shot();
                if (Input.GetKeyDown("c"))
                {
                    CameraWork();
                }
            }
            else
            {
                if (!dead)
                {
                    Instantiate(deadsound);
                    main.gameObject.SetActive(false);
                    bc.enabled = false;
                    rb.useGravity = false;
                    pausebutton.SetActive(false);
                    hpslider.gameObject.SetActive(false);
                    dead = true;
                    freezeimage.SetActive(false);
                    stop = false;
                    stoptimer = 0;
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                    backcamera.GetComponent<Camera>().depth = 2;
                    realcamera.GetComponent<Camera>().depth = 1;
                    fieldcamera.GetComponent<Camera>().depth = 0;
                }
                else
                {
                    deadtimer += Time.deltaTime;
                    if (deadtimer > 2)
                    {
                        Dead();
                    }
                }
            }
        }
        else
        {
            gameovertimer += Time.deltaTime;
            if (gameovertimer > 1)
            {
                gameovertime--;
                gameovertimertext.text = gameovertime.ToString();
                gameovertimer = 0;
            }
            if (gameovertime <= 0)
            {
                scorescript.DeadToScore();
            }
        }

        if (texton)
        {
            texttimer += Time.deltaTime;
            if (texttimer > 5)
            {
                texttimer = 0;
                texton = false;
                messagetext.text = "";
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

    public void Move(int n)
    {
        if (n == 1)
        {
            UPkey = true;
        }
        if (n == 2)
        {
            DOWNkey = true;
        }
        if (n == 3)
        {
            RIGHTkey = true;
        }
        if (n == 4)
        {
            LEFTkey = true;
        }
        if (n == 5)
        {
            UPkey = false;
        }
        if (n == 6)
        {
            DOWNkey = false;
        }
        if (n == 7)
        {
            RIGHTkey = false;
        }
        if (n == 8)
        {
            LEFTkey = false;
        }
    }

    public void CameraWork()
    {
        backcamera.GetComponent<Camera>().depth = (backcamera.GetComponent<Camera>().depth + 1) % 3;
        realcamera.GetComponent<Camera>().depth = (realcamera.GetComponent<Camera>().depth + 1) % 3;
        fieldcamera.GetComponent<Camera>().depth = (fieldcamera.GetComponent<Camera>().depth + 1) % 3;
    }

    void Shot()
    {
        shottimer += Time.deltaTime;
        if (shottimer > shottime)
        {

            GameObject bulletcopy = Instantiate(bullet, this.transform.position + this.transform.forward * 1.9f, this.transform.rotation);
            bulletscript = bulletcopy.gameObject.GetComponent<BulletScript>();
            bulletscript.reflimit = refcounter;
            bulletscript.bullife = bullifecounter;
            bulletscript.bulletpower = power;
            shottimer = 0;
        }
    }

    void Dead()
    {
        if (!revived)
        {
            hearttext.text = "×" + PlayerPrefs.GetInt("Heart").ToString();
            gameoverpanel.SetActive(true);
            gameover = true;
        }
        else
        {
            scorescript.DeadToScore();
        }
    }
    
    public void Revive()
    {
        if (PlayerPrefs.GetInt("Heart") == 0)
        {
            return;
        }
        PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart") - 1);
        playerhp = 5;
        deadtimer = 0;
        main.gameObject.SetActive(true);
        bc.enabled = true;
        rb.useGravity = true;
        pausebutton.SetActive(true);
        hpslider.gameObject.SetActive(true);
        gameoverpanel.SetActive(false);
        revived = true;
        muteki = true;
        mutekitime = 3;
        dead = false;
        deadtimer = 0;
        Instantiate(systemse);
        gameover = false;
    }

    public void Damage(float damage)
    {
        if (!muteki)
        {
            muteki = true;
            mutekitime = enemymutekitime;
            playerhp -= damage;
            if (playerhp > 0)
            {
                Instantiate(damagesound);
            }
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
            scorescript.Score(50);
            texton = true;
            texttimer = 0;
            messagetext.text = "スコア+50!";
        }
        if (itemnumber == 1)
        {
            if (playerhp <= 8)
            {
                playerhp += 2;
            }
            else
            {
                playerhp = 10;
            }
            texton = true;
            texttimer = 0;
            messagetext.text = "体力回復!";
        }
        if (itemnumber == 2)
        {
            runspeed *= 1.1f;
            rotatespeed *= 1.1f;
            texton = true;
            texttimer = 0;
            messagetext.text = "機動力UP!";
        }
        if (itemnumber == 3)
        {
            power += 0.5f;
            texton = true;
            texttimer = 0;
            messagetext.text = "攻撃力UP!";
        }
        if (itemnumber == 4)
        {
            shottime *= 0.9f;
            texton = true;
            texttimer = 0;
            messagetext.text = "連射力UP!";
        }
        if (itemnumber == 5)
        {
            bullifecounter++;
            texton = true;
            texttimer = 0;
            messagetext.text = "貫通力UP!";
        }
        if (itemnumber == 6)
        {
            refcounter++;
            texton = true;
            texttimer = 0;
            messagetext.text = "反発力UP!";
        }
        if (itemnumber == 7)
        {
            mutekiplay++;
            texton = true;
            texttimer = 0;
            messagetext.text = "無敵回数UP!";
        }
        if (itemnumber == 8)
        {
            stopplay++;
            texton = true;
            texttimer = 0;
            messagetext.text = "静止回数UP!";
        }
    }

    public void CoinUP()
    {
        Instantiate(getsound);
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
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

    public void Pause()
    {
        if (pause==true)
        {
            Time.timeScale = 1f;
            titlebutton.SetActive(false);
            pausetext.SetActive(false);
            pausebuttontext.text = "Pause";
            pause = false;
        }
        else
        {
            Time.timeScale = 0f;
            titlebutton.SetActive(true);
            pausetext.SetActive(true);
            pausebuttontext.text = "Back";
            pause = true;
        }
    }
}
