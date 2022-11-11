using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    public class AirState : State_Base
    {
        bool Action = false;

        public override void Setting() { StateType = PLAYERSTATE.AIR; }
        public override void StateEnter()
        {
            //Owner_rigidbody.velocity += new Vector3(0, -1.0f, 0);

            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationTrigger("Air", Pv_ID);
            }
            else
            {
                Owner_animator.SetTrigger("Air");
            }

        }

        public override void Update()
        {
            MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            if (MoveDirection.magnitude != 0)
            {
                Owner_Look = MoveDirection;
                Owner.LookAt(Owner.position + MoveDirection);
            }

            if (MoveDirection.magnitude != 0)
            {
                Owner_rigidbody.velocity = new Vector3(
                       Owner_Script.playerInformation.CurJumpDistance * MoveDirection.x,
                       Owner_rigidbody.velocity.y,
                       Owner_Script.playerInformation.CurJumpDistance * MoveDirection.z);
            }


            //Air로 가는 경우가 공격하고 나서부터라서 필요없을 듯 
           /* if (Input.GetKeyDown(KeyCode.Z))
            {
                if (Owner_Script.CharacterName != CHARACTERNAME.DUSEONIN)
                {
                    state_Machine.ChangeState(PLAYERSTATE.JUMPATTACK);
                }
                else
                {
                    state_Machine.ChangeState(PLAYERSTATE.DUSEONIN_JUMPATTACK);
                }
            }
            else if (Input.GetKeyDown(KeyCode.X) &&
                Owner_Script.playerInformation.SkillOn)
            {
                if (Owner_Script.playerInformation.JumpSkillPrepare)
                {
                    state_Machine.ChangeState(PLAYERSTATE.JUMPSKILLPREPARE);
                }
                else
                {
                    state_Machine.ChangeState(PLAYERSTATE.JUMPSKILL);
                }
            }*/
        }

        public override void StateExit()
        {
        }
    }
}