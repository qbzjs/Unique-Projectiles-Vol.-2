using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class MatchManager : MonoBehaviourPunCallbacks
    {
        private float t;
        [SerializeField] float Speed;

        [SerializeField] private List<GameObject> GameMode_Image;
        [SerializeField] private GameObject Matching_Clear;
        [SerializeField] private GameObject Progress_Circle;

        void Awake()
        {
            // All Client get Event
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            GameMode_Image[GameManager.Instance.i_GameMode].SetActive(true);
        }

        private void Update()
        {
            t += Time.deltaTime;
            Progress_Circle.transform.rotation = Quaternion.Euler(0, 0, t * Speed * -1);
            
            if (t * Speed <= -360)
            {
                t = 0;
            }
        }

        public void OnClickExitBtn() // LoadScene "MainLobby" and Leave Matching Room
        {
            PhotonNetwork.LoadLevel("02_0. MainLobby");

            PhotonNetwork.LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    PhotonNetwork.LoadLevel("04_2. OccupationScene");

                    /*Matching_Clear.SetActive(true);
                    gameObject.SetActive(false);*/
                }
            }
        }
    }
}
