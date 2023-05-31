using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObstacleBumper : MonoBehaviour
{
    public float reflectForce;
    
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody collisionObjectRigid = other.gameObject.GetComponent<Rigidbody>();
        Vector3 collisionVector = collisionObjectRigid.velocity.normalized;
        Vector3 normalVector = other.contacts[0].normal.normalized;
        Vector3 reflectionDirection = 
            Vector3.Reflect(collisionVector,normalVector);
        reflectionDirection *= other.impulse.magnitude;
        
        // Debug.Log($"{other.impulse.magnitude}");
        
        UnableToControlPlayerInput(other).Forget();

        // Debug.Log($"입사각 : {collisionVector}");
        // Debug.Log($"충돌 법선 벡터 : {other.contacts[0].normal}");
        // Debug.Log($"반사각 : {reflectionDirection}");
        
        // collisionObjectRigid.AddForce(reflectionDirection * reflectForce, ForceMode.Impulse);

        collisionObjectRigid.velocity = reflectionDirection * reflectForce;
    }
    
    // 플레이어가 부딪힐 경우 잠시 조작할 수 없게 만드는 UniTask
    private async UniTaskVoid UnableToControlPlayerInput(Collision col)
    {
        PlayerInput playerInput = col.gameObject.GetComponent<PlayerInput>();
        // TestInput testInput = col.gameObject.GetComponentInParent<TestInput>();
        playerInput.IsReflect = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        playerInput.IsReflect = false;
    }
    
}
