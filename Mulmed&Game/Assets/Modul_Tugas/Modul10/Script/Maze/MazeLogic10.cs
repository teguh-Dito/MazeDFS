using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation10{
    public int x;
    public int z;

    public MapLocation10(int _x, int _z)
    {
         x = _x;
         z = _z;
    }       
}

public class MazeLogic10 : MonoBehaviour
{
    public int width = 30; //x length
    public int depth = 30; //z length
    // public GameObject Cube; //Maze Wall
    public int scale = 6;
    public GameObject Character;
    public GameObject Enemy;
    public int EnemyCount = 3;
    public List<GameObject> Cube;
    public byte[,] map; 
    GameObject[,] BuildingList;
    

    // Start is called before the first frame update
    void Start()
    {
        initialiseMap();
        GenerateMap();
        DrawMaps(); 
        PlaceCharacter(); 
        PlaceEnemy();    
          
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

    public virtual void PlaceEnemy()
    {
        int EnemySet = 0;
        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int x = Random.Range(0, width);
                int z = Random.Range(0, depth);
                if (map[x, z] == 0 && EnemySet != EnemyCount)
                {
                    Debug.Log("Placing Enemy");
                    Debug.Log("Enemy position: " + new Vector3(x * scale, 0, z * scale));
                    EnemySet++;
                    Instantiate(Enemy, new Vector3(x * scale, 0, z * scale), Quaternion.identity);
                }
                else if (EnemySet == EnemyCount)
                {
                    Debug.Log("Already Placing All The Enemy");
                    return;
                }
            }
        }
    }
}