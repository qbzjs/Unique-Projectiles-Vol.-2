using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Junpyo
{
    public class DodgeState : State_Base
    {
        float DodgeSpeed = 10.0f;
        float Timer;
        float DodgeStateTime = 1.0f;

        
        public override void Setting() { StateType = PLAYERSTATE.DODGE; }

        public override void StateEnter()
        {
            //Rigidbody초기화 및 중력 해제
            Owner_rigidbody.velocity = Vector3.zero;
            Owner_rigidbody.useGravity = false;
            //무적상태 돌입
            Owner.gameObject.layer = LayerMask.NameToLayer("Imotal");
            //키입력받기
            Owner.LookAt(Owner.position + MoveDirection);

            //애니메이션 재생
            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationResetTrigger("Dodge",Pv_ID);
            }
            else
            {
                Owner_animator.SetTrigger("Dodge");
            }
        }

        public override void Update()
        {
            //무적상태로 이동
            Owner_rigidbody.velocity = DodgeSpeed * -Owner.forward;

            //Dodge시간 체크
            Timer += Time.deltaTime;

            //회피기 시간이 지나면 자동으로 돌아옴
            if (DodgeStateTime <= Timer)
            {
                Debug.Log(Mathf.Abs(GroundPos.position.y - Owner.position.y));
                //공중인지 아닌지를 판단 후 StateChange
                if (Mathf.Abs(GroundPos.position.y - Owner.position.y) > 0.1f)
                {
                    state_Machine.ChangeState(PLAYERSTATE.AIR);
                }
                else
                    state_Machine.ChangeState(PLAYERSTATE.IDLE);
            }


        }

        public override void StateExit()
        {
            Timer = 0.0f;

            //Rigdbody값들 초기화
            //Owner_rigidbody.velocity = Vector3.zero;
            Owner_rigidbody.useGravity = true;

            //Layer재설정
            Owner.gameObject.layer = LayerMask.NameToLayer("Player");

            //애니메이션 해제
            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("Dodge", false, Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("Dodge", false);
            }
        }
    }
}
