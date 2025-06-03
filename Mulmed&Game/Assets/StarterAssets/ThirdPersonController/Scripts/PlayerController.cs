using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller References
    [SerializeField]
    private Animator playerAnim;
    //Equip-Unequip parameters
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject swordOnShoulder;
    public bool isEquipping;
    public bool isEquipped;
    //Blocking Parameters
    public bool isBlocking;
    //Kick Parameters
    public bool isKicking;
    //Attack Parameters
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;
    public ThirdPersonController thirdPersonController;
    public bool falseMove = false;
    public AudioSource audioSource;
    public AudioClip equip;
    public AudioClip AudioAttack1;
    public AudioClip AudioAttack2;
    public AudioClip AudioAttack3;

    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        if(!falseMove){
            timeSinceAttack += Time.deltaTime;
            // float hitPoints = thirdPersonController.hitPoints;
            Attack();
            Equip();
            Block();
            Kick();
        }
    }

    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerAnim.GetBool("Grounded"))
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }
    
    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
        }
        else
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
        }
    }

    public void Equipped()
    {
        isEquipping = false;
    }

    public void Block()
    {
        // if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        // {
            if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        {
            
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    public void Kick()
    {
        if (Input.GetKey(KeyCode.E) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Kick", true);
            isKicking = true;
        }
        else
        {
            playerAnim.SetBool("Kick", false);
            isKicking = false;
        }
    }

    private void Attack()
    {
        if (thirdPersonController.hitPoints != 0f && thirdPersonController.hitPoints >= 0)
        {
            if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && isEquipped)
            {
                if (!isEquipped)
                    return;

                currentAttack++;
                isAttacking = true;

                if (currentAttack > 3)
                    currentAttack = 1;

                //Reset
                if (timeSinceAttack > 1.0f)
                    currentAttack = 1;

                //Call Attack Triggers
                playerAnim.SetBool("Punch", false);
                playerAnim.SetTrigger("Attack" + currentAttack);

                //Reset Timer
                timeSinceAttack = 0;
            }
            else if(Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && !isEquipped)
            {
                playerAnim.SetBool("Punch", true);
            }
            else
            {
                playerAnim.SetBool("Punch", false);
            }
        }
    }

    //This will be used at animation event
    public void ResetAttack()
    {
        isAttacking = false;
    } 

    //// SOUND
    private void SoundEquip(){
        audioSource.clip = equip;
        audioSource.Play();
    }
    private void SoundAttack1(){
        audioSource.clip = AudioAttack1;
        audioSource.Play();
    }
    private void SoundAttack2(){
        audioSource.clip = AudioAttack2;
        audioSource.Play();
    }
    private void SoundAttack3(){
        audioSource.clip = AudioAttack3;
        audioSource.Play();
    }
}   
