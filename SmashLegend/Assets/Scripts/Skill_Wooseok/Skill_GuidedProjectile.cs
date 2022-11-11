using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Wooseok
{
    public class Skill_GuidedProjectile : Skill_ProjectileGoUnder
    {
        public enum State { STRAIGHT = 0, GUIDED = 1 }

        State curstate;
        GameObject target = null;
        [SerializeField]
        float straightmovementendtime;
        Ray sphereray;
        [SerializeField]
        float DetectionLength;
        [SerializeField]
        float angularspeed;

        [SerializeField] Rigidbody myrigidbody;

        protected Skill_GuidedProjectile(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            if (CheckObj(other.gameObject))
            {
                Debug.Log(other.gameObject);
                SkillEffectOnEnter(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            if (CheckObj(other.gameObject))
            {
                Debug.Log(other.gameObject);
                SkillEffectOnExit(other.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            if (CheckObj(other.gameObject))
            {
                Debug.Log(other.gameObject);
                SkillEffectOnStay(other.gameObject);
            }
        }


        protected override void Update()
        {
            //일정시간이 지나면 삭제
            if(isdestroy)
            {
                this.gameObject.SetActive(false);
            }

            float minlength;

            //타겟을 포착
            if (target != null)
            {
                minlength = Vector3.Distance(this.transform.position, target.transform.position);

                if ( minlength > DetectionLength)
                {
                    target = null;
                    minlength = DetectionLength;
                }
            }
            //타켓이 없는 경우
            else
            {
                minlength = DetectionLength;
            }

            //범위 안에 모든 객체의 콜라이더를 저장
            Collider[] Cols = Physics.OverlapSphere(transform.position, minlength);

            //저장한 콜라이더 중에 적이 있는지 검사
            foreach(Collider col in Cols)
            {
                if (CheckObj(col.gameObject))
                {
                    float len;
                    len = Vector3.Distance(this.transform.position, col.gameObject.transform.position);

                    if (len < minlength)
                    {
                        minlength = len;
                        target = col.gameObject;
                    }
                }
            }

            SetDirection();
        }

        public override void SetDirection()
        {
            //범위 안에 타겟이 있다면
            if (curstate == State.GUIDED && target != null)
            {
                myrigidbody.velocity = (speed * (LifeTime - timer)) * (myrigidbody.velocity + (target.transform.position - this.transform.position) * Time.deltaTime * (angularspeed * (LifeTime - timer))).normalized;
                transform.LookAt(myrigidbody.velocity.normalized);
            }
            //범위 안에 타겟이 없다면 추적
            else
            {
                this.gameObject.transform.LookAt(this.transform.position + direction);
                this.GetComponent<Rigidbody>().velocity = this.transform.forward * (speed * (LifeTime - timer));
            }
            
        }


        protected override void FixedUpdate()
        {
            
            if(timer > straightmovementendtime)
            {
                curstate = State.GUIDED;
            }
            if (LifeTime > timer)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                isdestroy = true;
                FollowUp();
            }
        }

        private void OnDisable()
        {
            curstate = State.STRAIGHT;
        }
    }
}