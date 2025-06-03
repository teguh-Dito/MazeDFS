using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class LogicAttackCollider : MonoBehaviour
{
    private BulletTarget2 bulletTarget2;
    public Collider SwordHold;
     void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.CompareTag("Enemy"))
            // {
            //     other.gameObject.GetComponent<BulletTarget2>().TakeDamage(20f);
            // }
            if (other.tag == "Enemy")
            {
                // other.GetComponent<BulletTarget2>().TakeDamage(20f);
                Destroy(other.gameObject);
                Debug.Log("SUCCESS");
            }
        }
}
