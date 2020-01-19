using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScenecs : MonoBehaviour {

    public Text scoretext;
    public Text hiscoretext;
    public Text nametext;
    public Sprite item0;
    public Sprite item1;
    public Sprite item2;
    public Sprite item3;
    public Sprite item4;
    public Sprite item5;
    public Sprite item6;
    public Sprite item7;
    public Sprite item8;
    public Image itemset1;
    public Image itemset2;
    public Image itemset3;
    public Image highset1;
    public Image highset2;
    public Image highset3;

    void Start () {
        nametext.text = PlayerPrefs.GetString("Name");
        scoretext.text = PlayerPrefs.GetInt("Score").ToString();
        hiscoretext.text = PlayerPrefs.GetInt("HighScore").ToString();
        ItemSet(itemset1, PlayerPrefs.GetInt("ItemSet1"));
        ItemSet(itemset2, PlayerPrefs.GetInt("ItemSet2"));
        ItemSet(itemset3, PlayerPrefs.GetInt("ItemSet3"));
        ItemSet(highset1, PlayerPrefs.GetInt("HighSet1"));
        ItemSet(highset2, PlayerPrefs.GetInt("HighSet2"));
        ItemSet(highset3, PlayerPrefs.GetInt("HighSet3"));
    }
	
	void Update () {

	}

    void ItemSet(Image itemset,int n)
    {
        if (n == 0)
        {
            itemset.sprite = item0;
        }
        if (n == 1)
        {
            itemset.sprite = item1;
        }
        if (n == 2)
        {
            itemset.sprite = item2;
        }
        if (n == 3)
        {
            itemset.sprite = item3;
        }
        if (n == 4)
        {
            itemset.sprite = item4;
        }
        if (n == 5)
        {
            itemset.sprite = item5;
        }
        if (n == 6)
        {
            itemset.sprite = item6;
        }
        if (n == 7)
        {
            itemset.sprite = item7;
        }
        if (n == 8)
        {
            itemset.sprite = item8;
        }
    }
}
