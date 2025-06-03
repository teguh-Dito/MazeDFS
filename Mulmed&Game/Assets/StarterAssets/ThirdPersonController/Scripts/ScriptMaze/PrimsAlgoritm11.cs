using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarterAssets
{
public class PrimsAlgoritm11 : MazeLogic11
{
    public override void GenerateMap()
    {
        int x = 2;
        int z = 2;

        map[x, z] = 0;

        List<MapLocation11> walls = new List<MapLocation11>();
        walls.Add(new MapLocation11(x + 1, z));
        walls.Add(new MapLocation11(x - 1, z));
        walls.Add(new MapLocation11(x, z + 1));
        walls.Add(new MapLocation11(x, z - 1));

        int countLoops = 0;
        while (walls.Count > 0 && countLoops < 5000)
        {
            int rwall = Random.Range(0, walls.Count);
            x = walls[rwall].x;
            z = walls[rwall].z;
            walls.RemoveAt(rwall);
            if (CountSquareNeighbours(x,z) == 1)
            {
                map[x,z] = 0;
                walls.Add(new MapLocation11(x + 1, z));
                walls.Add(new MapLocation11(x - 1, z));
                walls.Add(new MapLocation11(x, z + 1));
                walls.Add(new MapLocation11(x, z - 1));
            }

            countLoops++;
        }

    }
}
}