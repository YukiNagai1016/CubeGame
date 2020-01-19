using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {
    
	void Start () {
        DontDestroyOnLoad(this);
        Destroy(this.gameObject, 1);
	}
}
