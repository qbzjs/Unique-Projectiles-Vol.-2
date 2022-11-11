using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    public class StunState : State_Base
    {
        float Timer;
        float StunStateTime = .0f;

        public override void Setting() { StateType = PLAYERSTATE.STUN; }

        public override void StateEnter()
        {
            Owner_rigidbody.velocity = Vector3.zero;

            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("Stun", true, Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("Stun", true);
            }
        }

        public override void Update()
        {
            Timer += Time.deltaTime;

            //�����ð����� �������°� �� Ǯ����.
            if (StunStateTime < Timer)
            {
                state_Machine.ChangeState(PLAYERSTATE.IDLE);
            }
        }

        public override void StateExit()
        {
            Timer = 0.0f;

            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("Stun", false, Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("Stun", false);
            }
        }
    }
}
