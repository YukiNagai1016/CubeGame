using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaScript : MonoBehaviour {

    public float rotatespeed = 1;
    public int gachatime;
    public GameObject bullet;
    public GameObject group1;
    public GameObject group2;
    public GameObject group3;
    public Text messagetext;
    public Text cointext;
    float timer;

    void Start()
    {

    }

    void Update () {
        cointext.text = PlayerPrefs.GetInt("Coin").ToString();
        if (gachatime == -1)
        {
            this.transform.rotation *= Quaternion.Euler(0, rotatespeed * 30 * Time.deltaTime, 0);
            messagetext.text = "コインが足りません";
            timer += Time.deltaTime;
            if (timer > 2)
            {
                gachatime = 0;
                timer = 0;
                messagetext.text = "１回コイン３枚";
            }
        }
        if (gachatime == 0)
        {
            group1.SetActive(true);
            group2.SetActive(false);
            this.transform.rotation *= Quaternion.Euler(0, rotatespeed * 30 * Time.deltaTime, 0);
        }
        if (gachatime == 1)
        {
            this.transform.rotation *= Quaternion.Euler(0, rotatespeed * 30 * Time.deltaTime, 0);
            if (timer >= 0)
            {
                group1.SetActive(false);
                timer += Time.deltaTime;
                if (timer > 1 + Random.Range(0, 0.5f))
                {
                    group3.SetActive(false);
                    bullet.transform.position = new Vector3(0, 1, -20);
                    timer = -1;
                }
            }
        }
        if (gachatime == 2)
        {

        }
        if (gachatime == 3)
        {

        }
        if (gachatime == 4)
        {
            group2.SetActive(true);
            group3.SetActive(true);
        }
	}

    public  void Gachastart()
    {
        int coinnumber;
        coinnumber = PlayerPrefs.GetInt("Coin");
        if (coinnumber < 3)
        {
            timer = 0;
            gachatime = -1;
        }
        else
        {
            timer = 0;
            gachatime = 1;
            coinnumber -= 3;
            PlayerPrefs.SetInt("Coin", coinnumber);
        }
    }
}
