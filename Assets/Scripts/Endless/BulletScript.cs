using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float bulletspeed = 20;
    public float bulletpower = 1;
    public int reflimit = 0;
    public int bullife = 1;
    public GameObject shotsound;

    int refcount;
    Vector3 direction;
    EnemyScript enemyscript;

    void Start () {
        direction = transform.forward;
        Destroy(this.gameObject, 4);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * bulletspeed * Time.deltaTime;
	}

    void OnCollisionEnter(Collision collision)
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
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemyscript = collision.gameObject.GetComponent<EnemyScript>();
            enemyscript.Damage(bulletpower);
            bullife--;
            Instantiate(shotsound);
            if (bullife <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
        }
    }
}
