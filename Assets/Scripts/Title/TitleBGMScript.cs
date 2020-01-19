using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGMScript : MonoBehaviour {

    public GameObject titlebgm;

	void Start () {
        if (GameObject.Find("TitleBGM(Clone)") == null)
        {
            GameObject bgm = Instantiate(titlebgm);
            DontDestroyOnLoad(bgm);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
