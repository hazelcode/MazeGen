using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int mW = 6;
        int mH = 6;
        int[] groupID = Enumerable.Range(0, mW * mH).ToArray();

        //Initialize maze representation with 2D Array
        int[,] mazeRep = new int[mW * mH, 4];
        for (int i = 0; i < mW * mH; i++)
        {
            mazeRep[i, 0] = 0; //top
            mazeRep[i, 1] = 0; //left
            mazeRep[i, 2] = 0; //bottom
            mazeRep[i, 3] = 0; //right
        }

        //Edge count
        int edgeCount = ((mW - 1) * mH) + ((mH - 1) * mW);

        //Randomized Edge List
        int[] edgeList = Enumerable.Range(0, edgeCount).ToArray();
        System.Random r = new System.Random();
        edgeList = edgeList.OrderBy(x => r.Next()).ToArray();

        for (int i = 0; i < edgeCount; i++)
        {
            int edgeID = edgeList[i];
            if (edgeID < (mW - 1) * mH)
            {

                int rowNumb = edgeID / (mW - 1);

                int V1 = edgeID + rowNumb;
                int V2 = V1 + 1;
                if (groupID[V1] != groupID[V2]) {
                    mazeRep[V1, 3] = 1; //right
                    mazeRep[V2, 1] = 1; //left
                    int oldG = groupID[V1];
                    int newG = groupID[V2];
                    for (int j = 0; j < mW * mH; j++)
                    {
                        if (groupID[j] == oldG)
                        {
                            groupID[j] = newG;
                        }
                    }
                }
            }
            else
            {
                edgeID -= (mW - 1) * mH;
                int V1 = edgeID;
                int V2 = V1 + mW;
                if (groupID[V1] != groupID[V2])
                {
                    mazeRep[V1, 2] = 1; //top
                    mazeRep[V2, 0] = 1; //bottom
                    int oldG = groupID[V1];
                    int newG = groupID[V2];
                    for (int j = 0; j < mW * mH; j++)
                    {
                        if (groupID[j] == oldG)
                        {
                            groupID[j] = newG;
                        }
                    }
                }
            }
        }
        int xx = 0;
        int yy = 0;
        /*
        //Initialize tiles with 2D Array
        int[,] tileRep = new int[(mW - 1) * (mH - 1), 4];
        for (int i = 0; i < (mW-1) * (mH-1); i++)
        {
            mazeRep[i, 0] = 0; //top free
            mazeRep[i, 1] = 0; //left free
            mazeRep[i, 2] = 0; //bottom free
            mazeRep[i, 3] = 0; //right free
        }
        
        int tilexx = 0;
        int tileyy = 0;

        for (int i = 0; i < (mW-1) * (mH-1); i++) {
            print("tile " + i + ": x: " + tilexx + ", yy:" + tileyy);

            tilexx++;
            if (tilexx == mW-1)
            {
                tilexx = 0;
                tileyy += 1;
            }
        }*/

        for (int i = 0; i < mW * mH; i++)
        {
            int T = mazeRep[i, 0];
            int L = mazeRep[i, 1];
            int B = mazeRep[i, 2];
            int R = mazeRep[i, 3];
            String representation = "" + T + L + B + R;
            //print("x: " + xx + ", y: " + yy + ", current: " + representation);
            GameObject tile = Resources.Load(representation, typeof(GameObject)) as GameObject;
            Instantiate(tile, new Vector3(xx, 0, yy), Quaternion.identity);
            xx += 1;
            if (xx==mW)
            {
                xx = 0;
                yy += 1;
            }
        }
    }

}
