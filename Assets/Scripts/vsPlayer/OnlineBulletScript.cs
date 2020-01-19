using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineBulletScript : Photon.PunBehaviour
{

    public float bulletspeed = 20;
    public float bulletpower = 1;
    public int reflimit = 0;
    public int bullife = 1;
    public GameObject shotsound;

    int refcount;
    float deathtimer;
    Vector3 direction;
    MyManagerScript MMS;
    OnlinePlayerScript OPS;

    void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.photonView.isMine)
        {
            transform.position += direction * bulletspeed * Time.deltaTime;
            deathtimer += Time.deltaTime;
            if (deathtimer > 4)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (this.photonView.isMine)
        {
            if (collision.gameObject.tag == "Field")
            {
                if (refcount < reflimit)
                {
                    ContactPoint point = collision.contacts[0];
                    Vector3 normal = point.normal;
                    direction += 2 * Vector3.Dot(-direction, normal) * normal;
                    refcount++;
                }
                else
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject.Find("MyManager").GetComponent<MyManagerScript>().myscore += 10;
                Instantiate(shotsound);
                bullife--;
                if (bullife <= 0)
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
            if (collision.gameObject.tag == "Block")
            {
                Destroy(this.gameObject);
            }
            if(collision.gameObject.tag == "Player")
            {
                if (!collision.gameObject.GetComponent<PhotonView>().isMine)
                {
                    OPS = collision.gameObject.GetComponent<OnlinePlayerScript>();
                    if (!OPS.muteki)
                    {
                        if (OPS.playerhp <= bulletpower)
                        {
                            GameObject.Find("MyManager").GetComponent<MyManagerScript>().myscore += 100;
                        }
                        Instantiate(shotsound);
                    }
                    bullife--;
                    if (bullife <= 0)
                    {
                        PhotonNetwork.Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(bulletpower);
        }
        else
        {
            bulletpower = (float)stream.ReceiveNext();
        }
    }
}
