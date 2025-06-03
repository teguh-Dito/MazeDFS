using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck6 : MonoBehaviour
{
    Movement6 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement6>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
