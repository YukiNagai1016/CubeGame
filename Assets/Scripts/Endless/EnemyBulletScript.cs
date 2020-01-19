using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    public float bulletspeed = 8;
    public float bulletpower = 1;

    public PlayerScript playercs;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!playercs.stop)
        {
            this.transform.position += this.transform.forward * bulletspeed * Time.deltaTime;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            playercs.Damage(bulletpower);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
        }
    }
}
