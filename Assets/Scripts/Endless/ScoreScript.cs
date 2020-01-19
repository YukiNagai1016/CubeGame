using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class ScoreScript : MonoBehaviour {

    public int nowscore;
    public Text scoretext;

    int dropcount;
    float deadtimer;
    
	void Start ()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
	
	void Update () {
        scoretext.text = nowscore.ToString();
	}

    public void DeadToScore()
    {
        PlayerPrefs.SetInt("Score", nowscore);
        if (nowscore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", nowscore);
            PlayerPrefs.SetInt("HighSet1", PlayerPrefs.GetInt("ItemSet1"));
            PlayerPrefs.SetInt("HighSet2", PlayerPrefs.GetInt("ItemSet2"));
            PlayerPrefs.SetInt("HighSet3", PlayerPrefs.GetInt("ItemSet3"));
        }
        SaveScore();
        SceneManager.LoadScene(4);
    }

    public void Score(int n)
    {
        nowscore += n;
    }

    void SaveScore()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");
        query.WhereEqualTo("objectId", PlayerPrefs.GetString("ID"));
        query.FindAsync((List<NCMBObject> objectlist, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("Miss");
            }
            else
            {
                Debug.Log("ObjectID=" + objectlist[0].ObjectId);
                objectlist[0]["Score"] = PlayerPrefs.GetInt("HighScore");
                objectlist[0]["Item1"] = PlayerPrefs.GetInt("HighSet1");
                objectlist[0]["Item2"] = PlayerPrefs.GetInt("HighSet2");
                objectlist[0]["Item3"] = PlayerPrefs.GetInt("HighSet3");
                objectlist[0].SaveAsync();
            }
        });
    }

    public bool Drop(int n)
    {
        dropcount += n;
        if (Random.Range(0, 400) < dropcount)
        {
            dropcount = 0;
            Debug.Log(dropcount);
            return true;
        }
        else
        {
            Debug.Log(dropcount);
            return false;
        }
    }
}
