using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemScript : MonoBehaviour {

    public int itemnumber = 0;
    public float movespeed = 10;
    public string message;
    public GameObject gacha;
    public GameObject systemse;
    public GameObject gamemanager;
    public Sprite heartimage;
    public Text messagetext;

    GachaScript gachascript;
    GameManagerScript gmcs;
    Transform tr;
    float timer;
    float waittime;
    bool hasitem;
    bool hit;

    void Start()
    {
        gmcs = gamemanager.GetComponent<GameManagerScript>();
        gachascript = gacha.gameObject.GetComponent<GachaScript>();
        tr = this.GetComponent<Transform>();
    }
    void Update()
    {
        if (hit)
        {
            if (gachascript.gachatime == 2)
            {
                tr.Rotate(0, 10, 0);
                if (tr.position.x > 3)
                {
                    tr.position -= new Vector3(movespeed * Time.deltaTime, 0, 0);
                }
                if (tr.position.x < 3)
                {
                    tr.position += new Vector3(movespeed * Time.deltaTime, 0, 0);
                }
                if (tr.position.z > -20)
                {
                    tr.position -= new Vector3(0, 0, movespeed * Time.deltaTime);
                }
                if (tr.position.y < 3)
                {
                    tr.position += new Vector3(0, movespeed * Time.deltaTime, 0);
                }
                if (tr.position.y >= 3 && tr.position.z <= -20)
                {
                    gachascript.gachatime = 3;
                }
            }
            if (gachascript.gachatime == 3)
            {
                timer += Time.deltaTime;
                if (timer > waittime)
                {
                    ItemGet(itemnumber);
                    tr.rotation = Quaternion.identity;
                    gachascript.gachatime = 4;
                    if (hasitem)
                    {
                        tr.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        this.GetComponent<SpriteRenderer>().sprite = heartimage;
                        Instantiate(systemse);
                        PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart") + 1);
                        messagetext.text = "復活ハートをGET！";
                    }
                    else
                    {
                        messagetext.text = message + "をGET!";
                    }
                }
                else
                {
                    tr.Rotate(0, 10, 0);
                }
            }
        }
    }

    public void GachaHit()
    {
        hit = true;
        gachascript.gachatime = 2;
        if (tr.position.z > 0)
        {
            waittime = 1;
        }
        else
        {
            waittime = 1.45f;
        }
    }

    public void ItemGet(int n)
    {
        int itemdata;
        itemdata = PlayerPrefs.GetInt("Item");
        if (gmcs.HasItemCheck(n))
        {
            hasitem = true;
        }
        else
        {
            itemdata += (int)Mathf.Pow(2, n - 1);
        }
        PlayerPrefs.SetInt("Item", itemdata);
    }
}
