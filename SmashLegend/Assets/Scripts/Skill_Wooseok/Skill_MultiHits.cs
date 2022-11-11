using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Wooseok
{

    public class Skill_MultiHits : Skill
    {
        [SerializeField]
        int count;
        int curcount = 0;


        [SerializeField]
        List<ATTACKTYPE> AttackTypes;
        [SerializeField]
        List<float> Damages;

        public Skill_MultiHits(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }

        public override void FollowUp()
        {
            if(FollowUpSkill.gameObject != null)
            {
                FollowUpSkill.gameObject.SetActive(true);
            }
        }
        public override void restart()
        {
            timer = 0f;
            curhit = 0;
            curcount = 0;
            AttackType = AttackTypes[0];
            Damage = Damages[0];
            slappedtarget.Clear();
            if (!IshaveParent)
            {
                this.transform.position = ParentPlayer.transform.position + new Vector3(startvector.x * ParentPlayer.transform.forward.x,
                                                                                            startvector.y * ParentPlayer.transform.up.y,
                                                                                            startvector.x * ParentPlayer.transform.forward.z);
            }
            else
            {
                this.transform.localPosition = new Vector3(startvector.x * transform.forward.x, startvector.y * transform.up.y, startvector.x * transform.forward.z);
            }
        }

        void NextCount()
        {
            curcount++;
            if(curcount >= count)
            {
                curcount = 0;
            }
            AttackType = AttackTypes[curcount];
            Damage = Damages[curcount];
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            if (
                (ParentPlayer.tag != otherobj.tag)
                && ParentPlayer != otherobj
                && MaxHit > curhit
                
                )
            {
                if(!GameObjectChecker(slappedtarget, otherobj) && MaxTargetNumber > slappedtarget.Count)
                {
                    slappedtarget.Add(new Pair<GameObject, int>(otherobj, 1));
                    if (otherobj.layer == LayerMask.NameToLayer("Player"))
                    {
                        HitEnemy(otherobj);
                        GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                    }
                    else if (otherobj.layer == LayerMask.NameToLayer("Coffin"))
                    {
                        HitCoffin(otherobj);
                    }
                    curhit++;
                    slappedtarget.Add(new Pair<GameObject, int>(otherobj, 1));
                }
                else
                {
                    int targetindex = TargetFinder(slappedtarget, otherobj);
                    if(targetindex != -1)
                    {
                        if(slappedtarget[targetindex].Value < MaxHitPerTarget)
                        {
                            if (otherobj.layer == LayerMask.NameToLayer("Player"))
                            {
                                HitEnemy(otherobj); 
                                GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                            }
                            else if (otherobj.layer == LayerMask.NameToLayer("Coffin"))
                            {
                                HitCoffin(otherobj);
                            }
                            curhit++;
                            slappedtarget[targetindex] = new Pair<GameObject, int>(slappedtarget[targetindex].Key, slappedtarget[targetindex].Value + 1);
                        }
                    }
                }
            }


        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
            //throw new System.NotImplementedException();
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
            //throw new System.NotImplementedException();
        }

        protected override void HitEnemy(GameObject otherobj)
        {
            if (IsImmortal(otherobj))
            {
                return;
            }

            Junpyo.PlayerController_FSM HitObj = otherobj.GetComponent<Junpyo.PlayerController_FSM>();

            //상대를 날리는 방향
            Vector3 Direction;

            //맞은 객체와 자신의 벡터를 이용해 자신에서 맞은객체 쪽으로 이동하는 벡터를 구함
            Direction = DrawDirection(otherobj);
            GameManager.Instance.Hurt(Direction, debuffDuration, AttackTypes[0], Damages[0], ParentScript.ID, HitObj.ID);
            GameManager.Instance.Hurt(Direction, debuffDuration, AttackTypes[1], Damages[1], ParentScript.ID, HitObj.ID);
        }
    }
}