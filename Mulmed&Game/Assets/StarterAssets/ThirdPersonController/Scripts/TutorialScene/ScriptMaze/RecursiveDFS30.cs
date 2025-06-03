using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveDFS30 : MazeLogic30
{
    private bool startSet = false;
    public GameObject finishCube;//
    private GameObject finishMarker;//
    public MapLocation30 start;
    public MapLocation30 finish;
    public GameObject redCube;
    public List<MapLocation30> directions = new List<MapLocation30>(){
                                            new MapLocation30(1,0),
                                            new MapLocation30(0,1),
                                            new MapLocation30(-1,0),
                                            new MapLocation30(0,-1)
    };
    // public override void GenerateMap()
    // {
    //     int startX = Random.Range(0, width); // random x-coordinate//
    //     int startZ = Random.Range(0, depth); // random z-coordinate//
    //     Generate(5, 5);
    // }
    // void Generate(int x, int z)
    // {
    //     if (CountSquareNeighbours(x,z) >= 2) return;
    //     map[x,z] = 0;
        
    //     directions.Shuffle1();
    //     Generate(x + directions[0].x, z + directions[0].z);
    //     Generate(x + directions[1].x, z + directions[1].z);
    //     Generate(x + directions[2].x, z + directions[2].z);
    //     Generate(x + directions[3].x, z + directions[3].z);
    // }
    public override void GenerateMap()
    {
        Generate(5, 5);
        // int startX = Random.Range(0, width);
        // int startZ = Random.Range(0, depth);
    }
    void Generate(int x, int z)
    {
        if (CountSquareNeighbours(x,z) >= 2) return;
        map[x,z] = 0;

        if (!startSet) // add these lines
        {
            start = new MapLocation30(x, z);
            startSet = true;

            // Instantiate cube at start point
            Vector3 startPos = new Vector3(start.x * scale, 0, start.z * scale);
            GameObject startMarker = Instantiate(redCube, startPos, Quaternion.identity);
            startMarker.transform.localScale = new Vector3(scale, scale, scale);
        }

         // Set finish point if it's farther from start than current finish point
        MapLocation30 newPoint = new MapLocation30(x, z);
        if (finish == null || DistanceSquared(start, newPoint) > DistanceSquared(start, finish))
        {
            finish = newPoint;

            // Remove old finish marker if it exists
            if (finishMarker != null)
            {
                Destroy(finishMarker);
            }

            // Instantiate cube at finish point
            // Vector3 finishPos = new Vector3(finish.x * scale, -2.42, finish.z * scale);
            // finishMarker = Instantiate(finishCube, finishPos, Quaternion.identity);
            // finishMarker.transform.localScale = new Vector3(scale, scale, scale);

            // Instantiate cube at finish point
            Vector3 finishPos = new Vector3((float)(finish.x * scale), -2.42f, (float)(finish.z * scale));
            finishMarker = Instantiate(finishCube, finishPos, Quaternion.identity);
            finishMarker.transform.localScale = new Vector3(scale, scale, scale);


            // Activate finishCube
            finishCube.SetActive(true);

            // Reset winning state when player enters finishCube
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                
                playerInventory.ResetWinningState();
            }
        }
        ///

        if (CountSquareNeighbours(x,z) >= 2) return;
        map[x,z] = 0;
        
        directions.Shuffle();

        Generate(x + directions[0].x, z + directions[0].z);
        Generate(x + directions[1].x, z + directions[1].z);
        Generate(x + directions[2].x, z + directions[2].z);
        Generate(x + directions[3].x, z + directions[3].z);
    }
    int DistanceSquared(MapLocation30 point1, MapLocation30 point2)
    {
        int dx = point2.x - point1.x;
        int dz = point2.z - point1.z;
        return dx * dx + dz * dz;
    }
    
}
