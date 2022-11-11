using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Wooseok
{

    public class Skill_Explosion : Skill
    {
        public Skill_Explosion(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }

        public override void FollowUp()
        {
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            if (
                otherobj.tag != ParentPlayer.tag
                && ParentPlayer != otherobj
                && MaxHit > curhit
                && MaxTargetNumber > slappedtarget.Count
                && !GameObjectChecker(slappedtarget, otherobj)
                )
            {
                if (otherobj.layer == LayerMask.NameToLayer("Player"))
                {
                    HitEnemy(otherobj);
                }
                else if (otherobj.layer == LayerMask.NameToLayer("Coffin"))
                {
                    HitCoffin(otherobj);
                }
                curhit++;
                slappedtarget.Add(new Pair<GameObject, int>(otherobj, 1));
            }
        }

        protected override void HitEnemy(GameObject otherobj)
        {
            if (IsImmortal(otherobj))
            {
                return;
            }

            Vector3 direction;
            direction = DrawDirection(otherobj);

            GameManager.Instance.Hurt(direction, debuffDuration, AttackType, Damage, ParentScript.ID, otherobj.GetComponent<PhotonView>().ViewID);

            if (Damage != 0)
            {
                GameManager.Instance.CameraShaking(ParentScript.ID);
                GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
            }
        }

        protected override Vector3 DrawDirection(GameObject otherobj)
        {
            Vector3 HitObj = otherobj.transform.position;
            Vector3 SkillObj = this.gameObject.transform.position;

            HitObj.y = 0;
            SkillObj.y = 0;

            Vector3 direction = HitObj - SkillObj;

            return direction.normalized;
        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
            //throw new System.NotImplementedException();
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
            //throw new System.NotImplementedException();
        }

        public override void restart()
        {
            timer = 0f;
            curhit = 0;
            slappedtarget.Clear();
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (timer > LifeTime)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}