using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    public class UltimateState : State_Base 
    {
        public override void Setting()
        {
            StateType = PLAYERSTATE.ULTIMATE;
        }

        public override void StateEnter()
        {
            Debug.Log("궁극기 사용");
            //Layer교채
            Owner.gameObject.layer = LayerMask.NameToLayer("Imotal");

            //궁극기 게이지 0으로 초기화 후 에셋도 초기화
            Owner_Script.UseUtimate();

            //애니메이션 재생
            if(PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("Ultimate", true,Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("Ultimate", true);
            }
        }

        public override void Update()
        {
            if(Owner_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f)
            {
                state_Machine.ChangeState(PLAYERSTATE.IDLE);
            }
        }

        public override void StateExit()
        {
            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("Ultimate", false, Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("Ultimate", false);
            }

            Owner.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
