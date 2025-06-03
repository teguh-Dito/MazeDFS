using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
//    [SerializeField] private float normalSensitivity;
//    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform hitpoint;
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private Transform crosshair;
    private Animator animator;
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
   

    private void Awake(){
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)){
                debugTransform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
                hitTransform = raycastHit.transform;
                hitpoint.position = raycastHit.point; 
            }

        if(starterAssetsInputs.aim){
            aimVirtualCamera.gameObject.SetActive(true);
            // thirdPersonController(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            debugTransform.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(false);
        }else{
            aimVirtualCamera.gameObject.SetActive(false);
            // thirdPersonController(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            debugTransform.gameObject.SetActive(true);
            crosshair.gameObject.SetActive(true);
        }

        if(starterAssetsInputs.shoot){
            if(hitTransform != null){
                // hit something
                if (hitTransform.GetComponent<BulletTarget>() != null) {
                    // Hit target
                    Instantiate(vfxHitGreen, hitpoint.position, Quaternion.identity);
                    Debug.Log("I hit this things : " +raycastHit.transform.name);
                } else {
                    // Hit something else
                    Instantiate(vfxHitRed, hitpoint.position, Quaternion.identity);
                    Debug.Log("I hit this things : " +raycastHit.transform.name);
                }
                if(raycastHit.transform.tag.Equals("Enemy")){
                    BulletTarget2 target = raycastHit.transform.GetComponent<BulletTarget2>();
                    target.TakeDamage(50);
                    Debug.Log("Damage absorb 50");
        }
            }
            // Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            // Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot =  false;

        }
    }
    // private void Shoot(){
    //     RaycastHit hit;
    //     Physics.Raycast(ShootCamera.transform.position, ShootCamera.transform.forward, out hit, range);
    //     Debug.Log("I hit this things : " + hit.transform.name);

    //     if(hit.transform.tag.Equals("Enemy")){
    //         EnemyLogic target = hit.transform.GetComponent<EnemyLogic>();
    //         target.TakeDamage(50);
    //     }
    // }
}
