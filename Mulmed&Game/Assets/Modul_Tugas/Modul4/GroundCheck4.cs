using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck4 : MonoBehaviour
{
    Movement4 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement4>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
