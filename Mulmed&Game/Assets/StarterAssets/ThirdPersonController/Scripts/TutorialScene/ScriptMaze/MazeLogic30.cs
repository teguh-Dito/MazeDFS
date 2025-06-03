using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapLocation30{
    public int x;
    public int z;

    public MapLocation30(int _x, int _z)
    {
         x = _x;
         z = _z;
    }       
}
public class MazeLogic30 : MonoBehaviour
{
    public int width = 30; //x length
    public int depth = 30; //z length
    public int scale = 6;
    public GameObject Character;
    public int RoomCount = 3;
    public int RoomMinSize = 6;
    public int RoomMaxSize = 10;
    // public NavMeshSurface surface;
    public List<GameObject> Cube;
    public byte[,] map; 
    GameObject[,] BuildingList;
    List<Vector3> usedPositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        initialiseMap();
        GenerateMap();
        AddRooms(RoomCount, RoomMinSize, RoomMaxSize);   
        DrawMaps(); 
        PlaceCharacter(); 
        // surface.BuildNavMesh(); 
    }
    void initialiseMap(){
        map = new byte[width, depth];
        for(int z = 0; z < depth; z++)
            for(int x = 0; x < width; x++)
            {
                map[x, z] = 1; // 1 = wall, 0 = corridor
        }
    }
    public virtual void GenerateMap(){ 
        for(int z = 0; z < depth; z++)
            for(int x = 0; x < width; x++)
            {
                if(Random.Range(0, 100) < 50)
                    map[x, z] = 0; 
            }
        // StartCoroutine(GenerateMapCoroutine());
    }

    void DrawMaps(){
        for(int z = 0; z < depth; z++)
                for(int x = 0; x < width; x++)
                {
                    if(map[x, z] == 1)
                    {
                        Vector3 pos = new Vector3(x * scale, 0, z * scale);
                        GameObject wall = Instantiate(Cube[Random.Range(0, Cube.Count)], pos, Quaternion.identity);
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
            Vector3 pos = new Vector3(x * scale, 0, z * scale);
            if (map[x,z] == 0 && !PlayerSet && !usedPositions.Contains(pos))
            {
                Debug.Log("Placing Character");
                Debug.Log("Letak saya adalah : " + pos);
                PlayerSet = true;
                Character.transform.position = pos;
                usedPositions.Add(pos);
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
    public virtual void AddRooms(int count, int minSize, int maxSize)
    {
        for (int c = 0; c < count; c++)
        {
            int startX = Random.Range(3, width - 3);
            int startZ = Random.Range(3, depth - 3);
            int roomWidth = Random.Range(minSize, maxSize);
            int roomDepth = Random.Range(minSize, maxSize);
            Debug.Log(startX + " & " + startZ);
            Debug.Log(roomWidth + " & " + roomDepth);

            for (int x = startX; x < width - 3 && x < startX + roomWidth; x++)
            {
                for (int z = startZ; z < depth - 3 && z < startZ + roomDepth; z++)
                {
                    map[x, z] = 2;
                }
            }
        }
    }

}