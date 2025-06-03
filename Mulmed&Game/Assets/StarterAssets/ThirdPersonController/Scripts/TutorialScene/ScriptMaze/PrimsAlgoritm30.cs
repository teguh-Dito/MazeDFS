using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsAlgoritm30 : MazeLogic30
{
    public override void GenerateMap()
    {
        int x = 2;
        int z = 2;

        map[x, z] = 0;

        List<MapLocation30> walls = new List<MapLocation30>();
        walls.Add(new MapLocation30(x + 1, z));
        walls.Add(new MapLocation30(x - 1, z));
        walls.Add(new MapLocation30(x, z + 1));
        walls.Add(new MapLocation30(x, z - 1));

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
                walls.Add(new MapLocation30(x + 1, z));
                walls.Add(new MapLocation30(x - 1, z));
                walls.Add(new MapLocation30(x, z + 1));
                walls.Add(new MapLocation30(x, z - 1));
            }

            countLoops++;
        }

    }
}
