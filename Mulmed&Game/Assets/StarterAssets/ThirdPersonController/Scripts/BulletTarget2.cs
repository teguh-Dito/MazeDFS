using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace StarterAssets{
public class BulletTarget2 : MonoBehaviour
{
    public float ChaseRange;
    public Transform target;
    // private UnityEngine.AI.NavMeshAgent agent;
    private NavMeshAgent agent;
    private float DistancetoTarget;
    public float HitPoints = 100f;
    public float turnSpeed = 15f;
    private bool hasPlayed = false;
    // private float DistancetoTarget;
    private float DistancetoDefault;
    private Animator anim;
    public AudioSource audioSource;
    public AudioClip ScreamZombie;
    public AudioClip DeathZombie;
    public AudioClip grateful;
    public bool falseMove = false;
    Vector3 DefaultPosition;

    [Header("Enemy VFX")]
    public ParticleSystem SlashEffect;
    public List<Transform> enemies = new List<Transform>();

   
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        anim.SetFloat("HitPoints", HitPoints);
        DefaultPosition = this.transform.position;

        // Automatically add the enemy to the list when instantiated
        AddEnemyToList();
    }

private void AddEnemyToList()
    {
        // Check if the enemy is not already in the list
        if (!enemies.Contains(transform))
        {
            // Add the enemy to the list
            enemies.Add(transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!falseMove){
            DistancetoTarget = Vector3.Distance(target.position, transform.position);
            DistancetoDefault = Vector3.Distance(DefaultPosition, transform.position);
            if(DistancetoTarget <= ChaseRange && HitPoints != 0 && target.GetComponent<ThirdPersonController>().hitPoints != 0){
                faceTarget(target.position);
                if(DistancetoTarget > agent.stoppingDistance + 2f){
                    ChaseTarget();
                    SlashEffect.Stop();
                    Debug.Log("ChaseTarget");
                }else if(DistancetoTarget <= agent.stoppingDistance){
                    Attack();
                    Debug.Log("Attack");
                }
            }else if(DistancetoTarget >= ChaseRange * 2){
                agent.SetDestination(DefaultPosition);
                faceTarget(DefaultPosition);
                Debug.Log("Attack Stop");
                anim.SetBool("Attack", false);
                if (DistancetoDefault <= agent.stoppingDistance || DistancetoDefault <= ChaseRange)
                {
                    Debug.Log("Run Stop");
                    anim.SetBool("Run", false);
                }
            }
            else if(target.GetComponent<ThirdPersonController>().hitPoints == 0)
            {
                    Debug.Log("Time to Stop");
                    anim.SetBool("Run", false);
                    anim.SetBool("Attack", false);
            }
        }
    }

    public void SplashEffectToggle(){
        SlashEffect.Play();
    }
     public void TakeDamage(float damage){
        HitPoints -= damage;
        anim.SetTrigger("GetHit");
        anim.SetFloat("HitPoints", HitPoints);
        audioSource.clip = ScreamZombie;
        audioSource.Play();
        // anim.SetBool("Attack", false);
        if(HitPoints < 1 && !hasPlayed){
            StartCoroutine(PlayAudioClips());
            hasPlayed = true;
            Destroy(gameObject, 3f);
        }
    }
    IEnumerator PlayAudioClips()
    {
        audioSource.clip = DeathZombie;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
                yield return null;  
        }
        audioSource.clip = grateful;
        audioSource.Play();
        }
    
    private void faceTarget(Vector3 destination){
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    public void Attack(){
        Debug.Log("Attack");
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
    }
    public void ChaseTarget(){
        Debug.Log("Running");
        agent.SetDestination(target.position);
        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }
    public void HitConnect(){
        if(DistancetoTarget <= agent.stoppingDistance){
            target.GetComponent<ThirdPersonController>().PlayerGetHit(20f);
        }
    }
}
}
// #endif