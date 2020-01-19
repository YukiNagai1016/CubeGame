using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGeneraterScript : MonoBehaviour
{
    public GameObject[] buildings = new GameObject[6];
    public GameObject[] roads = new GameObject[4];

    public int[,] fieldnumber = new int[10, 10];//0:road,1:building


    void Start()
    {
        //すべてのマスを道にする
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                fieldnumber[x, y] = 0;
            }
        }

        //ランダムでマスを建物にする
        for (int x = 1; x <= 4; x++)
        {
            for (int y = 1; y <= 4; y++)
            {
                int fieldset = Random.Range(1, 5);
                if ((x == 1 || x == 4) && (y == 1 || y == 4))
                {
                    if (fieldset == 1)
                    {
                        fieldnumber[2 * x, 2 * y] = 1;
                        fieldnumber[2 * x - 1, 2 * y] = 1;
                    }
                    else if (fieldset == 2)
                    {
                        fieldnumber[2 * x - 1, 2 * y] = 1;
                        fieldnumber[2 * x - 1, 2 * y - 1] = 1;
                    }
                    else if (fieldset == 3)
                    {
                        fieldnumber[2 * x - 1, 2 * y - 1] = 1;
                        fieldnumber[2 * x, 2 * y - 1] = 1;
                    }
                    else if (fieldset == 4)
                    {
                        fieldnumber[2 * x, 2 * y - 1] = 1;
                        fieldnumber[2 * x, 2 * y] = 1;
                    }
                }
                else
                {
                    if (fieldset == 1)
                    {
                        fieldnumber[2 * x, 2 * y] = 1;
                    }
                    else if (fieldset == 2)
                    {
                        fieldnumber[2 * x - 1, 2 * y] = 1;
                    }
                    else if (fieldset == 3)
                    {
                        fieldnumber[2 * x - 1, 2 * y - 1] = 1;
                    }
                    else if (fieldset == 4)
                    {
                        fieldnumber[2 * x, 2 * y - 1] = 1;
                    }
                }
            }
        }
        for(int x = 1; x <= 8; x++)
        {
            if(fieldnumber[x - 1, 1] == 0 && fieldnumber[x, 1] == 0 && fieldnumber[x + 1, 1] == 0)
            {
                fieldnumber[x, 0] = 1;
            }
            if (fieldnumber[x - 1, 8] == 0 && fieldnumber[x, 8] == 0 && fieldnumber[x + 1, 8] == 0)
            {
                fieldnumber[x, 9] = 1;
            }
        }
        for (int y = 1; y <= 8; y++)
        {
            if (fieldnumber[1, y - 1] == 0 && fieldnumber[1, y] == 0 && fieldnumber[1,y + 1] == 0)
            {
                fieldnumber[0, y] = 1;
            }
            if (fieldnumber[8, y - 1] == 0 && fieldnumber[8, y] == 0 && fieldnumber[8, y + 1] == 0)
            {
                fieldnumber[9, y] = 1;
            }
        }

        //道と建物を生成する
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (fieldnumber[x, y] == 1)
                {
                    int roadcount = 0;
                    int[] buildingrotate = new int[4];
                    if (x != 0)
                    {
                        if (fieldnumber[x - 1, y] == 0)
                        {
                            buildingrotate[roadcount]=3;
                            roadcount++;
                        }
                    }
                    if (x != 9)
                    {
                        if (fieldnumber[x + 1, y] == 0)
                        {
                            buildingrotate[roadcount] = 1;
                            roadcount++;
                        }
                    }
                    if (y != 0)
                    {
                        if (fieldnumber[x, y - 1] == 0)
                        {
                            buildingrotate[roadcount] = 2;
                            roadcount++;
                        }
                    }
                    if (y != 9)
                    {
                        if (fieldnumber[x, y + 1] == 0)
                        {
                            buildingrotate[roadcount] = 0;
                            roadcount++;
                        }
                    }
                    int fieldset = Random.Range(0, 6);
                    Instantiate(buildings[fieldset], new Vector3(8 * x, 0, 8 * y), Quaternion.Euler(0,90*buildingrotate[Random.Range(0,roadcount)],0));
                }
                else
                {
                    int xcount = 0;
                    int ycount = 0;
                    if (x != 0)
                    {
                        if (fieldnumber[x - 1, y] == 0)
                        {
                            xcount += 1;
                        }
                    }
                    if (x != 9)
                    {
                        if (fieldnumber[x + 1, y] == 0)
                        {
                            xcount += 2;
                        }
                    }
                    if (y != 0)
                    {
                        if (fieldnumber[x, y - 1] == 0)
                        {
                            ycount += 1;
                        }
                    }
                    if (y != 9)
                    {
                        if (fieldnumber[x, y + 1] == 0)
                        {
                            ycount += 2;
                        }
                    }

                    if (xcount * ycount % 9 == 0)
                    {
                        if (ycount != 0)
                        {
                            Instantiate(roads[xcount * ycount / 3], new Vector3(8 * x, 0, 8 * y), Quaternion.Euler(0, 0, 0));
                        }
                        else
                        {
                            Instantiate(roads[0], new Vector3(8 * x, 0, 8 * y), Quaternion.Euler(0, 90, 0));
                        }
                    }
                    else if(xcount * ycount % 3 == 0)
                    {
                        if (ycount % 3 == 0)
                        {
                            Instantiate(roads[2], new Vector3(8 * x, 0, 8 * y), Quaternion.Euler(0, 180*xcount-180, 0));
                        }
                        else
                        {
                            Instantiate(roads[2], new Vector3(8 * x, 0, 8 * y), Quaternion.Euler(0, 450 - 180 * ycount, 0));
                        }
                    }
                    else
                    {
                        Instantiate(roads[1], new Vector3(8 * x, 0, 8 * y), Quaternion.Euler(0, 90 * (2 * xcount * ycount - 3 * xcount - 5 * ycount + 9), 0));
                    }
                }
            }
        }
    }
}
