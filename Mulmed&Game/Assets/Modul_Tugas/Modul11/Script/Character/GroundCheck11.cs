using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck11 : MonoBehaviour
{
    Movement11 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement11>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
