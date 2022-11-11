using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class TTDManager : MonoBehaviourPun
    {
        private static TTDManager _instance;

        public static TTDManager Instance
        {
            get
            {
                // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(TTDManager)) as TTDManager;

                    if (_instance == null)
                        Debug.Log("no Singleton obj");
                }
                return _instance;
            }
        }

        [SerializeField] private List<Sprite> i_Num;                                    // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 sprite
        [SerializeField] public List<GameObject> PlayerPref;                            // PlayerPrefabs
        [SerializeField] public List<GameObject> spawnPoint = new List<GameObject>();   // Player SpawnPoint

        [SerializeField] private float StartDelayTime = 0.0f;                           // Occupation Trigger On before DelayTime

        //[SerializeField] private GameObject OccupationTrigger;                          // Occupation Range

        private GameObject go;                                                          // Player
        private int MyIndex;                                                            // My SpawnPos Idx

        public int Up_RedTeamPlayer = 0;                                 // In Occupation Trigger RedTeam Player Num
        public int Down_RedTeamPlayer = 0;                                 // In Occupation Trigger RedTeam Player Num

        public int Up_BlueTeamPlayer = 0;                                // In Occupation Trigger BlueTeam Player Num
        public int Down_BlueTeamPlayer = 0;                                // In Occupation Trigger BlueTeam Player Num

        [SerializeField] private Image Min;
        [SerializeField] private Image TenSec;
        [SerializeField] private Image OneSec;

        private float GameTime = 180.0f;

        private void Awake()
        {
            Singleton();
            CreatePlayer();
        }

        private void Start()
        {
            StartCoroutine(SetOccupationDelay());
        }

        private void Update()
        {
            GameTimer();
        }

        private void Singleton()
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
        }

        private void CreatePlayer()
        {
            if (PhotonNetwork.InRoom)
            {
                int actorNum = PhotonNetwork.LocalPlayer.ActorNumber;
                Player[] sortedPlayers = PhotonNetwork.PlayerList;

                for (int i = 0; i < sortedPlayers.Length; ++i)
                {
                    if (sortedPlayers[i].ActorNumber == actorNum)
                    {
                        GameManager.Instance.i_PlayerID = i;
                        go = PhotonNetwork.Instantiate
                            (
                                PlayerPref[(int)GameManager.Instance.e_SetChar].name,
                                spawnPoint[GameManager.Instance.i_PlayerID].transform.position,
                                spawnPoint[GameManager.Instance.i_PlayerID].transform.rotation
                            );

                        break;
                    }
                }

                if (GameManager.Instance.i_PlayerID % 2 == 0)
                {
                    photonView.RPC("SetTagRPC", RpcTarget.All, go.GetComponent<PhotonView>().ViewID, "Blue");
                }
                else
                {
                    photonView.RPC("SetTagRPC", RpcTarget.All, go.GetComponent<PhotonView>().ViewID, "Red");
                }
            }
        }

        private void GameTimer()
        {
            if (GameTime <= 0.0f)
            {
                return; // GameEnd
            }

            GameTime -= Time.deltaTime;

            int i_Min = (int)GameTime / 60;
            int i_Tensec = (int)(GameTime - (i_Min * 60)) / 10;
            int i_Onesec = (int)(GameTime - (i_Min * 60) - (i_Tensec * 10));

            Min.sprite = i_Num[i_Min];
            TenSec.sprite = i_Num[i_Tensec];
            OneSec.sprite = i_Num[i_Onesec];
        }

        private void GameMessage()
        {

        }

        //Coroutine Funtion
        IEnumerator SetOccupationDelay()
        {
            yield return new WaitForSeconds(StartDelayTime);

            //OccupationTrigger.SetActive(true);
        }

        // RPC Funtion
        [PunRPC]
        void SetTagRPC(int playerIndex, string tag)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject obj in players)
            {
                if (obj.GetComponent<PhotonView>().ViewID == playerIndex)
                {
                    obj.gameObject.tag = tag;
                    obj.GetComponent<Junpyo.PlayerController_FSM>().SpawnPos = spawnPoint[MyIndex].transform.position;
                    obj.GetComponent<Junpyo.PlayerController_FSM>().SpawnPos = spawnPoint[GameManager.Instance.i_PlayerID].transform.position;

                    GameManager.Instance.AddPlayer(obj);
                }
            }
        }
    }
}
