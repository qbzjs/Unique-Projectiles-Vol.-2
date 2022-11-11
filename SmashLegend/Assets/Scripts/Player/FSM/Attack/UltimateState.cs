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
            Debug.Log("�ñر� ���");
            //Layer��ä
            Owner.gameObject.layer = LayerMask.NameToLayer("Imotal");

            //�ñر� ������ 0���� �ʱ�ȭ �� ���µ� �ʱ�ȭ
            Owner_Script.UseUtimate();

            //�ִϸ��̼� ���
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
