using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wooseok
{
    public class TrueLove_JumpAttack : Skill_Slash
    {
        public TrueLove_JumpAttack(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            if (
                (ParentPlayer.tag != otherobj.tag)
                && ParentPlayer != otherobj
                && MaxHit > curhit
                && MaxTargetNumber > slappedtarget.Count
                && !GameObjectChecker(slappedtarget, otherobj)
                )
            {
                slappedtarget.Add(new Pair<GameObject, int>(otherobj, 1));
                if (otherobj.layer == LayerMask.NameToLayer("Player"))
                {
                    HitEnemy(otherobj);
                    GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                    //적을 맞추는데 성공했으면 TrueLove의 State를 JumpAttackSuccess로 교체
                    ParentPlayer.GetComponent<Junpyo.TrueLove_Controller>().JumpAttackSuccess();
                }
                else if (otherobj.layer == LayerMask.NameToLayer("Coffin"))
                {
                    HitCoffin(otherobj);
                }
                curhit++;
                slappedtarget.Add(new Pair<GameObject, int>(otherobj, 1));
            }
        }
    }
}
