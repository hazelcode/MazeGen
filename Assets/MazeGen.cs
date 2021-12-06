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
        int[] groupID = Enumerable.Range(0, mW*mH).ToArray();

        //Initialize maze representation with 2D Array
        int[,] mazeRep = new int[mW*mH, 4];
        for (int i=0; i<mW*mH; i++)
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

        int horizEdgeCount = (mH - 1) * mW;
        int vertEdgeCount = (mW - 1) * mH;
        int colNumb = 0;

        int[] colNumbs = new int[vertEdgeCount];
        for (int i = 0; i<vertEdgeCount; i++)
        {
            print(i + " column number = " + colNumb);
            colNumbs[i] = colNumb;
            colNumb++;
            if (colNumb >= mW) colNumb = 0;
        }
        for (int i = 0; i<edgeCount; i++)
        {
            int edgeID = edgeList[i];
            if (edgeID < horizEdgeCount - 1)
            {
                
                int rowNumb = edgeID / (mW - 1);
                print(edgeID + " row number = " + rowNumb);
                
                int V1 = edgeID + rowNumb;
                int V2 = V1 + 1;
                if (groupID[V1] != groupID[V2]) {
                    mazeRep[V1, 3] = 1; //right
                    mazeRep[V2, 1] = 1; //left
                }
                int oldG = groupID[V1];
                int newG = groupID[V2];
                for (int j = 0; j<mW*mH; j++)
                {
                    if (groupID[j] == oldG)
                    {
                        groupID[j] = newG;
                    }
                }
                
            }
            else
            {
                print(edgeID + " - " + vertEdgeCount + " = " + (edgeID - vertEdgeCount));
                print(edgeID + " column number = " + colNumbs[edgeID-vertEdgeCount]);
                print("that means that the upper vert is:" + (edgeID-vertEdgeCount));
                print("and the lower vert is: " + (edgeID - vertEdgeCount + mW));
                
                int V1 = (edgeID - vertEdgeCount);
                int V2 = (edgeID - vertEdgeCount + mW);
                if (groupID[V1] != groupID[V2])
                {
                    mazeRep[V1, 2] = 1; //top
                    mazeRep[V2, 0] = 1; //bottom
                }
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
        for (int i = 0; i < mW * mH; i++)
        {
            print("vert " + i + ": " + mazeRep[i, 0] + ", " + mazeRep[i, 1] + ", " + mazeRep[i, 2] + ", " + mazeRep[i, 3]);
        }
    }

}
