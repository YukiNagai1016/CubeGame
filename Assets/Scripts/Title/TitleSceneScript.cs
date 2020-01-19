using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class TitleSceneScript : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject systemse;
    public GameObject startimage;
    public GameObject cameracenter;
    public GameObject namepanel;
    public GameObject namecancelbutton;
    public GameObject taptostart;
    public GameObject titles;
    public GameObject homeframe;
    public GameObject howtoplaypanel;
    public GameObject howtoclosebutton;
    public GameObject[] homespage = new GameObject[3];
    public GameObject[] tankspage = new GameObject[3];
    public GameObject[] gamespage = new GameObject[3];
    public GameObject[] howtoplaypages;
    public Button[] homesbutton = new Button[3];
    public Button[] gamesbutton = new Button[3];
    public Button[] haveitem = new Button[8];
    public Button[] setitem = new Button[3];
    public Image homepanel;
    public Sprite[] itemimage = new Sprite[9];
    public Sprite[] homesbuttonsprite = new Sprite[2];
    public InputField nameinput;
    public Text connecttext;
    public Text playername;
    public Text homeplayername;
    public Text highscoretext;
    public Text cointext;
    public Text hearttext;

    int scenetime;
    float startimagetimer1;
    float startimagetimer2;
    float connectmessagetimer;
    bool connectmessage;
    GameManagerScript gmcs;

    void Start()
    {
        gmcs = gamemanager.GetComponent<GameManagerScript>();
        SceneStart(PlayerPrefs.GetInt("SceneShift"));
        for(int i = 0; i < 8; i++)
        {
            if (gmcs.HasItemCheck(i + 1))
            {
                haveitem[i].GetComponent<Image>().sprite = itemimage[i + 1];
            }
            else
            {
                haveitem[i].GetComponent<Image>().sprite = itemimage[0];
            }
        }
        for(int i = 1; i <= 3; i++)
        {
            setitem[i - 1].GetComponent<Image>().sprite = itemimage[PlayerPrefs.GetInt("ItemSet" + i.ToString())];
        }
    }
    

    void Update()
    {
        //commonscript
        if (connectmessage)
        {
            connectmessagetimer += Time.deltaTime;
            if (connectmessagetimer > 3)
            {
                connecttext.text = "";
                connectmessage = false;
                connectmessagetimer = 0;
            }
        }
        cameracenter.transform.rotation *= Quaternion.Euler(0, 60 * Time.deltaTime, 0);
        if (PlayerPrefs.HasKey("Name"))
        {
            playername.text = "PlayerName:" + PlayerPrefs.GetString("Name");
            homeplayername.text = "Name:" + PlayerPrefs.GetString("Name");
        }
        else
        {
            playername.text = "";
            homeplayername.text = "Name:";
        }
        highscoretext.text = "High Score:" + PlayerPrefs.GetInt("HighScore").ToString();
        cointext.text = "×" + PlayerPrefs.GetInt("Coin").ToString();
        hearttext.text = "×" + PlayerPrefs.GetInt("Heart").ToString();

        //scenetime0
        if (scenetime == 0)
        {
            startimagetimer1 += Time.deltaTime;
            if (startimagetimer1 > 0.5f)
            {
                startimage.SetActive(!startimage.activeInHierarchy);
                startimagetimer1 = 0;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(systemse);
                scenetime = 1;
                startimagetimer1 = 0;
            }
        }

        

        //scenetime1
        if (scenetime == 1)
        {
            if (startimagetimer1 < 1.2f)
            {
                startimagetimer1 += Time.deltaTime;
                startimagetimer2 += Time.deltaTime;
                if (startimagetimer2 > 0.1f)
                {
                    startimage.SetActive(!startimage.activeInHierarchy);
                    startimagetimer2 = 0;
                }
            }
            else
            {
                taptostart.SetActive(false);
                if (!PlayerPrefs.HasKey("Name"))
                {
                    if (!namepanel.activeInHierarchy)
                    {
                        namepanel.SetActive(true);
                        namecancelbutton.SetActive(false);
                    }
                }
                else if (PlayerPrefs.HasKey("Name") && howtoplaypanel.activeInHierarchy == false)
                {
                    scenetime = 2;
                    homepanel.gameObject.SetActive(true);
                    titles.SetActive(false);
                }
            }
        }

        //scenetime2
        if (scenetime == 2)
        {
            if (homepanel.rectTransform.sizeDelta.x < 2208)
            {
                if (homepanel.rectTransform.sizeDelta.x + 6000 * Time.deltaTime < 2208)
                {
                    homepanel.rectTransform.sizeDelta += new Vector2(6000 * Time.deltaTime, 0);
                }
                else
                {
                    homepanel.rectTransform.sizeDelta = new Vector2(2208, 100);
                }
            }
            else if (homepanel.rectTransform.sizeDelta.y < 1242)
            {
                if (homepanel.rectTransform.sizeDelta.y + 3000 * Time.deltaTime < 1242)
                {
                    homepanel.rectTransform.sizeDelta += new Vector2(0, 3000 * Time.deltaTime);
                }
                else
                {
                    homepanel.rectTransform.sizeDelta = new Vector2(2208, 1242);
                }
            }
            else
            {
                homeframe.transform.localScale = new Vector3(1, 1, 1);
                homepanel.rectTransform.sizeDelta = new Vector2(2208, 1242);
                scenetime = 3;
            }
            if (homeframe.transform.localScale.y > 1 && homepanel.rectTransform.sizeDelta.y > 400)
            {
                homeframe.transform.localScale -= new Vector3(0, 5 * Time.deltaTime, 0);
            }
        }

        //scenetime3
        if (scenetime == 3)
        {
            HomesPageShift(0);
            scenetime = 4;
        }
    }

    void SceneStart(int n)
    {
        if (n != 0)
        {
            scenetime = 4;
            homepanel.gameObject.SetActive(true);
            titles.SetActive(false);
            homeframe.transform.localScale = new Vector3(1, 1, 1);
            homepanel.rectTransform.sizeDelta = new Vector2(2208, 1242);
            int m = n % 10;
            if (n - m == 10)
            {
                HomesPageShift(0);
                TanksPageShift(m);
            }
            if (n - m == 20)
            {
                HomesPageShift(1);
                GamesPageShift(m);
            }
            PlayerPrefs.SetInt("SceneShift", 0);
        }
    }

    public void ItemSetting(int n)
    {
        if (gmcs.HasItemCheck(n))
        {
            if (PlayerPrefs.GetInt("ItemSet1") != n)
            {
                if (PlayerPrefs.GetInt("ItemSet2") != n)
                {
                    if (PlayerPrefs.GetInt("ItemSet3") != n)
                    {
                        if (PlayerPrefs.GetInt("ItemSet1") == 0)
                        {
                            PlayerPrefs.SetInt("ItemSet1", n);
                            setitem[0].GetComponent<Image>().sprite = itemimage[n];
                        }
                        else if (PlayerPrefs.GetInt("ItemSet2") == 0)
                        {
                            PlayerPrefs.SetInt("ItemSet2", n);
                            setitem[1].GetComponent<Image>().sprite = itemimage[n];
                        }
                        else if (PlayerPrefs.GetInt("ItemSet3") == 0)
                        {
                            PlayerPrefs.SetInt("ItemSet3", n);
                            setitem[2].GetComponent<Image>().sprite = itemimage[n];
                        }
                    }
                }
            }
        }
    }

    public void ItemUnSetting(int n)
    {
        PlayerPrefs.SetInt("ItemSet" + n.ToString(), 0);
        setitem[n - 1].GetComponent<Image>().sprite = itemimage[0];
    }

    public void NameChange()
    {
        nameinput.text = PlayerPrefs.GetString("Name");
        namepanel.SetActive(true);
        namecancelbutton.SetActive(true);
    }

    public void NameInput()
    {
        Instantiate(systemse);
        if (nameinput.text == "")
        {
            connecttext.text = "名前を入力してください";
            connectmessage = true;
            connectmessagetimer = 0;
        }
        else
        {
            if (PlayerPrefs.HasKey("Name"))
            {
                NameSet();
            }
            else if (!PlayerPrefs.HasKey("Name"))
            {
                IDSet();
            }
        }
    }
    
    void NameSet()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");
        query.WhereEqualTo("objectId", PlayerPrefs.GetString("ID"));
        query.FindAsync((List<NCMBObject> objectlist, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("Miss");
                connecttext.text = "接続に失敗しました";
                connectmessage = true;
                connectmessagetimer = 0;
            }
            else
            {
                Debug.Log("ObjectID=" + objectlist[0].ObjectId);
                PlayerPrefs.SetString("Name", nameinput.text);
                objectlist[0]["UserName"] = PlayerPrefs.GetString("Name");
                objectlist[0].SaveAsync();
                connecttext.text="";
                namepanel.SetActive(false);
            }
        });
    }

    void IDSet()
    {
        NCMBObject onlineRanking = new NCMBObject("OnlineRanking");
        onlineRanking["UserName"] = nameinput.text;
        onlineRanking["Score"] = 0;
        onlineRanking["Item1"] = 0;
        onlineRanking["Item2"] = 0;
        onlineRanking["Item3"] = 0;
        onlineRanking.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("Miss");
                connecttext.text = "接続に失敗しました";
                connectmessage = true;
                connectmessagetimer = 0;
            }
            else
            {
                Debug.Log("ObjectID=" + onlineRanking.ObjectId);
                PlayerPrefs.SetString("ID", onlineRanking.ObjectId);
                howtoclosebutton.SetActive(false);
                howtoplaypanel.SetActive(true);
                connecttext.text = "";
                namepanel.SetActive(false);
                PlayerPrefs.SetString("Name", nameinput.text);
            }
        });
    }

    public void HomesPageShift(int n)
    {
        for(int i = 0; i < 3; i++)
        {
            homespage[i].SetActive(false);
            homesbutton[i].GetComponent<Image>().sprite = homesbuttonsprite[0];
        }
        homespage[n].SetActive(true);
        homesbutton[n].GetComponent<Image>().sprite = homesbuttonsprite[1];
        TanksPageShift(0);
        GamesPageShift(0);
    }

    public void TanksPageShift(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            tankspage[i].SetActive(false);
        }
        tankspage[n].SetActive(true);
    }
    
    public void GamesPageShift(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            gamespage[i].SetActive(false);
            gamesbutton[i].GetComponent<RectTransform>().sizeDelta = new Vector2(800, 200);
        }
        gamespage[n].SetActive(true);
        gamesbutton[n].GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 250);
    }

    public void HowToPlayPageShift(int n)
    {
        for(int i = 0; i < howtoplaypages.Length; i++)
        {
            howtoplaypages[i].SetActive(false);
        }
        howtoplaypages[n-1].SetActive(true);
    }
}
