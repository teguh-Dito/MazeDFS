using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingEnemyClone : MonoBehaviour
{
 void Start()
    {
        Clone(); 
        // PlaceEnemy();
    }
    public GameObject objectToClone;
    public int numberOfClones = 1;
    public int width = 30; //x length
    public int depth = 30; //z length
    public byte[,] map; 
    public GameObject Enemy;
    public int EnemyCount = 3;
    public int scale = 6;

    public void Clone()
    {
        for (int i = 0; i < numberOfClones; i++)
        {
            GameObject clone = Instantiate(objectToClone, transform.position, transform.rotation);
            clone.tag = "Enemy";
        }
    }

    // public virtual void PlaceEnemy()
    // {
    //     int EnemySet = 0; 
    //     for (int i = 0; i < depth; i++)
    //     {
    //         for (int j = 0; j < width; j++)
    //         {
    //             int x = Random.Range(0, width);
    //             int z = Random.Range(0, depth);
    //                 if ( map[x, z] == 2 && EnemySet != EnemyCount)
    //                 {
    //                     Debug.Log("Placing Enemy");
    //                     EnemySet++;
    //                     GameObject enemy = Instantiate(Enemy, new Vector3(x * scale, 0, z * scale), Quaternion.identity);
    //                 }
    //                 if (EnemySet == EnemyCount)
    //                 {
    //                     Debug.Log("Already Placed All The Enemies and Keys");
    //                     return;
    //                 }
    //         }
    //     }
    // }
}
