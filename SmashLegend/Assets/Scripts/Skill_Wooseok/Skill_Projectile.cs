using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Wooseok
{
    public class Skill_Projectile : Skill
    {
        protected bool isdestroy;
        protected Vector3 direction;
        [SerializeField]
        protected float speed;
        [SerializeField]
        protected GameObject Projectlie;

        private void Awake()
        {
            this.transform.parent = null;
            slappedtarget = new List<Pair<GameObject, int>>();
        }

        protected Skill_Projectile(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }

        public override void FollowUp()
        {
            FollowUpSkill.ParentPlayer = this.ParentPlayer;
            FollowUpSkill.transform.position = this.transform.position;
            FollowUpSkill.gameObject.SetActive(true);
            FollowUpSkill.restart();
            //this.gameObject.SetActive(false);
            //Instantiate(this.FollowUpSkill, this.gameObject.transform.position, this.gameObject.transform.rotation);

            
            //생성하고 죽음
        }
        private void OnTriggerEnter(Collider other)
        {
            SkillEffectOnEnter(other.gameObject);
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            if (CheckObj(otherobj.gameObject))
            {
                FollowUp();

                //Junpyo.EffectManager.Instance.EffectInst()

            }
        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            this.gameObject.transform.localPosition = new Vector3(  ParentPlayer.transform.position.x + startvector.x * ParentPlayer.transform.forward.x,
                                                                    ParentPlayer.transform.position.y + startvector.y, 
                                                                    ParentPlayer.transform.position.z + startvector.x * ParentPlayer.transform.forward.z);
            //this.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponent<SphereCollider>().enabled = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            timer = 0f;
            if(this.ParentPlayer)
            { 
                this.direction = this.ParentPlayer.transform.forward;
                
            }
            else
            {
                this.direction = Vector3.forward;
            }
            SetDirection();
        }

        public virtual void SetDirection()
        {
            this.gameObject.transform.LookAt(this.transform.position + this.direction);
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        }

        // Update is called once per frame
        protected virtual void Update()
        {

            if (FollowUpSkill.gameObject.activeSelf)
            {
                this.gameObject.GetComponent<SphereCollider>().enabled = false;
                Rigidbody thisbody = this.gameObject.GetComponent<Rigidbody>();
                thisbody.velocity = Vector3.zero;
                thisbody.isKinematic = true;
                if (PhotonNetwork.IsConnected)
                {
                    GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                }
                Projectlie.SetActive(false);
            }
            if(isdestroy && !FollowUpSkill.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }
        }

        protected virtual void FixedUpdate()
        {
            if (LifeTime > timer)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                isdestroy = true;
            }
        }

        public override void restart()
        {
            timer = 0f;
            curhit = 0;
            slappedtarget.Clear();
            Projectlie.SetActive(true);
            isdestroy = false;
            Start();
        }
    }
}