using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Wooseok;
using UnityEngine.SceneManagement;

namespace Junpyo
{
    public class PlayerController_FSM : MonoBehaviourPunCallbacks , IPunObservable
    {
        //Player Componenet
        [HideInInspector] public Rigidbody playerRigidbody;
        [HideInInspector] public Animator playerAnimator;
        [HideInInspector] public State_Machine state_Machine;

        //±◊∂ÛøÓµÂ √º≈©
        public Transform GroundPos;
        private GroundCheack GroundScript;

        //PlayerÔøΩÔøΩÔøΩÔøΩ
        public PlayerInformation playerInformation;

        //ÔøΩ√∑ÔøΩÔøΩÃæÔøΩ Lookat
        [HideInInspector] public Vector3 PlayerLook;

        //ƒ´ÔøΩﬁ∂ÔøΩ
        [HideInInspector] public CameraTarget CameraTarget;
        public GameObject CameraManager;
        [HideInInspector] public CameraManager CamManager;

        //IkÔøΩ÷¥œ∏ÔøΩÔøΩÃºÔøΩ
        [HideInInspector] public Ik_Controller IK_Controller;

        //«√∑π¿ÃæÓªÛ≈¬UI
        [SerializeField] public Canvas playerCanvas;
        [SerializeField] public Image StaminaUI;
        [SerializeField] public Image HP_Bar;
        [HideInInspector] public Text ConditionText;

        //∫Œ»∞ UI
        [HideInInspector] public GameObject PlayerUI;
        [HideInInspector] public Transform RevivalUI;
        [HideInInspector] public Image RevivalWaitingImage;

        //AttackUI
        [HideInInspector] public SkillUI Skill_UI;
        [HideInInspector] public UltimateUI Ultimate_UI;

        //Dodge_Ui
        [HideInInspector] public Dodge_UI dodge_UI;

        //SpawnPos
        [HideInInspector] public Vector3 SpawnPos;

        //Effect
        [SerializeField] public GameObject DustEffect;

        //DeadFlyStateÔøΩÔøΩÔøΩÔøΩÔøΩœ∂ÔøΩ ÔøΩÔøΩÔøΩ∆∞ÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩ
        [HideInInspector] public Vector3 DeadFlyDirection;

        [HideInInspector] public bool IsUpdate;

        //StateÔøΩÔøΩÔøΩ”Ω√∞ÔøΩ
        float FearTime = 5.0f;
        float CurceTime = 5.0f;

        //?§Ìä∏?åÌÅ¨ ?ôÍ∏∞??Î≥Ä??
        [HideInInspector] public Vector3 networkPosition;
        [HideInInspector] public Quaternion networkRotation;
        [HideInInspector] public Vector3 networkVelocity;

        //»≠ªÏ«•
        [SerializeField] public GameObject LookCheckFrefab;
        [HideInInspector] public GameObject LookCheck;

        [HideInInspector] public int ID;
        [HideInInspector] public bool IsMine = false;

        //»∏««±‚
        [HideInInspector] public float DodgeCoolTime = 5.0f;
        [HideInInspector] public bool isDodge = true;

        //¿˚»Æ¿Œ ƒ›∂Û¿Ã¥ı
        [HideInInspector]  public EenemyCheck eenemyCheck;

        //πŸøÓΩ∫øÎ
        [HideInInspector] public Vector3 BounsDir;
        [HideInInspector] public float BounsStrong;

        //Ω∫≈◊¿Ã∆Æ πŸ≤Ÿ±‚øÎ
        public PLAYERSTATE WantState;

        //PlayerSkill
        [SerializeField] public PlayerSkill playerSkill;

        public bool test;

        public CHARACTERNAME CharacterName;

        [HideInInspector] public string Nicname;

        public CHARACTERNAME EnemyCharacter;

        public bool fallDead;

        public bool OnGround = false;

        protected void Awake()
        {
            //ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ∆Æ ÔøΩﬁ±ÔøΩ
            playerRigidbody = GetComponent<Rigidbody>();
            playerAnimator = GetComponent<Animator>();
            IK_Controller = GetComponent<Ik_Controller>();
            GroundScript = GroundPos.GetComponent<GroundCheack>();
            IsUpdate = true;

            ID = photonView.ViewID;

            if (photonView.IsMine)
            {
                IsMine = true;

                GameObject Camera = GameObject.Instantiate(CameraManager);
                Camera.name = "CameraManager";

                //ƒ´∏ﬁ∂Û≈∏∞Ÿ∆√ ¡ˆ¡§
                CameraTarget = GameObject.Find("CameraTarget").GetComponent<CameraTarget>();

                CameraTarget.TagetObj = transform;
                //GroundPos = transform.GetChild(4);

                //LookChackª˝º∫
                LookCheck = Instantiate<GameObject>(LookCheckFrefab, transform);

                //ΩÃ±€πˆ¿¸
                playerInformation.Initialization(CharacterName);

                eenemyCheck = transform.GetChild(5).GetComponent<EenemyCheck>();

                //UIº≥¡§
                PlayerUI = GameObject.Find("PlayerUI");
                RevivalUI = PlayerUI.transform.GetChild(3);
                RevivalWaitingImage = RevivalUI.GetChild(2).GetComponent<Image>();
                ConditionText = PlayerUI.transform.GetChild(4).GetComponent<Text>();
                dodge_UI = PlayerUI.transform.GetChild(5).GetComponent<Dodge_UI>();

                Skill_UI = PlayerUI.transform.GetChild(1).GetComponent<SkillUI>();
                Ultimate_UI = PlayerUI.transform.GetChild(2).GetComponent<UltimateUI>();

                //ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ∆Æ ÔøΩﬂ∞ÔøΩ ÔøΩÔøΩ PlayerTransformÔøΩÔøΩÔøΩÔøΩ
                state_Machine = new State_Machine();
                state_Machine._Owner = transform;

                state_Machine.StateAdd(new IdleState(), PLAYERSTATE.IDLE);
                state_Machine.ChangeState(PLAYERSTATE.IDLE); //ÔøΩ‚∫ª ÔøΩÔøΩÔøΩÔøΩIdle
                state_Machine.StateAdd(new RunState(), PLAYERSTATE.RUN);
                state_Machine.StateAdd(new BaseAttackState(), PLAYERSTATE.BASEATTACK);
                state_Machine.StateAdd(new JumpState(), PLAYERSTATE.JUMP);
                state_Machine.StateAdd(new LandState(), PLAYERSTATE.LAND);
                state_Machine.StateAdd(new HangState(), PLAYERSTATE.HANG);
                state_Machine.StateAdd(new HangAttackPrepare(), PLAYERSTATE.HANGATTACKPREPARE);
                state_Machine.StateAdd(new HangAttackState(), PLAYERSTATE.HANGATTACK);
                state_Machine.StateAdd(new HurtState(), PLAYERSTATE.HURT);
                state_Machine.StateAdd(new AirborneState(), PLAYERSTATE.AIRBORNE);
                state_Machine.StateAdd(new AirState(), PLAYERSTATE.AIR);
                state_Machine.StateAdd(new JumpAttackState(), PLAYERSTATE.JUMPATTACK);
                state_Machine.StateAdd(new GroundDownState(), PLAYERSTATE.GROUNDDOWN);
                state_Machine.StateAdd(new RollingState(), PLAYERSTATE.ROLLING);
                state_Machine.StateAdd(new DeadHighlightState(), PLAYERSTATE.DEADHIGHLIGHT);
                state_Machine.StateAdd(new DeadFlyState(), PLAYERSTATE.DEADFLY);
                state_Machine.StateAdd(new DeadState(), PLAYERSTATE.DEAD);
                state_Machine.StateAdd(new SmashState(), PLAYERSTATE.SMASH);
                state_Machine.StateAdd(new SkillState(), PLAYERSTATE.SKILL);
                state_Machine.StateAdd(new JumpSkillState(), PLAYERSTATE.JUMPSKILL);
                state_Machine.StateAdd(new DodgeState(), PLAYERSTATE.DODGE);
                state_Machine.StateAdd(new StunState(), PLAYERSTATE.STUN);
                state_Machine.StateAdd(new CurceState(), PLAYERSTATE.CURCE);
                state_Machine.StateAdd(new UltimateState(), PLAYERSTATE.ULTIMATE);
                state_Machine.StateAdd(new DropState(), PLAYERSTATE.DROP);
                state_Machine.StateAdd(new BounsHurtState(), PLAYERSTATE.BOUNSHURT);
                state_Machine.StateAdd(new StandUpState(), PLAYERSTATE.STANDUP);
                state_Machine.StateAdd(new StandUpAttackState(), PLAYERSTATE.STANDUPATTACK);
                //state_Machine.StateAdd(new ChePeSyuUltimateState(), PLAYERSTATE.CHEPESYULTIMATE);

                CamManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
                CamManager.target = CameraTarget.transform;
            }
            gameObject.name = photonView.ViewID.ToString();
        }

        protected void Update()
        {
            if (photonView.IsMine)
            {
                if (IsUpdate)
                {
                    state_Machine.Update();

                    if (Input.GetKeyDown(KeyCode.LeftShift) && isDodge)
                    {
                        StartCoroutine(DodgeDelay());
                        state_Machine.ChangeState(PLAYERSTATE.DODGE);
                    }
                }

                if(Input.GetKeyDown(KeyCode.P))
                {
                    WantStateChnage();
                }
            }
        }

        protected void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                state_Machine.FixedUpdate();
            }
            else
            {
                GetComponent<Rigidbody>().velocity = networkVelocity;

                //positionÍ∞íÏù¥ Í∏âÍ≤©?òÍ≤å Î≥Ä??Í≤ΩÏö∞ Î∞îÎ°ú Î≥Ä?òÍ≤å ?§Ï†ï
                if ((GetComponent<Rigidbody>().position - networkPosition).magnitude < 0.5f)
                {
                    GetComponent<Rigidbody>().position = Vector3.Lerp(GetComponent<Rigidbody>().position, networkPosition, 10 * Time.fixedDeltaTime);
                }
                else
                {
                    GetComponent<Rigidbody>().position = networkPosition;
                }

                GetComponent<Rigidbody>().rotation = networkRotation;
            }

        }

        public void Hang(Vector3 HangPosition, Vector3 Angel)
        {
            if (photonView.IsMine)
            {
                //playerCanvas.GetComponent<Billboard>().Hang = true;
                transform.position = HangPosition;
                transform.rotation = Quaternion.Euler(Angel);
                state_Machine.ChangeState(PLAYERSTATE.HANG);
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (photonView.IsMine)
            {
                //∂•ø° ¥Í∞Ì ¿÷¥¬ ªÛ≈¬ø°º≠ ø°æÓ∫ª ¿œ∂ß 
                if (collision.transform.tag == "Ground" || collision.transform.CompareTag("Stairs"))
                {
                        //ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ¬øÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ
                        if ((state_Machine._CurState == PLAYERSTATE.JUMPATTACK) ||
                           (state_Machine._CurState == PLAYERSTATE.AIR) ||
                           (state_Machine._CurState == PLAYERSTATE.HURT) ||
                           (state_Machine._CurState == PLAYERSTATE.JUMP))
                        {
                            state_Machine.ChangeState(PLAYERSTATE.LAND);
                        }
                        //ÔøΩ«∞›ªÔøΩÔøΩ¬∑ÔøΩ ÔøΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ
                        else if ((state_Machine._CurState == PLAYERSTATE.AIRBORNE))
                        {
                            state_Machine.ChangeState(PLAYERSTATE.GROUNDDOWN);
                        }

                }

                //∞Ë¥‹∞˙¿« √Êµπ√≥∏Æ Check
                if (collision.transform.CompareTag("Stairs") && 
                    (state_Machine._CurState == PLAYERSTATE.RUN))
                {
                    if ((collision.transform.position.y + 0.4f) > transform.position.y)
                    {
                        transform.position += new Vector3(0, 0.3f, 0);
                    } 
                }
            }
        }

        protected void OnCollisionStay(Collision collision)
        {
            if (photonView.IsMine)
            {
                if (OnGround)
                {
                    //∂•ø° ¥Í¿ª Ω√
                    if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Stairs"))
                    {
                        if ((state_Machine._CurState == PLAYERSTATE.AIRBORNE))
                        {
                            state_Machine.ChangeState(PLAYERSTATE.GROUNDDOWN);
                            OnGround = false;
                        }
                    }
                }

                //∞Ë¥‹∞˙¿« √Êµπ√≥∏Æ Check
                if (collision.transform.CompareTag("Stairs") &&
                    (state_Machine._CurState == PLAYERSTATE.RUN))
                {
                    if ((collision.transform.position.y + 0.4f) > transform.position.y)
                    {
                        transform.position += new Vector3(0, 0.3f, 0);
                    }
                }

                if (state_Machine._CurState == PLAYERSTATE.AIRBORNE && OnGround)
                {
                    transform.position += new Vector3(0, -0.2f, 0);
                }
            }
        }

        public void Hurt(Vector3 direction, float debuff, ATTACKTYPE type, float damage)
        {
            if (photonView.IsMine)
            {
                //±‚∫ªªÛ≈¬∑Œ √ ±‚»≠
                ReSet();

                playerInformation.Cur_HP -= damage;
                HP_Bar.fillAmount = playerInformation.Cur_HP / playerInformation.HP_Max;

                if(!playerInformation.UltimateOn)
                {
                    UtimateGageUp(5);
                }

                //Ω∫≈≥ ªÁøÎø©∫Œ∏¶ »Æ¿Œ
                if(state_Machine._CurState == PLAYERSTATE.SKILL)
                {
                    //ªÁøÎ¡ﬂ¿Œ Ω∫≈≥¿ª ¡§¡ˆ«—¥Ÿ
                    playerSkill.mySkill.SetActive(false);
                }

                if (playerInformation.Cur_HP <= 0)
                {
                    gameObject.layer = LayerMask.NameToLayer("Imotal");
                    //ÔøΩÔøΩÔøΩ∆∞ÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩ ÔøΩÔøΩ DeadFlyÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ»Ø
                    DeadFlyDirection = direction;
                    state_Machine.ChangeState(PLAYERSTATE.DEADHIGHLIGHT);
                }
                else
                {
                    if(damage != 0)
                    {
                        CamManager.CameraShaking();
                    }

                    if (type == ATTACKTYPE.LIGHTATTACK)
                    {
                        playerRigidbody.velocity = (direction * 2.0f) + new Vector3(0, 3.0f, 0);
                        state_Machine.ChangeState(PLAYERSTATE.HURT);
                    }
                    else if (type == ATTACKTYPE.MIDDLEATTACK)
                    {
                        playerRigidbody.velocity = (direction * 8.0f) + new Vector3(0, 6.0f, 0);
                        state_Machine.ChangeState(PLAYERSTATE.AIRBORNE);
                    }
                    else if (type == ATTACKTYPE.HEAVYATTACK)
                    {
                        playerRigidbody.velocity = (direction * 10.0f) + new Vector3(0, 7.0f, 0);
                        state_Machine.ChangeState(PLAYERSTATE.AIRBORNE);
                    }
                    else if (type == ATTACKTYPE.STUN)
                    {
                        ConditionText.text = "Ω∫≈œ";
                        state_Machine.ChangeState(PLAYERSTATE.STUN);
                    }
                    else if (type == ATTACKTYPE.CURSE)
                    {
                        if (playerInformation.Curce)
                        {
                            StopCoroutine(CurceDuration());
                            playerInformation.CurRunSpeed = playerInformation.RunSpeed;
                            playerInformation.CurJumpDistance = playerInformation.JumpDistance;
                            playerInformation.Curce = false;
                            ConditionText.text = "ªÛ≈¬¿ÃªÛ";
                        }

                        StartCoroutine(CurceDuration());
                    }
                    else if(type == ATTACKTYPE.BOUNCE)
                    {
                        BounsDir = direction;
                        BounsDir.y = -debuff * 3;
                        BounsStrong = debuff;
                        playerRigidbody.velocity = (BounsDir * debuff);
                        state_Machine.ChangeState(PLAYERSTATE.BOUNSHURT);
                    }
                    else if(type == ATTACKTYPE.STIFF)
                    {
                        playerRigidbody.velocity = new Vector3(0, 0.5f, 0);
                        state_Machine.ChangeState(PLAYERSTATE.HURT);
                    }
                }
            }
        }

        public void CameraChange()
        {
            if (photonView.IsMine)
            {
                CamManager.CameraChange();
            }
        }     

        public void ChangeState(PLAYERSTATE type)
        {
            state_Machine.ChangeState(type);
        }

        public void SmashTrigger()
        {
            Debug.Log("SmashTriggerπﬂµø");
            if (photonView.IsMine)
            {
                //Player ««∞›¥Á«œ¡ˆ æ ∞‘ ∫Ø∞Ê
                gameObject.layer = LayerMask.NameToLayer("Imotal");
                playerRigidbody.isKinematic = true;

                //¿·Ω√ æ˜µ•¿Ã∆Æ ¡§¡ˆ
                IsUpdate = false;

                //ƒ´∏ﬁ∂Û ≈∏∞Ÿ ¿⁄Ω≈¿∏∑Œ ∫Ø∞Ê
                CameraTarget.transform.position = transform.position;

                //ƒ´∏ﬁ∂Û ¡‹¿Œ
                CameraChange();

                //ø°¥œ∏ﬁ¿Ãº« ¿·Ω√ ¡§¡ˆ
                GameManager.Instance.AnimationStop(photonView.ViewID);

                //ƒ⁄∑Á∆æ Ω««‡
                StopCoroutine("SetInitialization");
                StartCoroutine(SetInitialization());
            }
        }

        IEnumerator SetInitialization()
        {
            yield return new WaitForSeconds(0.5f);

            //æ÷¥œ∏ﬁ¿Ãº« ¿Áª˝
            GameManager.Instance.AnimationStart(photonView.ViewID);

            //π∞∏Æ¿€øÎ ¥ŸΩ√ »∞º∫»≠ Ω√≈∞±‚
            playerRigidbody.isKinematic = false;

            //ƒ´∏ﬁ∂Û ¥ŸΩ√ µ«µπ∏Æ±‚
            CameraChange();

            //Layer¥ŸΩ√ µ«µπ∏Æ±‚
            gameObject.layer = LayerMask.NameToLayer("Player");

            //Ω∫∏ﬁΩ¨ Ω√∞£µøæ» æ˜µ•¿Ã∆Æ∏¶ ¡ﬂ¡ˆ«‘
            IsUpdate = true;
        }

        IEnumerator FearDuration()
        {
            yield return new WaitForSeconds(FearTime);

            playerInformation.Fear = false;
        }

        IEnumerator CurceDuration()
        {
            playerInformation.CurRunSpeed *= 0.5f;
            playerInformation.CurJumpDistance *= 0.5f;
            playerInformation.Curce = true;
            ConditionText.text = "¿˙¡÷";

            yield return new WaitForSeconds(CurceTime);

            playerInformation.CurRunSpeed = playerInformation.RunSpeed;
            playerInformation.CurJumpDistance = playerInformation.JumpDistance;
            playerInformation.Curce = false;
            ConditionText.text = "ªÛ≈¬¿ÃªÛ";
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(GetComponent<Rigidbody>().velocity);

                //HP
                stream.SendNext(playerInformation.Cur_HP);

                //Ω∫≈¬πÃ≥™
                stream.SendNext(StaminaUI.enabled);
                stream.SendNext(StaminaUI.fillAmount);
                stream.SendNext(playerInformation.Curce);
            }
            else
            {
                networkPosition = (Vector3)stream.ReceiveNext();
                networkRotation = (Quaternion)stream.ReceiveNext();
                networkVelocity = (Vector3)stream.ReceiveNext();

                //HPÎ≥Ä?òÎì§
                playerInformation.Cur_HP = (float)stream.ReceiveNext();

                //Ω∫≈¬πÃ≥™
                StaminaUI.enabled = (bool)stream.ReceiveNext();
                StaminaUI.fillAmount = Mathf.MoveTowards((float)stream.ReceiveNext(), StaminaUI.fillAmount, Time.deltaTime);
                playerInformation.Curce = (bool)stream.ReceiveNext();
            }
        }
        public void Dead(bool b)
        {
            transform.GetChild(0).gameObject.SetActive(b);
            playerCanvas.enabled = b;
            GroundPos.gameObject.SetActive(b);
            playerRigidbody.useGravity = false;

            if (photonView.IsMine)
            {
                LookCheck.SetActive(false);
            }
        }

        public void Fall()
        {
            if(photonView.IsMine)
            {
                //√ﬂ∂Ù«œ¥¬ ¿Ã∆Â∆Æ √ﬂ∞° »ƒ DeadStateµπ¿‘
                GameManager.Instance.Dead(false, photonView.ViewID);
                fallDead = true;
                state_Machine.ChangeState(PLAYERSTATE.DEAD);
            }
        }

        IEnumerator DodgeDelay()
        {
            dodge_UI.UseDodge();
            isDodge = false;

            yield return new WaitForSeconds(DodgeCoolTime);

            isDodge = true;
        }

        public void TemmDivision(string tag)
        {
            if (!CompareTag(tag))
            {
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Red");
                ChangeLayersRecursively(transform.GetChild(0), "Red");

                GroundScript.ColorChange();
            }
        }

        //¿⁄Ωƒ ∑π¿ÃæÓ±Ó¡ˆ πŸ≤Ÿ¥¬ «‘ºˆ
        public static void ChangeLayersRecursively(Transform trans, string name)
        {
            trans.gameObject.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in trans)
            {
                ChangeLayersRecursively(child, name);
            }
        }

        public void StopAnimation()
        {
            playerAnimator.StartPlayback();
        }

        public void StartAnimation()
        {
            playerAnimator.StopPlayback();
        }

        public void UseSkill()
        {
            Skill_UI.UseSkill(playerInformation.Skill_Time);
            playerInformation.SkillOn = false;      
            StartCoroutine(SkillCootime());
        }

        IEnumerator SkillCootime()
        {
            yield return new WaitForSeconds(playerInformation.Skill_Time);
            playerInformation.SkillOn = true;
        }

        public void UseUtimate()
        {
            playerInformation.Cur_UltGage = 0;
            playerInformation.UltimateOn = false;
            Ultimate_UI.UseUtimate();
        }

        public void UtimateGageUp(int gage)
        {
            if (IsMine)
            {
                if (!playerInformation.UltimateOn)
                {
                    if (playerInformation.Cur_UltGage + gage >= playerInformation.UltGage_Max)
                    {
                        playerInformation.Cur_UltGage = 0;
                        playerInformation.UltimateOn = true;
                    }
                    else
                    {
                        playerInformation.Cur_UltGage += gage;
                    }

                    Ultimate_UI.UtimateGageUp(gage);
                }
            }
        }
        public void WantStateChnage()
        {
            state_Machine.ChangeState(WantState);
        }
        
        public void LineSet()
        {
            GroundScript.LineSet();
        }

        void ReSet()
        {
            playerAnimator.StopPlayback();
            playerRigidbody.isKinematic = false;
            playerRigidbody.useGravity = true;
        }
    }
}

