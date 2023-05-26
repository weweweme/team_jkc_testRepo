using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementStateRigWeight : StateMachineBehaviour
{
    private float _elapsedTime;
    [SerializeField] private float _duration;
    [SerializeField] private Rig _rig;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rig = animator.GetComponentInChildren<Rig>();
        Debug.Log(_rig.gameObject.name);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rig.weight = 0f;

        // if (_rig.weight != 0)
        // {
        //     _elapsedTime += Time.deltaTime;
        //     Debug.Log(_elapsedTime);
        //     _rig.weight = Mathf.Lerp(1, 0, _elapsedTime / _duration);
        //     Debug.Log($"rig weight : {_rig.weight}");
        //
        //     if (_rig.weight == 0)
        //     {
        //         _elapsedTime = 0;
        //     }
        // }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
