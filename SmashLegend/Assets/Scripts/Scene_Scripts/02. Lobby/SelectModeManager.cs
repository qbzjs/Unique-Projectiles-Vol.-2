using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class SelectModeManager : MonoBehaviourPun
    {
        public void OnClickBackBtn()
        {
            PhotonNetwork.LoadLevel("02_0. MainLobby");
        }

        public void OnClickHomeBtn()
        {
            PhotonNetwork.LoadLevel("02_0. MainLobby");
        }

        public void OnClick1vs1Btn()
        {
            GameManager.Instance.i_GameMode = 0;
            PhotonNetwork.LoadLevel("02_0. MainLobby");
        }

        public void OnClick3vs3Btn()
        {
            GameManager.Instance.i_GameMode = 1;
            PhotonNetwork.LoadLevel("02_0. MainLobby");
        }

        public void OnClick3vs3_2Btn()
        {
            GameManager.Instance.i_GameMode = 2;
            PhotonNetwork.LoadLevel("02_0. MainLobby");
        }
    }
}
