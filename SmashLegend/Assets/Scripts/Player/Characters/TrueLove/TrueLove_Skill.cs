using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junpyo
{
    public class TrueLove_Skill : PlayerSkill
    {
        public void JumpAttack()
        {
            playerRig.velocity = Vector3.zero;
            playerRig.velocity = (playertransform.forward * 10.0f) + (-playertransform.up * 7.0f);
        }
    }
}
