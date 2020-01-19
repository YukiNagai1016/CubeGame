using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType4cs : MonoBehaviour {

    public float movespeed = 4;
    public float bullettime = 5;
    public float bulletcopyspeed = 8;
    public float bulletcopypower = 1;
    public GameObject bullet;

    float bullettimer;
    Vector3 direction;
    GameObject Player;
    PlayerScript playerscript;
    EnemyBulletScript bulletscript;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>(); ;
        Player = GameObject.Find("Player");
        playerscript = Player.GetComponent<PlayerScript>();
        bullettimer = Random.Range(0, bullettime - 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerscript.stop)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (!playerscript.stop)
        {
            rb.velocity = transform.forward * movespeed;
            if (playerscript.playerhp > 0)
            {
                Shot();
            }
            else
            {
                bullettimer = Random.Range(0, bullettime - 4);
            }
        }
    }

    void Shot()
    {
        bullettimer += Time.deltaTime;
        if (bullettimer > bullettime)
        {
            direction = (Player.transform.position - this.transform.position).normalized;
            GameObject bulletcopy = Instantiate(bullet, this.transform.position + 2 * direction, Quaternion.LookRotation(direction));
            bulletscript = bulletcopy.GetComponent<EnemyBulletScript>();
            bulletscript.playercs = playerscript;
            bulletscript.bulletpower = bulletcopypower;
            bulletscript.bulletspeed = bulletcopyspeed;
            bullettimer = 0;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
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
