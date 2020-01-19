using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    
    PlayerScript playerscript;

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, 0, 5);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerscript = other.gameObject.GetComponent<PlayerScript>();
            playerscript.CoinUP();
            Destroy(this.gameObject);
        }
    }
}
