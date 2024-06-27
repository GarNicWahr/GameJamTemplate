using UnityEngine;
using UnityEngine.Assertions.Must;

/// <summary>
/// Base for a third person character controller
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    // how fast the character can turn
    public float RotationSpeed;

    // Damping for locomotion animator parameter
    public float LocomotionParameterDamping = 0.1f;

    // Script Component
    private PlayerPhysics _scriptPlayerPhysics;

    // Animator playing animations
    private Animator _animator;

    // Hash speed parameter
    private int _speedParameterHash;

    // Hash isWalking parameter
    private int _isWalkingParameterHash;

    // Hash isCrouching parameter
    private int _isCrouchingParameterHash;

    // Hash isJumping parameter
    private int _isJumpingParameterHash;

    // Is character audible (moving fast)
    public bool IsAudible { get; private set; }

    // Main Camera
    private Transform _cameraTransform;

    // Player GameObject
    private GameObject _player;

    // Character Controller of Player
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _speedParameterHash = Animator.StringToHash("speed");
        _isWalkingParameterHash = Animator.StringToHash("isMoving");
        _isCrouchingParameterHash = Animator.StringToHash("isCrouching");
        _isJumpingParameterHash = Animator.StringToHash("isJumping");

        _cameraTransform = Camera.main.transform;

        _player = GameObject.FindWithTag("Player");
        _characterController = _player.GetComponent<CharacterController>();

        _scriptPlayerPhysics = GetComponent<PlayerPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // "speed" variable im Animator auf 0 oder 1 begrenzen
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        movementDirection = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        // Should run? (left or right shift held)
        bool shouldRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Set speed to twice as much as input when running
        // otherwise use horizontal input
        float speed = shouldRun ? inputMagnitude * 2 : inputMagnitude;

        // Set animator isJumping parameter depending on input

        if (_scriptPlayerPhysics.IsGrounded() && !_animator.GetBool(_isCrouchingParameterHash)) 
        {
            _animator.SetBool(_isJumpingParameterHash, Input.GetKeyDown(KeyCode.Space));
        }
       
        // Set animator isCrouch parameter depending on input
        _animator.SetBool(_isCrouchingParameterHash, Input.GetKey(KeyCode.C));

        // Set animator isWalking parameter depending on input
        _animator.SetBool(_isWalkingParameterHash, inputMagnitude > 0);

        // Set animaotr speed parameter with damping (moves the character via root motion)
        _animator.SetFloat(_speedParameterHash, speed, LocomotionParameterDamping, Time.deltaTime);

        // Rotate Character
        if(movementDirection != Vector3.zero) 
        {
            Quaternion targetCharacterRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCharacterRotation, RotationSpeed * Time.deltaTime);
        }

        // Set CharacterController Height and Center when crouched
        if (Input.GetKey(KeyCode.C))
        {
            _characterController.height = 1.2f;
            _characterController.center = new Vector3(0, 0.6f, 0);
        }
        //Set CharacterController Height and Center when standing up 
        else
        {
            _characterController.height = 1.7f;
            _characterController.center = new Vector3(0, 0.85f, 0);
        }

        // Character is audible, when moving fast
        IsAudible = speed >= 0.5f;

    }
}
