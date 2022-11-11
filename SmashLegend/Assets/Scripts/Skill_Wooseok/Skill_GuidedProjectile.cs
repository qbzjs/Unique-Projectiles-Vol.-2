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
            //�����ð��� ������ ����
            if(isdestroy)
            {
                this.gameObject.SetActive(false);
            }

            float minlength;

            //Ÿ���� ����
            if (target != null)
            {
                minlength = Vector3.Distance(this.transform.position, target.transform.position);

                if ( minlength > DetectionLength)
                {
                    target = null;
                    minlength = DetectionLength;
                }
            }
            //Ÿ���� ���� ���
            else
            {
                minlength = DetectionLength;
            }

            //���� �ȿ� ��� ��ü�� �ݶ��̴��� ����
            Collider[] Cols = Physics.OverlapSphere(transform.position, minlength);

            //������ �ݶ��̴� �߿� ���� �ִ��� �˻�
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
            //���� �ȿ� Ÿ���� �ִٸ�
            if (curstate == State.GUIDED && target != null)
            {
                myrigidbody.velocity = (speed * (LifeTime - timer)) * (myrigidbody.velocity + (target.transform.position - this.transform.position) * Time.deltaTime * (angularspeed * (LifeTime - timer))).normalized;
                transform.LookAt(myrigidbody.velocity.normalized);
            }
            //���� �ȿ� Ÿ���� ���ٸ� ����
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