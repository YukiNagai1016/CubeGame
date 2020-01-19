using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaBulletScript : MonoBehaviour {

    public float bulletspeed = 10;
    public GameObject gacha;
    public GameObject getsound;
    public GameObject cancelsound;
    GachaScript gachascript;

	void Start ()
    {
        gachascript = gacha.gameObject.GetComponent<GachaScript>();
    }
	
	void Update () {
        if (this.transform.position.z>-25)
        {
            this.transform.position += new Vector3(0, 0, bulletspeed * Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Instantiate(getsound);
            Destroy(this.gameObject);
            other.gameObject.GetComponent<GachaItemScript>().GachaHit();
        }
        if (other.gameObject.tag == "Block")
        {
            Instantiate(cancelsound);
            this.transform.position = new Vector3(0, 1, -30);
            gachascript.gachatime = 4;
        }
    }
}
