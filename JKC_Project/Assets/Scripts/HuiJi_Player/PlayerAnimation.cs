using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    
    [SerializeField] private float _acceleration = 0.5f;
    [SerializeField] private float _deceleration = 0.5f;
    
    private float _velocity;
    
    private static readonly int MOVESPEED = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
    }
    
    private void Update()
    {
        if (_playerInput.InputVec.x != 0 || _playerInput.InputVec.z != 0)
        {
            _velocity += Time.deltaTime * _acceleration;
            _velocity = Mathf.Clamp(_velocity, 0, 1);
        }

        else
        {
            _velocity -= Time.deltaTime * _deceleration;
            _velocity = Mathf.Clamp(_velocity, 0, 1);
        }
        
        _animator.SetFloat(MOVESPEED, _velocity);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GrabBox") && _animator.GetBool("IsGrab"))
        {
            _animator.SetBool("IsGrabSuccess", true);
        }
    }
}
