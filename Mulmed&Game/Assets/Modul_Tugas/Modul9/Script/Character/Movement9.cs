using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement9 : MonoBehaviour
{
    [Header("Player Setting")]
    private Rigidbody rb;
    public float walkspeed = 0.5f, runSpeed = 1f, jumpPower = 10f, fallSpeed = 5f, airMultiplier = 10f, hitPoints = 100f;

    private Transform PlayerOrientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true, aerialBoost = true, AimMode = false, TPSMode = true;
    public Animator anim;
    public CameraLogic9 camlogic;

    [Header("Player Setting")]
    public AudioClip ShootAudio;
    public AudioClip StepAudio;
    public AudioClip DeathAudio;
    AudioSource PlayerAudio;
    public UIGameplayLogic uiGameplayLogic;
    // UIGameplayLogic uiGameplayLogic = new UIGameplayLogic();
    float Maxhealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerOrientation = this.GetComponent<Transform>();
        PlayerAudio = this.GetComponent<AudioSource>();
        Maxhealth = hitPoints;
        uiGameplayLogic.UpdateHealthBar(hitPoints, Maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints != 0f)
        {
            Movement();
            Jump();
            AimModeAdjuster();
            ShootLogic();
        }

        if (Input.GetKey(KeyCode.F))
        {
            PlayerGetHit(100f);
        }
    }
    private void Movement(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;

        if (grounded && moveDirection != Vector3.zero)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Run!");
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                rb.AddForce(moveDirection.normalized * runSpeed * 10f, ForceMode.Force);
            }
            else
            {
                Debug.Log("Walk");
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            grounded = false;
            anim.SetBool("Jump", true);
            Debug.Log("Jump and Ground become false");
        }
        else if (!grounded)
        {
            rb.AddForce(Vector3.down * fallSpeed * rb.mass, ForceMode.Force);
            if (aerialBoost)
            {
                rb.AddForce(moveDirection.normalized * walkspeed * 10f * airMultiplier, ForceMode.Impulse);
                aerialBoost = false;
            }
            // grounded = true;
        }
    }
    public void groundedChanger(){
        grounded = true;
        aerialBoost = true;
        anim.SetBool("Jump", false);
        Debug.Log("Change Ground to True");
    }
    public void AimModeAdjuster()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (AimMode)
            {
                TPSMode = true;
                AimMode = false;
                anim.SetBool("AimMode", false);
            }
            else if(TPSMode)
            {
                TPSMode = false;
                AimMode = true;
                anim.SetBool("AimMode", true);
            }
            camlogic.CameraModeChanger(TPSMode, AimMode);
        }
    }
    private void ShootLogic(){
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerAudio.clip = ShootAudio;
            PlayerAudio.Play();
            if (moveDirection.normalized != Vector3.zero)
            {
                anim.SetBool("WalkShoot", true);
                anim.SetBool("IdleShoot", false);
            }
            else
            {
                anim.SetBool("WalkShoot", false);
                anim.SetBool("IdleShoot", true);
            }
        }
        else
        {
            anim.SetBool("WalkShoot", false);
            anim.SetBool("IdleShoot", false);
        }
    }
    public void PlayerGetHit(float damage)
    {
        Debug.Log("Player Receive Damage - " + damage);
        hitPoints -= damage;
        anim.SetTrigger("GetHit");
        uiGameplayLogic.UpdateHealthBar(hitPoints, Maxhealth);
        if (hitPoints == 0f)
        {
            PlayerAudio.clip = DeathAudio;
            PlayerAudio.Play();
            anim.SetBool("Death", true);
        }
    }
    public void Step()
    {
        Debug.Log("Step");
        PlayerAudio.clip = StepAudio;
        PlayerAudio.Play();
    }

}
