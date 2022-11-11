using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Junpyo;


namespace Wooseok
{

    public class Skill_TouchOfCurseFirst : Skill
    {
        public GameObject Buffer;
        public bool BufferTime;
        [SerializeField] public SpriteRenderer circle;

        Skill_TouchOfCurseFirst(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {

        }

        public override void FollowUp()
        {
            return;
        }

        private void Awake()
        {
            this.transform.parent = null;
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            if (
                ParentPlayer.tag != otherobj.tag
                && ParentPlayer != otherobj
                && MaxHit > curhit
                && MaxTargetNumber > slappedtarget.Count
                && !GameObjectChecker(slappedtarget, otherobj)
                )
            {
                slappedtarget.Add(new Pair<GameObject, int>(otherobj, 1));                   
                if(otherobj.layer == LayerMask.NameToLayer("Player"))
                {
                    HitEnemy(otherobj);
                    GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                }
                else if(otherobj.layer == LayerMask.NameToLayer("Coffin"))
                {
                    HitCoffin(otherobj);
                }
                curhit++;
            }
        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
            return;
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
            return;
        }


        protected override void HitEnemy(GameObject otherobj)
        {
            if (IsImmortal(otherobj))
            {
                return;
            }

            PlayerController_FSM HitObj = otherobj.GetComponent<PlayerController_FSM>();

            //상대를 날리는 방향
            Vector3 Direction;

            //맞은 객체와 자신의 벡터를 이용해 자신에서 맞은객체 쪽으로 이동하는 벡터를 구함
            Direction = this.transform.position - otherobj.transform.position;
            Direction.y = 0;
            Direction.Normalize();

            GameManager.Instance.CameraShaking(ParentScript.ID);
            GameManager.Instance.Hurt(Direction, debuffDuration, AttackType, Damage, ParentScript.ID ,HitObj.ID);
        }

        public override void restart()
        {
            this.gameObject.transform.position = this.ParentPlayer.transform.position + (this.ParentPlayer.transform.forward * 1.5f) + this.ParentPlayer.transform.up * 0.1f;
            timer = 0f;
            curhit = 0;
            slappedtarget.Clear();
            Buffer.transform.localScale = new Vector3(1, 1, 1);
            transform.localScale = new Vector3(1, 1, 1);
        }

        private void FixedUpdate()
        {
            if (timer < LifeTime)
            {
                timer += Time.fixedDeltaTime;

                //일정시간 이 후 문양이 줄어들음
                if (timer > 0.2f && !BufferTime)
                {
                    //로컬 스케일을 Fixtime을 이용해서 줄이기
                    Buffer.transform.localScale = new Vector3(Buffer.transform.localScale.x - (Time.fixedDeltaTime * 4.5f),
                        Buffer.transform.localScale.y,
                        Buffer.transform.localScale.z - (Time.deltaTime * 4.5f));

                    if(Buffer.transform.localScale.x <= 0)
                    {
                        BufferTime = true;
                    }
                    Debug.Log(Buffer.transform.localScale.x);
                }
                //문양 외각선을 서서히 없애는 식
                else if (BufferTime)
                {
                    circle.color = new Color(circle.color.r, circle.color.g, circle.color.b, circle.color.a - (Time.fixedDeltaTime * 8.0f));

                    if (circle.color.a <= 0)
                    {
                        //Alpha값이 0이 되는순간 공격을 지운다.
                        timer = 0.0f;
                        BufferTime = false;
                        circle.color = new Color(circle.color.r, circle.color.g, circle.color.b, 1);
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}