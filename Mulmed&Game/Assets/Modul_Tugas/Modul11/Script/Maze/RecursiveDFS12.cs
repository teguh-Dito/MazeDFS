using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveDFS12 : MazeLogic12
{
    public List<MapLocation12> directions = new List<MapLocation12>(){
                                            new MapLocation12(1,0),
                                            new MapLocation12(0,1),
                                            new MapLocation12(-1,0),
                                            new MapLocation12(0,-1)
    };
    public override void GenerateMap()
    {
        Generate(5, 5);
    }
    void Generate(int x, int z)
    {
        if (CountSquareNeighbours(x,z) >= 2) return;
        map[x,z] = 0;
        
        directions.Shuffle1();

        Generate(x + directions[0].x, z + directions[0].z);
        Generate(x + directions[1].x, z + directions[1].z);
        Generate(x + directions[2].x, z + directions[2].z);
        Generate(x + directions[3].x, z + directions[3].z);
        

        // Generate(x + 1, z);
        // Generate(x - 1, z);
        // Generate(x, z + 1);
        // Generate(x, z - 1);
    }
}
