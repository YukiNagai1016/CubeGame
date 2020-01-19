using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class RankingScript : MonoBehaviour {

    public GameObject noscorepanel;
    public GameObject cameracenter;
    public Text[] nametext=new Text[5];
    public Text[] scoretext=new Text[5];
    public Image[] set1 = new Image[5];
    public Image[] set2 = new Image[5];
    public Image[] set3 = new Image[5];
    public Text[] mynametext = new Text[5];
    public Text[] myscoretext = new Text[5];
    public Text[] myranktext = new Text[5];
    public Image[] myset1 = new Image[5];
    public Image[] myset2 = new Image[5];
    public Image[] myset3 = new Image[5];
    public Sprite[] item = new Sprite[9];

    void Start () {
        MakeTopRanking();
        MakeMyRanking();
	}
	
	void Update () {
        cameracenter.transform.rotation *= Quaternion.Euler(0, 60 * Time.deltaTime, 0);
	}

    void MakeTopRanking()
    {
        NCMBQuery<NCMBObject> topquery = new NCMBQuery<NCMBObject>("OnlineRanking");
        topquery.OrderByDescending("Score");
        topquery.Limit = 5;
        topquery.FindAsync((List<NCMBObject> objectlist, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("Miss");
            }
            else
            {
                for(int i = 0; i < 5; i++)
                {
                    nametext[i].text = System.Convert.ToString(objectlist[i]["UserName"]);
                    scoretext[i].text = System.Convert.ToString(objectlist[i]["Score"]);
                    set1[i].sprite = item[System.Convert.ToInt32(objectlist[i]["Item1"])];
                    set2[i].sprite = item[System.Convert.ToInt32(objectlist[i]["Item2"])];
                    set3[i].sprite = item[System.Convert.ToInt32(objectlist[i]["Item3"])];
                }
            }
        });
    }

    void MakeMyRanking()
    {
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            noscorepanel.SetActive(true);
        }
        else
        {
            noscorepanel.SetActive(false);
            int myrank = 0;

            NCMBQuery<NCMBObject> myquery = new NCMBQuery<NCMBObject>("OnlineRanking");
            myquery.OrderByDescending("Score");
            myquery.FindAsync((List<NCMBObject> objectlist, NCMBException e) =>
            {
                if (e != null)
                {
                    Debug.Log("Miss");
                }
                else
                {
                    for (int i = 0; objectlist[i].ObjectId != PlayerPrefs.GetString("ID"); i++)
                    {
                        myrank++;
                        Debug.Log(myrank);
                    }
                    if (myrank < 3)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            mynametext[i].text = System.Convert.ToString(objectlist[i]["UserName"]);
                            myscoretext[i].text = System.Convert.ToString(objectlist[i]["Score"]);
                            myset1[i].sprite = item[System.Convert.ToInt32(objectlist[i]["Item1"])];
                            myset2[i].sprite = item[System.Convert.ToInt32(objectlist[i]["Item2"])];
                            myset3[i].sprite = item[System.Convert.ToInt32(objectlist[i]["Item3"])];
                            myranktext[i].text = i + 1.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            mynametext[i].text = System.Convert.ToString(objectlist[myrank - 2 + i]["UserName"]);
                            myscoretext[i].text = System.Convert.ToString(objectlist[myrank - 2 + i]["Score"]);
                            myset1[i].sprite = item[System.Convert.ToInt32(objectlist[myrank - 2 + i]["Item1"])];
                            myset2[i].sprite = item[System.Convert.ToInt32(objectlist[myrank - 2 + i]["Item2"])];
                            myset3[i].sprite = item[System.Convert.ToInt32(objectlist[myrank - 2 + i]["Item3"])];
                            myranktext[i].text = (myrank - 1 + i).ToString();
                        }
                    }
                }
            });
        }
    }
}
