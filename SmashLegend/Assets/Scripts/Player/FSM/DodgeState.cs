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
            //Rigidbody�ʱ�ȭ �� �߷� ����
            Owner_rigidbody.velocity = Vector3.zero;
            Owner_rigidbody.useGravity = false;
            //�������� ����
            Owner.gameObject.layer = LayerMask.NameToLayer("Imotal");
            //Ű�Է¹ޱ�
            Owner.LookAt(Owner.position + MoveDirection);

            //�ִϸ��̼� ���
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
            //�������·� �̵�
            Owner_rigidbody.velocity = DodgeSpeed * -Owner.forward;

            //Dodge�ð� üũ
            Timer += Time.deltaTime;

            //ȸ�Ǳ� �ð��� ������ �ڵ����� ���ƿ�
            if (DodgeStateTime <= Timer)
            {
                Debug.Log(Mathf.Abs(GroundPos.position.y - Owner.position.y));
                //�������� �ƴ����� �Ǵ� �� StateChange
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

            //Rigdbody���� �ʱ�ȭ
            //Owner_rigidbody.velocity = Vector3.zero;
            Owner_rigidbody.useGravity = true;

            //Layer�缳��
            Owner.gameObject.layer = LayerMask.NameToLayer("Player");

            //�ִϸ��̼� ����
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
