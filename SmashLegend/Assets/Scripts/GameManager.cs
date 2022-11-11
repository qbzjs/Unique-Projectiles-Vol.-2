using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;
    private Junpyo.CHARACTERNAME CharacterName;
    public List<GameObject> Players = new List<GameObject>();
    public List<Junpyo.PlayerController_FSM> PlayersScript = new List<Junpyo.PlayerController_FSM>();

    //public GameObject TestEffect;
    // 인스턴스에 접근하기 위한 프로퍼티

    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                //_instance = new GameObject($"[{nameof(GameManager)}]").AddComponent<GameManager>();
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public Junpyo.CHARACTERNAME _CharacterName
    {
        get { return CharacterName; }
        set { CharacterName = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }
    public int i_GameMode = 0;
    public int i_PlayerID = 0;
    public Junpyo.CHARACTERNAME e_SetChar;
    public Junpyo.CHARACTERNAME e_Temp;

    public void AddPlayer(GameObject obj)
    {
        Players.Add(obj);
        PlayersScript.Add(obj.GetComponent<Junpyo.PlayerController_FSM>());

        string TagString = "";

        if(Players.Count == SetMaxPlayer())
        {
            for (int i =0; i < Players.Count; ++i)
            {
                if(Players[i].GetComponent<PhotonView>().IsMine)
                {
                    TagString =  Players[i].tag;
                    break;
                }
            }

            for (int i = 0; i < Players.Count; ++i)
            {
                PlayersScript[i].TemmDivision(TagString);
            }
        }
    }

    private int SetMaxPlayer()
    {
        switch(i_GameMode)
        {
            case 0:
                return 2;

            case 1:
                return 6;

            case 2:
                return 6;

            default:
                return 2;
        }
    }


//----------------------------------------------------------------------------------------RPC-------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------Animation-------------------------------------------------------------------------------------------
    public void AnimationTrigger(string name, int ID)
    {
        photonView.RPC("AnimationTriggerRPC", RpcTarget.All, name, ID);
    }

    public void PlayClip(string name, int ID)
    {
        photonView.RPC("PlayClipRPC", RpcTarget.All, name, ID);
    }

    public void AnimationBool(string name, bool b, int ID)
    {
        photonView.RPC("AnimationTBoolRPC", RpcTarget.All, name, b, ID);
    }

    public void AnimationFloat(string name, float f, int ID)
    {
        photonView.RPC("AnimationFloatRPC", RpcTarget.All, name, f, ID);
    }

    public void AnimationResetTrigger(string name, int ID)
    {
        photonView.RPC("AnimationResetTriggerRPC", RpcTarget.All, name, ID);
    }

    public void AnimationSetLayerWeight(int index, int weight, int ID)
    {
        photonView.RPC("AnimationSetLayerWeightRPC", RpcTarget.All, index, weight, ID);
    }

    public void AnimationStop(int ID)
    {
        photonView.RPC("AnimationStopRPC", RpcTarget.All, ID);
    }

    public void AnimationStart(int ID)
    {
        photonView.RPC("AnimationStartRPC", RpcTarget.All, ID);
    }

    [PunRPC]
    private void AnimationTriggerRPC(string name, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();
                ainmation.SetTrigger(name);
                return;
            }
        }
    }

    [PunRPC]
    private void AnimationTBoolRPC(string name, bool b, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.SetBool(name, b);
                return;
            }
        }
    }

    [PunRPC]
    private void PlayClipRPC(string clip, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.Play(clip , 0);
                return;
            }
        }
    }

    [PunRPC]
    private void AnimationFloatRPC(string name, float f, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.SetFloat(name, f);
                return;
            }
        }
    }

    [PunRPC]
    private void AnimationSetLayerWeightRPC(int index, int weight, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.SetLayerWeight(index, weight);
                return;
            }
        }
    }

    [PunRPC]
    private void AnimationResetTriggerRPC(string name, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.ResetTrigger(name);
                return;
            }
        }
    }

    [PunRPC]
    private void AnimationStopRPC(int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.StartPlayback();
                return;
            }
        }
    }

    [PunRPC]
    private void AnimationStartRPC(int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Animator ainmation = player.GetComponent<Animator>();

                ainmation.StopPlayback();
                return;
            }
        }
    }

//----------------------------------------------------------------------------------------Transform-------------------------------------------------------------------------------------------

    public void SetPos(Vector3 pos, int ID)
    {
        photonView.RPC("SetPosRPC", RpcTarget.All, pos, ID);
    }

    [PunRPC]
    private void SetPosRPC(Vector3 pos, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                Transform playerTransform = player.transform;

                if (playerTransform.position != pos)
                {
                    playerTransform.position = pos;
                    //playerTransform.GetComponent<Junpyo.PlayerController_FSM>().networkPosition = pos;
                    playerTransform.GetComponent<Junpyo.PlayerEvent>().AddEvent(new Junpyo.EventStruct(Junpyo.EVENTTYPE.SETPOS, pos));
                }
                return;
            }
        }
    }

//-----------------------------------------------------------------------------------Attack-----------------------------------------------------------------------------------
    public void Attack(int type, bool On, int ID)
    {
        photonView.RPC("AttackRPC", RpcTarget.All, type, On, ID);
    }

    
    [PunRPC]
    public void AttackRPC(int type, bool On, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                player.GetComponent<Junpyo.PlayerSkill>().AttackInst(type, On);
                return;
            }
        }
    }

    public void Test()
    {
        photonView.RPC("TestRPC", RpcTarget.All);
    }


    [PunRPC]
    public void TestRPC()
    {
        
    }

//----------------------------------------------------------------------------------Hurt---------------------------------------------------------------------------------------
    public void Hurt(Vector3 dir, float debuff,Wooseok.ATTACKTYPE type, float dag, int playerID,int enemyID)
    {
        photonView.RPC("HurtRPC", RpcTarget.All, dir, debuff, type, dag, playerID, enemyID);
    }

    [PunRPC]
    public void HurtRPC(Vector3 dir, float debuff, Wooseok.ATTACKTYPE type, float dag, int playerID, int enemyID)
    {
        foreach (Junpyo.PlayerController_FSM enemy in PlayersScript)
        {
            if (enemy.ID == enemyID)
            {
                if (enemy.playerInformation.Curce)
                {
                    dag *= 2.0f;
                }

                if (dag != 0)
                {
                    //상대 피가 데미지보다 작을 시 스메쉬 함수 실행
                    if (enemy.playerInformation.Cur_HP <= dag && 
                        enemy.playerInformation.Cur_HP >= 0
                        && enemy.gameObject.layer != LayerMask.NameToLayer("Imotal"))
                    {
                        foreach (Junpyo.PlayerController_FSM player in PlayersScript)
                        {
                            if (player.ID == playerID)
                            {
                                player.SmashTrigger();
                                Junpyo.KillLogManager.Instance.KillLogAdd(playerID, enemyID);

                                //자신이 죽였다는 것을 적 플레이어에게 알림
                                enemy.EnemyCharacter = player.CharacterName;
                            }
                        }
                    }
                }

                enemy.Hurt(dir, debuff, type, dag);
                return;
            }
        }
    }

//--------------------------------------------------------------------------------State---------------------------------------------------------------------------------------------

    public void StateChange(Junpyo.PLAYERSTATE type, int ID)
    {
        photonView.RPC("StateChangeRPC", RpcTarget.All,type, ID);
    }

    [PunRPC]
    public void StateChangeRPC(Junpyo.PLAYERSTATE type, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                player.GetComponent<Junpyo.PlayerController_FSM>().ChangeState(type);
                return;
            }
        }
    }

    public void Smash(int ID)
    {
        photonView.RPC("SmashRPC", RpcTarget.All, ID);
    }

    [PunRPC]
    public void SmashRPC(int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                player.GetComponent<Junpyo.PlayerController_FSM>().SmashTrigger();
                return;
            }
        }
    }

    public void Dead(bool b, int ID)
    {
        photonView.RPC("DeadRPC", RpcTarget.All, b, ID);
    }

    [PunRPC]
    public void DeadRPC(bool b,int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {
                player.GetComponent<Junpyo.PlayerController_FSM>().Dead(b);
                return;
            }
        }
    }

    public void EnemyListAdd(int OwnerID, int EnemyID)
    {
        photonView.RPC("EnemyListAddRPC", RpcTarget.All, OwnerID, EnemyID);
    }

    [PunRPC]
    public void EnemyListAddRPC(int OwnerID, int EnemyID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == OwnerID.ToString())
            {
                Junpyo.PlayerSkill Skill = player.GetComponent<Junpyo.PlayerSkill>();

                foreach (GameObject Enemy in Players)
                {
                    if (Enemy.name == EnemyID.ToString())
                    {
                        Skill.myUltimate.GetComponent<Wooseok.Skill>().slappedtarget.Add(new Wooseok.Pair<GameObject, int>(Enemy, 0));
                        return;
                    }
                }
            }
        }
    }

    public void IKEvent(Junpyo.PLAYERSTATE envent, bool b, int ID)
    {
        photonView.RPC("IKEventRPC", RpcTarget.All, envent, b, ID);
    }

    [PunRPC]
    public void IKEventRPC(Junpyo.PLAYERSTATE envent, bool b, int ID)
    {
        foreach (GameObject player in Players)
        {
            if (player.name == ID.ToString())
            {

                player.GetComponent<Junpyo.Ik_Controller>().IKEvent(envent, b);
                return;
            }
        }
    }

    public void GagePus(int gage, int ID)
    {
        photonView.RPC("GagePusRPC", RpcTarget.All, gage, ID);
    }

    [PunRPC]
    public void GagePusRPC(int gage, int ID)
    {
        foreach (Junpyo.PlayerController_FSM player in PlayersScript)
        {
            if (player.ID == ID)
            {
                player.UtimateGageUp(gage);
                return;
            }
        }
    }

    public void CameraShaking(int ID)
    {
        photonView.RPC("CameraShakingRPC", RpcTarget.All, ID);
    }

    [PunRPC]
    public void CameraShakingRPC(int ID)
    {
        foreach (Junpyo.PlayerController_FSM player in PlayersScript)
        {
            if (player.ID == ID && player.IsMine)
            {
                Junpyo.CameraManager.Instance().CameraShaking();
                break;
            }
        }
    }
}
