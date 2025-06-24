using UnityEngine;
using Fusion;

namespace Game.Character 
{
    public class NetworkCharacterController : NetworkBehaviour, IDamagebleProvider
    {
        #region CONFIGS
    
        [SerializeField] private CharacterMovementConfig _characterMovementConfig;
        [SerializeField] private CharacterMovementInputConfig _characterInputConfig;
        [SerializeField] private CameraControlConfig _cameraControlConfig;
        [SerializeField] private CharacterHealthConfig _characterHealthConfig;
        [SerializeField] private CharacterWeaponActionConfig _characterWeaponActionConfig;
        [SerializeField] private WeaponPickupSystemConfig _weaponPickupSystemConfig;

        [SerializeField] private Transform _cameraTarget;
        
        #endregion
        [field: SerializeField] public Camera Camera { get; private set; }
        
        public UnityEngine.CharacterController CharacterController { get; private set; }
        
        [field: SerializeField] public CharacterAnimationsHandler CharacterAnimationsHandler { get; private set; }

        public IDamageble Damageble { get; private set; }

        public CharacterMovementInput CharacterMovementInput { get; private set; }
        public CharacterMovement CharacterMovement { get; private set; }
        public CameraControl CameraControl { get; private set; }
        public WeaponInventory WeaponInventory { get; private set; }
        public CharacterWeaponActionHandler CharacterWeaponActionHandler { get; private set; }
        public WeaponPickupSystem WeaponPickupSystem { get; private set; }

        private void Awake()
        {
            CharacterController = GetComponent<UnityEngine.CharacterController>();
            
            CameraControl = new (_cameraControlConfig, Camera);
            CharacterMovementInput = new (_characterInputConfig);
            
            CharacterMovement = new(_characterMovementConfig, CharacterController)
            {
                RotateNotifier = CameraControl
            };

            CharacterAnimationsHandler.CharacterMovementNotifiers = CharacterMovement;
            
            
            WeaponInventory = new();
            
            CharacterWeaponActionHandler = new(_characterWeaponActionConfig)
            {
                WeaponInventoryNotifiers = WeaponInventory,
                AttackPoint = Camera.transform
            };
            
            WeaponPickupSystem = new(_weaponPickupSystemConfig, WeaponInventory) 
            {
                PickupSystemInputNotifiers = CharacterWeaponActionHandler
            };
            
        }
        
        private void Update() 
        {
            if (Object.HasStateAuthority) 
            {
                UpdateInput();
            }
        }

        private void LateUpdate()
        {
            if (Object.HasStateAuthority) 
            {
                CameraControl.LateUpdateFollowThePoint(_cameraTarget);
            }
        }

        public override void Spawned()
        {
            CameraControl.DisableForOtherClient(Object); 
            
            var networkCharacterHealth = GetComponent<NetworkCharacterHealth>();
            networkCharacterHealth.Initialize(_characterHealthConfig);
            Damageble = networkCharacterHealth;
            
            CharacterAnimationsHandler.HealthNotifiers = Damageble;
        }
        public void FixedUpdate()
        {
            if (Object.HasStateAuthority) 
            {
                FixedUpdateMovement(); 
            }
        }

        private void UpdateInput() 
        {
            CharacterMovementInput.UpdateMovementInput(false);
            
            CharacterMovement.CalculateJump(CharacterMovementInput.IsJumpInput);
            
            CameraControl.UpdateControl(CharacterMovementInput.GetInputMouseX(), CharacterMovementInput.GetInputMouseY(), Time.deltaTime);
            
            CharacterWeaponActionHandler.HandlingInput();   
            CharacterWeaponActionHandler.HandlingPickupSystemInput(Camera.transform, Camera.transform);
        }
        
        private void FixedUpdateMovement() 
        {
            CharacterMovement.CalculateMove(CharacterMovementInput.Direction, CharacterMovementInput.IsRunInput, Time.fixedDeltaTime);
            
            CharacterMovement.ApplyGravity(Time.fixedDeltaTime);
        }
    }

}
