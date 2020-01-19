using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float enemyhp = 10;
    public float power = 1;
    public int scorepoint;
    public GameObject item;
    public GameObject coin;
    
    GameObject scoretext;
    PlayerScript playerscript;
    ScoreScript scorescript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyhp <= 0)
        {
            scoretext = GameObject.Find("ScoreText");
            scorescript = scoretext.GetComponent<ScoreScript>();
            scorescript.Score(scorepoint);
            if (scorescript.Drop(scorepoint))
            {
                if (Random.Range(0, 5) < 3)
                {
                    Instantiate(item, this.transform.position + new Vector3(0, 0.2f, 0), Quaternion.Euler(0, 0, 30));
                }
                else
                {
                    Instantiate(coin, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0));
                }
            }
            Destroy(this.gameObject);
        }
	}

    public void Damage(float damage)
    {
        enemyhp -= damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerscript = collision.gameObject.GetComponent<PlayerScript>();
            playerscript.Damage(power);
        }
    }
}
