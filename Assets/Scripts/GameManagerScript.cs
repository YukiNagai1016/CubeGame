using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManagerScript : MonoBehaviour {

    public GameObject[] systemsound=new GameObject[2];

	void Start () {
        if (!PlayerPrefs.HasKey("UpDate"))
        {
            PlayerPrefs.SetFloat("UpDate", 2.0f);
            DataReset();
        }
        DataSet();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void DataSet()
    {
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 3);
        }
        if (!PlayerPrefs.HasKey("Heart"))
        {
            PlayerPrefs.SetInt("Heart", 0);
        }
        if (!PlayerPrefs.HasKey("Item"))
        {
            PlayerPrefs.SetInt("Item", 0);
        }
        if (!PlayerPrefs.HasKey("ItemSet1"))
        {
            PlayerPrefs.SetInt("ItemSet1", 0);
        }
        if (!PlayerPrefs.HasKey("ItemSet2"))
        {
            PlayerPrefs.SetInt("ItemSet2", 0);
        }
        if (!PlayerPrefs.HasKey("ItemSet3"))
        {
            PlayerPrefs.SetInt("ItemSet3", 0);
        }
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        if (!PlayerPrefs.HasKey("HighSet1"))
        {
            PlayerPrefs.SetInt("HighSet1", 0);
        }
        if (!PlayerPrefs.HasKey("HighSet2"))
        {
            PlayerPrefs.SetInt("HighSet2", 0);
        }
        if (!PlayerPrefs.HasKey("HighSet3"))
        {
            PlayerPrefs.SetInt("HighSet3", 0);
        }
        if (!PlayerPrefs.HasKey("SceneShift"))
        {
            PlayerPrefs.SetInt("SceneShift", 0);
        }
    }
    public void DataReset()
    {
        PlayerPrefs.SetInt("Coin", 3);
        PlayerPrefs.SetInt("Item", 0);
        PlayerPrefs.SetInt("Heart", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("ItemSet1", 0);
        PlayerPrefs.SetInt("ItemSet2", 0);
        PlayerPrefs.SetInt("ItemSet3", 0);
        PlayerPrefs.DeleteKey("Name");
        ToTitle(0);
    }

    public void ToTitle(int n)
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("SceneShift", n);
        SceneManager.LoadScene(0);
    }
    public void ToGame()
    {
        Destroy(GameObject.Find("TitleBGM(Clone)"));
        SceneManager.LoadScene(1);
    }
    public void ToGacha()
    {
        SceneManager.LoadScene(3);
    }
    public void ToScore()
    {
        SceneManager.LoadScene(4);
    }
    public void ToRanking()
    {
        SceneManager.LoadScene(5);
    }
    public void ToOnlineRoom()
    {
        SceneManager.LoadScene(8);
    }

    public void SystemSE(int n)
    {
        Instantiate(systemsound[n]);
    }
    
    public bool HasItemCheck(int n)
    {
        int a = PlayerPrefs.GetInt("Item") % (int)Mathf.Pow(2, n);
        if (a - (a % (int)Mathf.Pow(2, n - 1)) == (int)Mathf.Pow(2, n - 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
