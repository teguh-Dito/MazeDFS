using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck9 : MonoBehaviour
{
    Movement9 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement9>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
