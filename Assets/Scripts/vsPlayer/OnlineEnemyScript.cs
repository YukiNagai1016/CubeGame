using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineEnemyScript : Photon.PunBehaviour
{
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * 4;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            PhotonNetwork.Destroy(this.gameObject);
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
            this.transform.rotation = Quaternion.LookRotation(new Vector3(this.transform.position.x - collision.transform.position.x, 0, this.transform.position.z - collision.transform.position.z));
        }
    }
}
