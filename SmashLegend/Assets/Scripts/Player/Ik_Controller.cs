using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junpyo
{
    public class Ik_Controller : MonoBehaviour
    {
        protected Animator animator;

        [SerializeField] protected Transform RightHand;
        [SerializeField] protected Transform RighlookObj = null;
        [Range(0.0f, 1.0f)] public float RightdistanceRange;
        [SerializeField] protected Vector3 RightHandRotation;
        [Range(0.0f, 1.0f)] public float RightrotationRange;

        [SerializeField] protected Transform LeftHand;
        [SerializeField] protected Transform LeftlookObj = null;
        [Range(0.0f, 1.0f)] public float LeftdistanceRange;
        [Range(0.0f, 1.0f)] public float LeftrotationRange;

        [SerializeField] protected Transform Head;
        [SerializeField] protected Transform HeadlookObj = null;
        [Range(0.0f, 1.0f)] public float HeaddistanceRange;
        [Range(0.0f, 1.0f)] public float HeadrotationRange;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void OnAnimatorIK()
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, RightdistanceRange);
            animator.SetIKPosition(AvatarIKGoal.RightHand, RighlookObj.position);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, RightrotationRange);
            animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(RightHandRotation));


            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftdistanceRange);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftlookObj.position);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftrotationRange);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(LeftlookObj.localPosition));


            animator.SetLookAtWeight(HeaddistanceRange);
            animator.SetLookAtPosition(HeadlookObj.position);
        }

        public void IKEvent(PLAYERSTATE state, bool on)
        {
            switch (state)
            {
                case PLAYERSTATE.JUMPSKILL:
                    JumpSkillIK(on);
                    break;

                case PLAYERSTATE.SKILL:
                    SkillIK(on);
                    break;

                case PLAYERSTATE.BASEATTACK:
                    BaseAttackIK(on);
                    break;

                case PLAYERSTATE.JUMPATTACK:
                    JumpAttackIK(on);
                    break;

                case PLAYERSTATE.HANG:
                    HnagIK(on);
                    break;
                case PLAYERSTATE.GROUNDDOWN:
                    DownIK(on);
                    break;
                case PLAYERSTATE.HANGATTACK:
                    HangAttack(on);
                    break;
                case PLAYERSTATE.STANDUPATTACK:
                    StandUpAttack(on);
                    break;
            }
        }

        protected virtual void JumpSkillIK(bool on) { }
        protected virtual void SkillIK(bool on) { }
        protected virtual void BaseAttackIK(bool on) { }
        protected virtual void JumpAttackIK(bool on) { }
        protected virtual void HnagIK(bool on) { }
        protected virtual void DownIK(bool on) { }
        protected virtual void StandUpAttack(bool on) { }
        protected virtual void HangAttack(bool on) { }
    }
}
