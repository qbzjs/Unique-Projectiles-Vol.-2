using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Wooseok
{
    public class Skill_TeleportToEnemy : Skill
    {
        public enum State
        {
            NOTP = 1,
            TPED = 2,
            END = 3
        }


        [SerializeField]
        float SkillRange;
        float mindist;
        GameObject closestenemy = null;
        public State Curstate = State.NOTP;
        public bool isdestroy = false;
        public Skill_TeleportToEnemy(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp) { }
        public override void SkillEffectOnEnter(GameObject otherobj)
        {
            //���� ���� ������Ʈ�� ���̸�, ����Ʈ�� �ֱ�
            if (Curstate == State.NOTP)
            {
                Debug.Log(otherobj);
                if (LayerMask.NameToLayer("Player") == otherobj.layer &&
                    !ParentPlayer.CompareTag(otherobj.tag) &&
                    otherobj != ParentPlayer &&
                    !GameObjectChecker<GameObject,int>(slappedtarget,otherobj)
                    //��ǥ ���� �ش� ���� ����ߴ���, �ƴϸ� ������� �ʾҴ��� Ȯ���ϴ� ��� �����
                    )
                {
                    GameManager.Instance.EnemyListAdd(ParentScript.ID, otherobj.GetComponent<Junpyo.PlayerController_FSM>().ID);
                }
            }
        }

        public override void SkillEffectOnExit(GameObject otherobj)
        {
        }

        public override void SkillEffectOnStay(GameObject otherobj)
        {
            //���� ���� ������Ʈ�� ���̸�, ����Ʈ�� �ֱ�
            if (Curstate == State.NOTP)
            {
                Debug.Log(otherobj);
                if (
                    LayerMask.NameToLayer("Player") == otherobj.layer &&
                    !ParentPlayer.CompareTag(otherobj.tag) &&
                    otherobj != ParentPlayer &&
                    !GameObjectChecker<GameObject, int>(slappedtarget, otherobj)
                    //��ǥ ���� �ش� ���� ����ߴ���, �ƴϸ� ������� �ʾҴ��� Ȯ���ϴ� ��� �����
                    )
                {
                    GameManager.Instance.EnemyListAdd(ParentScript.ID, otherobj.GetComponent<Junpyo.PlayerController_FSM>().ID);
                    
                    //slappedtarget.Add(new Pair<GameObject, int>(otherobj, 0));
                }
            }
        }

        public override void FollowUp()
        {
            Debug.Log(Curstate);
            if (Curstate == State.TPED)
            {
                Debug.Log("����");
                FollowUpSkill.gameObject.SetActive(true);
                FollowUpSkill.restart();
                FollowUpSkill.SetParent(ParentPlayer);
                isdestroy = true;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            mindist = SkillRange;
            closestenemy = null;
        }

        // Update is called once per frame
        void Update()
        {
            foreach(var objs in slappedtarget)
            {
                Debug.Log(objs);
            }
            
            if (Curstate == State.NOTP && timer <= 0.5f)
            {
                timer += Time.deltaTime;
            }
            else if (Curstate == State.END)
            {
                timer += Time.deltaTime;
            }

            //Debug.Log(Curstate.ToString());
            //Debug.Log("��������");
            if (Curstate == State.NOTP && timer >= 0.5f)
            {
               
                foreach (var enemyPair in slappedtarget)
                {
                    Debug.Log(enemyPair);
                    GameObject enemy = enemyPair.Key; 
                    float thisenemydist = Vector3.Distance(enemy.transform.position, ParentPlayer.transform.position);
                    if (thisenemydist < mindist)
                    {
                        mindist = thisenemydist;
                        closestenemy = enemy;
                    }
                    //����Ʈ�� ���� ������, ���� ����� ������ ����
                    //���� ������ 10m �������� ���� �� �����
                }

                if (closestenemy != null)
                {
                    ParentPlayer.transform.position = new Vector3(closestenemy.transform.position.x,
                                                                    ParentPlayer.transform.position.y,
                                                                    closestenemy.transform.position.z);
                    // �÷��̾ ������ ���� �����ǿ��� ���� ���� * (�÷��̾� ��ġ + �� ��ġ) ��ŭ ���� ��ǥ�� �����̵�.
                    //ParentPlayer.transform.position += new Vector3(closestenemy.transform.forward.x, 0, closestenemy.transform.forward.z) * 0.3f;                    
                   /* ParentPlayer.transform.LookAt(ParentPlayer.transform.position
                        + new Vector3(closestenemy.transform.position.x,
                                                                    ParentPlayer.transform.position.y,
                                                                    closestenemy.transform.position.z));*/
                    Curstate = State.TPED;
                }
                else
                {
                    ParentPlayer.transform.position += ParentPlayer.transform.forward * SkillRange;
                    ParentPlayer.transform.Rotate(new Vector3(0, 180, 0));
                    Curstate = State.TPED;
                }
            }
            if(FollowUpSkill.gameObject.activeSelf)
            {
                Curstate = State.END;
            }

            if(Curstate == State.END && FollowUpSkill.gameObject.activeSelf == false && isdestroy && timer > LifeTime)
            {
                slappedtarget.Clear();
                this.gameObject.SetActive(false);
            }
        }

        public override void restart()
        {
            timer = 0f;
            curhit = 0;
            slappedtarget.Clear();
            Curstate = State.NOTP;
            isdestroy = false;
            FollowUpSkill.gameObject.SetActive(false);
            Start();
        }

        private void FixedUpdate()
        {


        }
    }
}