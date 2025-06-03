using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLogic1 : MonoBehaviour
{
    public int width = 30; //x length
    public int depth = 30; //z length
    // public GameObject Cube; //Maze Wall
    public int scale = 6;
    public List<GameObject> Cube;
    public byte[,] map; 
    // Start is called before the first frame update
    void Start()
    {
        initialiseMap();
        GenerateMap();
        DrawMaps();      
          
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
                        Vector3 pos = new Vector3(x, 0, z);
                        // GameObject wall = Instantiate(Cube, pos, Quaternion.identity);
                        GameObject wall = Instantiate(Cube[Random.Range(0, Cube.Count)], pos, Quaternion.identity);
                        // wall.transform.localScale = new Vector3(scale, scale, scale);
                        // wall.transform.position = pos;
                    }
            }
    }
}