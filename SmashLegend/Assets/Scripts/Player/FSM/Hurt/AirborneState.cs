using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    class AirborneState : State_Base
    {
        private float HurtDelayTime = 0.5f;
        private float HurtCurTime;
        Coroutine coroutine;

        public override void Setting() { StateType = PLAYERSTATE.AIRBORNE; }
        public override void StateEnter()
        {
            if (PhotonNetwork.IsConnected)
            {
                GameManager.Instance.AnimationTrigger("Airborne", Pv_ID);
            }
            else
            {
                Owner_animator.SetTrigger("Airborne");
            }

            coroutine = CoroutineHelper.StartCoroutine(TriggetStay());
        }

        public override void Update()
        {
            HurtCurTime += Time.deltaTime;

            if (HurtCurTime >= HurtDelayTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state_Machine.ChangeState(PLAYERSTATE.JUMP);
                }
            }
        }

        public override void StateExit()
        {
            HurtCurTime = 0.0f;
            Owner_Script.OnGround = false;
            CoroutineHelper.StopCoroutine(coroutine);
        }

        IEnumerator TriggetStay()
        {
            yield return new WaitForSeconds(0.3f);

            Owner_Script.OnGround = true;
        }
    }
}
