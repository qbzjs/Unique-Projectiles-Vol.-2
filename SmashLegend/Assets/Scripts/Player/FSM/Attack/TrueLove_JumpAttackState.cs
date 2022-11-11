using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    public class TrueLove_JumpAttack : State_Base
    {
        public override void Setting()
        {
            StateType = PLAYERSTATE.TRUELOVE_JUMPATTACK;
        }

        public override void StateEnter()
        {
            Owner_rigidbody.velocity = Vector3.zero;
            
            if(PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("JumpAttack",true,Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("JumpAttack", true);
            }
        }

        public override void Update()
        {
            if(GroundPos.position.y < Owner.position.y)
            {
                //Owner_rigidbody.velocity = (Owner.forward * 8.0f) + new Vector3(0, -4, 0); 
            }
            else
            {
                state_Machine.ChangeState(PLAYERSTATE.TRUELOVE_JUMPATTACKSUCCESS);
            }
        }

        public override void StateExit()
        {
            Owner_rigidbody.velocity = Vector3.zero;

            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationBool("JumpAttack", false, Pv_ID);
            }
            else
            {
                Owner_animator.SetBool("JumpAttack", false);
            }
        }
    }
}
