using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarterAssets
{
public class RecursiveDFS11 : MazeLogic11 
{
    private bool startSet = false;
    public GameObject finishCube;//
    private GameObject finishMarker;//
    public MapLocation11 start;
    public MapLocation11 finish;
    public GameObject redCube;
    public List<MapLocation11> directions = new List<MapLocation11>(){
                                            new MapLocation11(1,0),
                                            new MapLocation11(0,1),
                                            new MapLocation11(-1,0),
                                            new MapLocation11(0,-1)
    };
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
            start = new MapLocation11(x, z);
            startSet = true;

            // Instantiate cube at start point
            Vector3 startPos = new Vector3(start.x * scale, 0, start.z * scale);
            GameObject startMarker = Instantiate(redCube, startPos, Quaternion.identity);
            startMarker.transform.localScale = new Vector3(scale, scale, scale);
        }

         // Set finish point if it's farther from start than current finish point
        MapLocation11 newPoint = new MapLocation11(x, z);
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
    int DistanceSquared(MapLocation11 point1, MapLocation11 point2)
    {
        int dx = point2.x - point1.x;
        int dz = point2.z - point1.z;
        return dx * dx + dz * dz;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            
            if (playerInventory != null && playerInventory.NumberOfDiamonds >= 3)
            {
                Debug.Log("Kamu Menang!!!!!!!!!!!!!!!");
            }
        }
    }
}
}