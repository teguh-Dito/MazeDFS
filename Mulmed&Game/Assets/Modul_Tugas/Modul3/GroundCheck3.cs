using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck3 : MonoBehaviour
{
    Movement3 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement3>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
