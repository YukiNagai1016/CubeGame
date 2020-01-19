using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GItemSetScript : MonoBehaviour {

    public Sprite item1;
    public Sprite item2;
    public Sprite item3;
    public Sprite item4;
    public Sprite item5;
    public Sprite item6;
    public Sprite item7;
    public Sprite item8;
    public Sprite itemempty;
    public Text itemplay;
    public GameObject Player;
    public int setnumber;
    int itemset;
    Image itemimage;
    Button buttoncom;
    OnlinePlayerScript playerscript;

    void Start () {
        itemimage = this.GetComponent<Image>();
        buttoncom = this.GetComponent<Button>();
        playerscript = Player.GetComponent<OnlinePlayerScript>();
        if (setnumber == 1)
        {
            itemset = PlayerPrefs.GetInt("ItemSet1");
        }
        if (setnumber == 2)
        {
            itemset = PlayerPrefs.GetInt("ItemSet2");
        }
        if (setnumber == 3)
        {
            itemset = PlayerPrefs.GetInt("ItemSet3");
        }
        if (itemset == 0)
        {
            itemimage.sprite = itemempty;
        }
        if (itemset == 1)
        {
            itemimage.sprite = item1;
        }
        if (itemset == 2)
        {
            itemimage.sprite = item2;
        }
        if (itemset == 3)
        {
            itemimage.sprite = item3;
        }
        if (itemset == 4)
        {
            itemimage.sprite = item4;
        }
        if (itemset == 5)
        {
            itemimage.sprite = item5;
        }
        if (itemset == 6)
        {
            itemimage.sprite = item6;
        }
        if (itemset == 7)
        {
            itemimage.sprite = item7;
        }
        if (itemset == 8)
        {
            itemimage.sprite = item8;
        }
        if (itemset == 7 || itemset == 8)
        {
            buttoncom.enabled = true;
            itemplay.gameObject.SetActive(true);
        }
    }
	
	void Update ()
    {
        if (itemset == 7)
        {
            itemplay.text = playerscript.mutekiplay.ToString();
        }
        if (itemset == 8)
        {
            itemplay.text = playerscript.stopplay.ToString();
        }
    }
}
