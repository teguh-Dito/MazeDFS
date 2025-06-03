using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck2 : MonoBehaviour
{
    Movement1 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement1>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
