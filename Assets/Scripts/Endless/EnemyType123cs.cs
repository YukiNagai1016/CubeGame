using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType123cs : MonoBehaviour {

    public float movespeed = 4;
    GameObject Player;
    PlayerScript playerscript;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        playerscript = Player.gameObject.GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerscript.stop)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (!playerscript.stop)
        {
            rb.velocity = transform.forward * movespeed;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            this.transform.rotation=Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
        if (collision.gameObject.tag == "Player")
        {
            if (!playerscript.stop)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(this.transform.position.x - collision.transform.position.x, 0, this.transform.position.z - collision.transform.position.z));
            }
        }
    }
}
