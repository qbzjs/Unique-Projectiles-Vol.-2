using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    class DeadHighlightState : State_Base
    {
        private float CurTime;
        private float HiglightTime = 0.5f;

        public override void Setting() { StateType = PLAYERSTATE.DEADHIGHLIGHT; }

        public override void StateEnter()
        {
            //Player �ǰݴ����� �ʰ� ����
            Owner.gameObject.layer = LayerMask.NameToLayer("Imotal");
            Owner_rigidbody.isKinematic = true;

            //Deadī�޶�� ����
            Owner_Script.CameraChange();

            //���� �ִϸ��̼��� ������Ŵ

            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("Hurt", true, Pv_ID);
                GameManager.Instance.AnimationStop(Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("Hurt", true);
                Owner_animator.StartPlayback();
            }
        }

        public override void Update()
        {
            CurTime += Time.deltaTime;

            //�����ð��� ������ DeadFlyState�� ��ȯ
            if (HiglightTime < CurTime)
            {
                //���󰡴� ����� �ٲ�
                state_Machine.ChangeState(PLAYERSTATE.DEADFLY);
            }
        }

        public override void StateExit()
        {
            CurTime = 0.0f;

            //ī�޶� MainCamera�� ��ü
            Owner_Script.CameraChange();

            //���󰡴� �ܰ迡�� GroundCheak�� UI����
            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.Dead(false, Pv_ID);
            }
            else
            {
                Owner_Script.Dead(false);
            }
        }
    }
}
