using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;
        public float Sensitivity = 1f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        [Header("Logic Death")]
        public float hitPoints = 100f;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _Death;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDDeath;
        private int _animIDTakeItem;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        public Animator _animator;
        private CharacterController _controller;
        public bool falseMove = false;
        private bool hasAddedHitPoints = false;
        private bool NotificationSeventy = false;
        
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private bool _rotateOnMove = true;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }
        // Adding
        // PlayerControllerParameter
        private PlayerController playerController; 
        private BulletTarget2 bulletTarget2;
        private MazeLogic11 mazeLogic;
        public  Transform enemy;
        public GameObject buttonWarnHPSeventy;
        public List<Transform> enemies = new List<Transform>();

        private float DistancetoTarget;

        [Header("Player Setting")]
        public AudioClip Death;
        public AudioClip GetHit;
        public AudioClip GetHitScream;
        public AudioClip HealingPlayer;
        public AudioClip Punch;
        public AudioClip AirPunch;
        public AudioClip Kick;
        public AudioClip AirKick;
        // public AudioClip istigfar;
        public AudioClip Bismillah;
        AudioSource PlayerAudio;
        public GameManagerScript gameManager;
        public Finish finish;
        private bool checkGameOver;
        
        public float MaxHealth;
        public UIGameplayLogic1 UIGameplay;
        public GameObject ColliderEntireBody;
        public GameObject SwordHolding;
        public Collider SwordHold;
        [Header("Enemy VFX")]
        public ParticleSystem SlashEffect;
        public void SwordEffectToggle(){
            SlashEffect.Play();
        }
        public void AnimateDeath()
        {
            PlayerAudio.clip = Death;
            PlayerAudio.Play();      
        }
        public void AnimateGetHit()
        {
            PlayerAudio.clip = GetHit;
            PlayerAudio.Play();
        }
        private void Awake()
        {
            playerController = gameObject.GetComponent<PlayerController>();
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        public void HitSword()
        {
            Debug.Log("Sword!");

            foreach (Transform enemyTransform in enemies)
            {
                // Check if the enemyTransform is null before accessing its position
                if (enemyTransform != null)
                {
                    float distanceToTarget = Vector3.Distance(enemyTransform.position, transform.position);

                    if (distanceToTarget <= 2)
                    {
                        // enemyTransform.GetComponent<BulletTarget2>().TakeDamage(20f);
                        enemyTransform.GetComponent<BulletTarget2>().TakeDamage(100f);
                        Debug.Log("Enemy Get Hit");
                    }
                }
            }
        }
        
        public void HitKick(){
            
            PlayerAudio.clip = AirKick;
            PlayerAudio.Play();
            foreach (Transform enemyTransform in enemies)
            {
                // Check if the enemyTransform is null before accessing its position
                if (enemyTransform != null)
                {
                    float distanceToTarget = Vector3.Distance(enemyTransform.position, transform.position);

                    if (distanceToTarget <= 2)
                    {
                        // enemyTransform.GetComponent<BulletTarget2>().TakeDamage(20f);
                        enemyTransform.GetComponent<BulletTarget2>().TakeDamage(20f);
                        Debug.Log("Enemy Get Hit by Kick");
                        PlayerAudio.clip = Kick;
                        PlayerAudio.Play();
                    }
                }
            }
        }
        public void HitPunch(){
            
            PlayerAudio.clip = AirPunch;
            PlayerAudio.Play();

            // if(enemy != null && DistancetoTarget <= 2){
            //     enemy.GetComponent<BulletTarget2>().TakeDamage(10f);
            //     Debug.Log("Enemy Get Hit");

            //     PlayerAudio.clip = Punch;
            //     PlayerAudio.Play();
            // }  
            foreach (Transform enemyTransform in enemies)
            {
                // Check if the enemyTransform is null before accessing its position
                if (enemyTransform != null)
                {
                    float distanceToTarget = Vector3.Distance(enemyTransform.position, transform.position);

                    if (distanceToTarget <= 2)
                    {
                        // enemyTransform.GetComponent<BulletTarget2>().TakeDamage(20f);
                        enemyTransform.GetComponent<BulletTarget2>().TakeDamage(5f);
                        Debug.Log("Enemy Get Hit by Punch");
                        PlayerAudio.clip = Punch;
                        PlayerAudio.Play();
                    }
                }
            }
            
        }
        
        private void Start()
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            MaxHealth = hitPoints;
            UIGameplay.UpdateHealthBar(hitPoints, MaxHealth);
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
            PlayerAudio = this.GetComponent<AudioSource>();
        }
        

        private void Update()
        {  
            // if(!falseMove){
                if(enemy != null){
                DistancetoTarget = Vector3.Distance(enemy.position, transform.position);
                }
                _hasAnimator = TryGetComponent(out _animator);
                if (hitPoints != 0f && hitPoints >= 0)
                {
                    JumpAndGravity();
                    GroundedCheck();
                    Move();
                    // TAMBAHAN
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        PlayerGetHit(20f);
                    }
                    if (Input.GetKeyDown(KeyCode.T)) 
                    {
                        PlayerTakeItem();
                    }
                    if (Input.GetKeyDown (KeyCode.M))//(KeyCode.Escape))
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    if (hitPoints < 70 && !NotificationSeventy){
                        buttonWarnHPSeventy.SetActive(true);
                        Invoke("HidebuttonWarnHPSeventy", 5f);
                        NotificationSeventy = true;
                    }
                    if (hitPoints < 70 && Input.GetKeyDown(KeyCode.H) && !hasAddedHitPoints)
                    {
                        StartCoroutine(PlayAudioClips());
                        hasAddedHitPoints = true;  // Set to true to ensure this operation is performed only once
                    }
                    if(hitPoints < 0 ){
                        hitPoints = 0;
                        UIGameplay.UpdateHealthBar(hitPoints, MaxHealth);
                    }
                }
            // }   
        }
        IEnumerator PlayAudioClips()
        {
            PlayerAudio.clip = Bismillah;
            PlayerAudio.Play();
            while (PlayerAudio.isPlaying)
            {
                yield return null;  
            }
            PlayerAudio.clip = HealingPlayer;
            PlayerAudio.Play();
            hitPoints += 30;
            UIGameplay.UpdateHealthBar(hitPoints, MaxHealth);
        }
        void HidebuttonWarnHPSeventy()
        {
            buttonWarnHPSeventy.SetActive(false); // Nonaktifkan objek
        }
        public void PlayerGetHit(float damage)
        {
            if(playerController.isBlocking == false && hitPoints != 0){
                Debug.Log("Player Receive Damage - " + damage);
                _animator.SetTrigger("GetHit");
                hitPoints -= damage;
                UIGameplay.UpdateHealthBar(hitPoints, MaxHealth);
                
                if (hitPoints <= 0 && !checkGameOver)
                {
                    checkGameOver = true;
                    // _animator.SetTrigger(_animID`);
                    // gameObject.SetActive(false); // Optional Choice
                    Debug.Log("Player Died!");
                    // gameManager.gameOver();
                    // finish.gameWinning.SetActive(true);
                    finish.gameOver();
                    finish.TextAppear();
                    // finish.gameOver();
                    _animator.SetFloat("hitPoints", hitPoints );
                }
            }
        }
        
        public void PlayerTakeItem()
        {
            if (hitPoints != 0f && hitPoints >= 0){
                Debug.Log("Take Item");
                _animator.SetTrigger(_animIDTakeItem);
            }
        }
        public void CastingSpell(){
            Debug.Log("Casting Spell");

        }
        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDDeath = Animator.StringToHash("Death");
            _animIDTakeItem = Animator.StringToHash("TakeItem");

        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime; 

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier; // * Sensitivity
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier; // * Sensitivity
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            if (hitPoints != 0f && hitPoints >= 0){
            // Adding 
                if (playerController.isEquipping || playerController.isBlocking || playerController.isKicking || playerController.isAttacking)
                    return;
            }
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                if(_rotateOnMove){
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {   
                if (hitPoints != 0f && hitPoints >= 0)
                {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
            // }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        public void SetSensitivity(float newSensitivity){
            Sensitivity = newSensitivity;
        }
        public void SetRotateOnMove(bool newRotateOnMove){
            _rotateOnMove = newRotateOnMove;
        }
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if(!falseMove){
                if (animationEvent.animatorClipInfo.weight > 0.5f)
                {
                    if (FootstepAudioClips.Length > 0)
                    {
                        var index = Random.Range(0, FootstepAudioClips.Length);
                        AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                    }
                }
            }
            
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if(!falseMove){
                if (animationEvent.animatorClipInfo.weight > 0.5f)
                {
                    AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }
    }
}