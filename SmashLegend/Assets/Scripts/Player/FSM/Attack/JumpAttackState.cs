using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    public class JumpAttackState : State_Base
    {
        public override void Setting() { StateType = PLAYERSTATE.JUMPATTACK; }

        public override void StateEnter()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (Owner_Script.playerInformation.IsAttackUplayer)
                {
                    GameManager.Instance.AnimationSetLayerWeight(1, 1, Pv_ID);
                }

                GameManager.Instance.AnimationBool("JumpAttack", true, Pv_ID);
            }
            else
            {
                if (Owner_Script.playerInformation.IsAttackUplayer)
                {
                    Owner_animator.SetLayerWeight(1, 1);
                }

                Owner_animator.SetBool("JumpAttack", true);
            }
        }

        public override void Update()
        {
            AnimatorStateInfo info;

            if (Owner_Script.playerInformation.IsAttackUplayer)
            {
                info = Owner_animator.GetCurrentAnimatorStateInfo(1);
            }
            else
            {
                info = Owner_animator.GetCurrentAnimatorStateInfo(0);
            }

            if (info.normalizedTime > 0.95f && info.IsName("JumpAttack"))
            {
                Debug.Log("����� �ϴ�");
                //��ų �ִϸ��̼��� ���� �� �� IdleState�� ��ȯ
                state_Machine.ChangeState(PLAYERSTATE.AIR);
            }
        }

        public override void StateExit()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (Owner_Script.playerInformation.IsAttackUplayer)
                {
                    GameManager.Instance.AnimationSetLayerWeight(1, 0, Pv_ID);
                }

                GameManager.Instance.AnimationBool("JumpAttack", false, Pv_ID);
            }
            else
            {
                if (Owner_Script.playerInformation.IsAttackUplayer)
                {
                    Owner_animator.SetLayerWeight(1, 0);
                }

                Owner_animator.SetBool("JumpAttack", false);
            }
        }
    }
}
