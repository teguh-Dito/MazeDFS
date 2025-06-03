using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
namespace StarterAssets
{
public class MapLocation11{
    public int x;
    public int z;

    public MapLocation11(int _x, int _z)
    {
         x = _x;
         z = _z;
    }       
}
public class MazeLogic11 : MonoBehaviour
{
    public int width = 15; //x length
    public int depth = 15; //z length
    // public GameObject Cube; //Maze Wall
    public int scale = 6;
    public GameObject Character;
    public GameObject Enemy;
    public List<Transform> enemies = new List<Transform>();
    public GameObject Key;
    public int KeyCount = 3;
    public int EnemyCount = 3;
    public int RoomCount = 3;
    public int RoomMinSize = 6;
    public int RoomMaxSize = 10;
    public NavMeshSurface surface;
    public List<GameObject> Cube;
    private BulletTarget2 BulletTarget2;
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
        // StartCoroutine(PlaceCharacterDelayed());
        PlaceCharacter(); 
        PlaceEnemy(); 
        surface.BuildNavMesh(); 
    }
    IEnumerator PlaceCharacterDelayed()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed
        PlaceCharacter();
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
    for (int i = 0; i < depth; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int x = Random.Range(0, width);
            int z = Random.Range(0, depth);
            if (map[x, z] == 0 && !PlayerSet)
            {
                Debug.Log("Placing Character");
                Debug.Log("Letak saya adalah : " + (x * scale) + " " + 0 + " " + (z * scale));

                // // Check if the character position is valid (no wall at the position)
                if (!IsWallAtPosition(x, z))
                {
                    if(IsWallAtOn(x,z)){
                        Vector3 characterPosition = new Vector3(x * scale, -3, z * scale);
                        Character.transform.position = characterPosition;
                        PlayerSet = true;
                        Debug.Log("Character placed successfully at: " + characterPosition);
                    }
                }
                else
                {
                    Debug.Log("Character can't be placed on a wall. Trying again.");
                    PlayerSet = false;
                }
            }
            else if (PlayerSet)
            {
                Debug.Log("Already Placing Character");
                return;
            }
        }
    }
}

// Helper method to check if there is a wall at the specified position
private bool IsWallAtPosition(int x, int z)
{
    return map[x, z] == 1;
}
private bool IsWallAtOn(int x, int z)
{
    return map[x, z] == 0;
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
    int KeySet = 0;

    for (int i = 0; i < depth; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int x = Random.Range(0, width);
            int z = Random.Range(0, depth);
            Vector3 pos = new Vector3(x * scale, 0, z * scale);

            if (map[x, z] == 0 && !usedPositions.Contains(pos))
            {
                if (EnemySet != EnemyCount)
                {
                    // Check if the position is not occupied by the player
                    if (IsPlayerAtPosition(x, z))
                    {
                        Debug.Log("Enemy can't be placed on the player's position. Trying again.");
                        continue; // Skip to the next iteration of the loop
                    }

                    Debug.Log("Placing Enemy");
                    Debug.Log("Enemy position: " + pos);

                    // Instantiate the enemy
                    GameObject newEnemy = Instantiate(Enemy, pos, Quaternion.identity);

                    // Add the enemy's transform to the list
                    enemies.Add(newEnemy.transform);

                    // Add the enemy's transform to the list in ThirdPersonController
                    ThirdPersonController thirdPersonController = FindObjectOfType<ThirdPersonController>();
                    if (thirdPersonController != null)
                    {
                        thirdPersonController.enemies.Add(newEnemy.transform);
                    }

                    EnemySet++;
                    usedPositions.Add(pos);
                }
                else if (KeySet != KeyCount)
                {
                    Debug.Log("Placing Key");
                    Debug.Log("Key position: " + pos);
                    KeySet++;
                    Instantiate(Key, pos, Quaternion.identity);
                    usedPositions.Add(pos);
                }
            }

            if (EnemySet == EnemyCount && KeySet == KeyCount)
            {
                Debug.Log("Already Placed All The Enemies and Keys");
                return;
            }
        }
    }
}

// // Helper method to check if the player is at a specific position
private bool IsPlayerAtPosition(int x, int z)
{
    Vector3 playerPosition = Character.transform.position;
    int playerX = Mathf.FloorToInt(playerPosition.x / scale);
    int playerZ = Mathf.FloorToInt(playerPosition.z / scale);

    return playerX == x && playerZ == z;
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
}