using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Junpyo
{
    public class HangAttackPrepare : State_Base
    {
        public Image StaminaUI;

        public override void Setting()
        {
            StaminaUI = Owner_Script.StaminaUI;
            StateType = PLAYERSTATE.HANGATTACKPREPARE;
        }

        public override void StateEnter()
        {
            Owner_rigidbody.useGravity = false;

            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("HangAttackPrepare", true,Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("HangAttackPrepare", true);
            }
        }

        public override void Update()
        {
            if(Owner_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.85)
            {
                state_Machine.ChangeState(PLAYERSTATE.HANGATTACK);
            }
        }

        public override void StateExit()
        {
            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("HangAttackPrepare", false, Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("HangAttackPrepare", false);
            }

            Owner_rigidbody.useGravity = true;
        }
    }
}
