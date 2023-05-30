using UnityEngine;

public class JumpStartState : StateMachineBehaviour
{
    private Rigidbody _playerRigidbody;
    [SerializeField] private float _jumpForce;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerRigidbody = animator.GetComponent<Rigidbody>();

        
        animator.SetLayerWeight(1, 0);
        
        // 위로 Jump한다.
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
