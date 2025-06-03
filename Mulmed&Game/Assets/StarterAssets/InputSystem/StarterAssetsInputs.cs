using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		// public InputAction aimClickAction;

		private PlayerController playerController;
		
		private void Awake(){
			playerController = gameObject.GetComponent<PlayerController>();
		}

#if ENABLE_INPUT_SYSTEM 
//&& STARTER_ASSETS_PACKAGES_CHECKED
		// void Awake()
		// {
    	// 	aimClickAction = new InputAction(binding: "<Mouse>/leftButton");
    	// 	aimClickAction.performed += ctx => AimInput(ctx.ReadValue<float>() > 0);
    	// 	aimClickAction.Enable();
		// }
		
		
		public void OnMove(InputValue value)
		{
			// if(!aim){
				MoveInput(value.Get<Vector2>());
			// }
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if (playerController.isBlocking || playerController.isKicking || playerController.isAttacking)
				return;
			JumpInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
				AimInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{	
			// if(!aim){
				SprintInput(value.isPressed);
			// }
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
            // SetCursorState(!newAimState); // Lock or unlock the cursor depending on aim state
            // if (aim)
            // {
            //     move = Vector2.zero; // Menonaktifkan input pergerakan saat dalam mode "aim"
            // }
		}
		public void ShootInput(bool newShootState)
		{	
			// if(!aim){ 
				shoot = newShootState;
			// }
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}