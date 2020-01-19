using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour {

    float timer;
    int RandomNo;
    PlayerScript playerscript;

	void Start ()
    {

    }
	
	void Update () {
        timer += Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(30*Mathf.Sin(2*timer), 0, 30*Mathf.Cos(2*timer));
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerscript = collision.gameObject.GetComponent<PlayerScript>();
            playerscript.PowerUP();
            Destroy(this.gameObject);
        }
    }
}
