using UnityEngine;

namespace Literal
{
    public static class AnimLiteral
    {
        public static readonly int MOVESPEED = Animator.StringToHash("MoveSpeed");
        public static readonly int ISJUMPING = Animator.StringToHash("IsJumping");
        public static readonly int ISGRAB = Animator.StringToHash("IsGrab");
    }
}