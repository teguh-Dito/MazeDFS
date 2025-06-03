using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation9{
    public int x;
    public int z;

    public MapLocation9(int _x, int _z)
    {
         x = _x;
         z = _z;
    }       
}

public class MazeLogic9 : MonoBehaviour
{
    public int width = 15; //x length
    public int depth = 15; //z length
    // public GameObject Cube; //Maze Wall
    public int scale = 6;
    public List<GameObject> Cube;
    public byte[,] map; 
    public GameObject Character;

    // Start is called before the first frame update
    void Start()
    {
        initialiseMap();
        GenerateMap();
        DrawMaps(); 
        PlaceCharacter();     
          
    // for (int z = 0; z < depth; z++)
    //     for (int x = 0; x < width; x++)
    //     {
    //         Vector3 pos = new Vector3(x, 0, z);
    //         GameObject wall = Instantiate(Cube, pos, Quaternion.identity);
    //     }
            
    }
    
    void initialiseMap(){
        map = new byte[width, depth];
        for(int z = 0; z < depth; z++)
            for(int x = 0; x < width; x++)
            {
                map[x, z] = 1; // 1 = wall, 0 = corridor
        }
    }

    // void GenerateMap(){ // initialize some corridors with randomness
    //     for(int z = 0; z < depth; z++)
    //         for(int x = 0; x < width; x++)
    //         {
    //             if(Random.Range(0, 100) < 50)
    //                 map[x, z] = 0; 
    //         }
    // }

    public virtual void GenerateMap(){ // initialize some corridors with randomness
        for(int z = 0; z < depth; z++)
            for(int x = 0; x < width; x++)
            {
                if(Random.Range(0, 100) < 50)
                    map[x, z] = 0; 
            }
    }

    void DrawMaps(){
        for(int z = 0; z < depth; z++)
                for(int x = 0; x < width; x++)
                {
                    if(map[x, z] == 1)
                    {
                        Vector3 pos = new Vector3(x * scale, 0, z * scale);
                        // GameObject wall = Instantiate(Cube, pos, Quaternion.identity);
                        GameObject wall = Instantiate(Cube[Random.Range(0, Cube.Count)], pos, Quaternion.identity);
                        // wall.transform.localScale = new Vector3(scale, scale, scale);
                        wall.transform.localScale = new Vector3(scale,scale, scale);
                        wall.transform.position = pos;
                    }
            }
    }

    public virtual void PlaceCharacter()
    {
        bool PlayerSet = false;
        for(int i = 0; i < depth; i++)
        {
            for(int j = 0; j < width; j++)
            {
                int x = Random.Range(0, width);
                int z = Random.Range(0, depth);
                if (map[x,z] == 0 && !PlayerSet)
                {
                    Debug.Log("Placing Character");
                    Debug.Log("Letak saya adalah : " + (x * scale) + " " + 2 + " " + (z * scale));
                    PlayerSet = true;
                    Character.transform.position = new Vector3(x * scale, 2, z * scale);

                }else if (PlayerSet)
                {
                    Debug.Log("Already Placing Character");
                    return;
                }
            }
        }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if(x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if(map[x - 1, z] == 0) count++;
        if(map[x + 1, z] == 0) count++;
        if(map[x, z + 1] == 0) count++;
        if(map[x, z - 1] == 0) count++;
        return count;
    }
}