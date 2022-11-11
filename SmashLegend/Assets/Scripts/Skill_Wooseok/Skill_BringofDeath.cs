using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Junpyo;

namespace Wooseok
{

    public class Skill_BringofDeath : Skill
    {
        [SerializeField]
        int count;

        [SerializeField]
        List<float> DmgList;
        [SerializeField]
        List<ATTACKTYPE> ATKtypeList;

       [SerializeField]
        float Firerate;

        BoxCollider hitbox;
        public bool ison;
        Animator myAnimator;

        [SerializeField] GameObject[] BeamList;

        Skill_BringofDeath(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }


        
        private void OnTriggerEnter(Collider other)
        {
            SkillEffectOnEnter(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            SkillEffectOnStay(other.gameObject);
        }

        public override void FollowUp()
        {
            
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

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
                    Debug.Log("여기까지는 들어감");
                    GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                }
                else if (otherobj.layer == LayerMask.NameToLayer("Coffin"))
                {
                    Debug.Log("Coffin 타격 진입");
                    HitCoffin(otherobj);
                }

                curhit++;
            }
        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
            
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }


            if (otherobj.layer == LayerMask.NameToLayer("Player") || otherobj.layer == LayerMask.NameToLayer("Coffin"))
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

                    PlayerController_FSM HitObj = otherobj.GetComponent<PlayerController_FSM>();

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

                    GameManager.Instance.GagePus(Ultimatecharge,ParentScript.ID);
                }
            }
        }
        // Start is called before the first frame update

        protected override void Awake()
        {
            base.Awake();
            hitbox = this.gameObject.GetComponent<BoxCollider>();
            myAnimator = this.gameObject.GetComponent<Animator>();
            myAnimator.speed = Firerate;
        }
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
        public void Hit()
        {
            Damage = DmgList[count];
            AttackType = ATKtypeList[count];
            slappedtarget.Clear();
            curhit = 0;
            Debug.Log(AttackType);
            count++;
        }
        private void FixedUpdate()
        {
            if (ison)
            {
                ison = false;
                this.gameObject.SetActive(false);
            }
        }

        public override void restart()
        {
            this.gameObject.transform.position = this.ParentPlayer.transform.position + 
                (this.ParentPlayer.transform.forward * startvector.x)
                + this.ParentPlayer.transform.up * startvector.y;

           /* foreach(GameObject beam in BeamList)
            {
                beam.transform.localRotation = ParentPlayer.transform.localRotation;
            }*/

            hitbox.enabled = false;
            timer = 0f;
            curhit = 0;
            count = 0;
            slappedtarget.Clear();
            Start();
            myAnimator.Play("Play");
            
            ison = false;
        }
    }

}