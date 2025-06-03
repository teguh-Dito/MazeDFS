using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck7 : MonoBehaviour
{
    Movement7 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement7>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
