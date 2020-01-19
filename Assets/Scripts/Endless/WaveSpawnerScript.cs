using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerScript : MonoBehaviour {

    public float wavetime = 60;
    public int enemycount;
    public GameObject player;
    public GameObject stagegenerater;
    public GameObject[] enemys;

    float wavetimer;
    int randomnumber;
    int wavecount;
    int[,] field = new int[10, 10];
    int[] countx = new int[100];
    int[] county = new int[100];
    int[] putx = new int[100];
    int[] puty = new int[100];
    int roadcount;
    PlayerScript playerscript;
    StageGeneraterScript sgcs;

	void Start ()
    {
        wavetimer = wavetime - 5;
        playerscript = player.gameObject.GetComponent<PlayerScript>();
        FieldSet();
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerscript.stop && playerscript.playerhp > 0)
        {
            wavetimer += Time.deltaTime;
        }
        if (wavetimer > wavetime)
        {
            randomnumber = Random.Range(0, Mathf.Min(4,wavecount+1));
            WaveOn(randomnumber);
            wavecount++;
            wavetimer = 0;
            if (wavetime > 15)
            {
                wavetime -= 5;
            }
        }
	}

    void FieldSet()
    {
        sgcs = stagegenerater.GetComponent<StageGeneraterScript>();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                field[x, y] = 0;
                if (sgcs.fieldnumber[x, y] == 0)
                {
                    countx[roadcount] = x;
                    county[roadcount] = y;
                    roadcount++;
                }
            }
        }
    }

    void WaveOn(int n)
    {
        for(int i = 0; i < 100; i++)
        {
            putx[i] = countx[i];
            puty[i] = county[i];
        }

        for (int i = 0; i < Mathf.Min(enemycount+2*wavecount,roadcount); i++)
        {
            int random = Random.Range(0, roadcount - i+1);
            field[putx[random], puty[random]] = 1;
            for (int j = random; j < roadcount; j++)
            {
                putx[j] = putx[j + 1];
                puty[j] = puty[j + 1];
            }
        }


        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (field[x, y] == 1)
                {
                    Instantiate(enemys[n], new Vector3(8 * x, 1, 8 * y), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    field[x, y] = 0;
                }
            }
        }
    }
}
