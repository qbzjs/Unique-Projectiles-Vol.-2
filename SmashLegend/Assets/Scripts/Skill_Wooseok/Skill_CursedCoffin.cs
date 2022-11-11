using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Junpyo;
using System.ComponentModel;
using Photon.Pun;

namespace Wooseok
{
    public class Skill_CursedCoffin : Skill
    {
        [SerializeField]
        float Range;
        [SerializeField]
        float interval;
        [SerializeField]
        Dictionary<GameObject, Pair<bool, float>> PlayerTimer = new Dictionary<GameObject, Pair<bool, float>>();
        [SerializeField] bool Ground = false;
        [SerializeField] GameObject[] Effect;
        bool Start;

        [SerializeField]
        Skill_Coffin CoffinData;
        Skill_CursedCoffin(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {

        }
        private void Awake()
        {
            this.transform.parent = null;
            PlayerTimer = new Dictionary<GameObject, Pair<bool, float>>();
        }

        public override void FollowUp()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Ground && (other.gameObject.layer == LayerMask.NameToLayer("Ground")))
            {
                Ground = true;

                foreach (GameObject effect in Effect)
                {
                    effect.SetActive(true);
                }

                this.GetComponent<Rigidbody>().isKinematic = true;
                Start = true;
            }
        }

        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            otherobj.GetComponent<PlayerController_FSM>();

            if (Ground)
            {
                if (
                    otherobj != this.ParentPlayer
                    && !ParentPlayer.CompareTag(otherobj.tag)
                    && otherobj.layer == LayerMask.NameToLayer("Player")
                    )
                {
                    if (!PlayerTimer.ContainsKey(otherobj))
                    {
                        PlayerTimer.Add(otherobj, new Pair<bool, float>(true, 0.0f));
                    }
                    else
                    {
                        Pair<bool, float> temp_pair = PlayerTimer[otherobj];
                        temp_pair.Key = true;
                        PlayerTimer[otherobj] = temp_pair;
                    }
                }
            }
        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
            if (Ground)
            {
                if (PlayerTimer.ContainsKey(otherobj))
                {
                    Pair<bool, float> pair = PlayerTimer[otherobj];
                    pair.Key = false;
                    PlayerTimer[otherobj] = pair;
                }
            }
            //
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
            if (Ground)
            {
                if (
                    otherobj != this.ParentPlayer
                    && !ParentPlayer.CompareTag(otherobj.tag)
                    && otherobj.layer == LayerMask.NameToLayer("Player")
                )
                {
                    if (!PlayerTimer.ContainsKey(otherobj))
                    {
                        PlayerTimer.Add(otherobj, new Pair<bool, float>(true, 0.0f));
                    }
                    else
                    {
                        Pair<bool, float> pair = PlayerTimer[otherobj];
                        pair.Key = true;
                        PlayerTimer[otherobj] = pair;
                    }
                }


                if (
                    otherobj.layer == LayerMask.NameToLayer("Player") &&
                    !ParentPlayer.CompareTag(otherobj.tag) &&
                    PlayerTimer.ContainsKey(otherobj)
                    )
                {

                    Pair<bool, float> keyvalue;
                    PlayerTimer.TryGetValue(otherobj, out keyvalue);
                    if (keyvalue.Value > interval)
                    {

                        HitEnemy(otherobj);
                        GameManager.Instance.GagePus(Ultimatecharge, ParentScript.ID);
                        keyvalue.Value = 0.0f;
                        PlayerTimer[otherobj] = keyvalue;
                    }
                }
            }
        }

        //Update is called once per frame
        void FixedUpdate()
        {
            if (Ground)
            {
                Pair<bool, float> pair;
                if (PlayerTimer.Count > 0)
                {
                    var keylist = new List<GameObject>(PlayerTimer.Keys);
                    for (int i = 0; i < PlayerTimer.Count; i++)
                    {
                        pair = PlayerTimer[keylist[i]];
                        pair.Value += pair.Key ? Time.fixedDeltaTime : 0.0f;
                        PlayerTimer[keylist[i]] = pair;
                    }
                }

                if(Start)
                {
                    Effect[0].transform.localScale = new Vector3((Effect[0].transform.localScale.x + (Time.fixedDeltaTime * 11f)),
                        1, (Effect[0].transform.localScale.z + (Time.fixedDeltaTime *11f)));
                    
                    if(Effect[0].transform.localScale.x >= 4)
                    {
                        Start = false;
                        Effect[0].transform.localScale = new Vector3(4, 0, 4);
                    }
                }
            }
        }

        public override void restart()
        {
            this.tag = this.ParentPlayer.tag;

            if (CoffinData.CurHP <= 0)
            {
                CoffinData.CurHP = CoffinData.MaxHP;
            }

            this.gameObject.transform.position = (ParentPlayer.transform.position
                + this.ParentPlayer.transform.forward * Range) + new Vector3(0, startvector.y, 0);
            timer = 0f;
            curhit = 0;
            slappedtarget.Clear();
            PlayerTimer.Clear();

            Effect[0].transform.localScale = Vector3.zero;

            foreach (GameObject effect in Effect)
            {
                effect.SetActive(false);
            }

            Ground = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}