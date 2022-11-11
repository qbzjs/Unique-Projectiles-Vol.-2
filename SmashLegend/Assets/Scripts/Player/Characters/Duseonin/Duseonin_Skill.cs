using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Junpyo
{
    public class Duseonin_Skill : PlayerSkill
    {
        [SerializeField]
        private float BackRebound;

        [SerializeField]
        private float UpRebound;

        public void DuseoninJumpAttack()
        {
            playerRig.velocity = Vector3.zero;
            playerRig.velocity = -(playertransform.forward * BackRebound) + (playertransform.up * UpRebound);
        }
    }
}
