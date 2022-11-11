using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Juhyung
{
    public class SelectCharacterManager : MonoBehaviourPun
    {
        public void OnClickExitBtn()
        {
            PhotonNetwork.LoadLevel("02_0. MainLobby");
        }
        public void OnClickGangnim()
        {
            GameManager.Instance.e_Temp = Junpyo.CHARACTERNAME.GANGNIM;
            PhotonNetwork.LoadLevel("03_0. Gangnim");
        }

        public void OnClickChePeSyu()
        {
            GameManager.Instance.e_Temp = Junpyo.CHARACTERNAME.CHEPESYU;
            PhotonNetwork.LoadLevel("03_1. Chepesyu");
        }

        public void OnClickPenuKue()
        {
            GameManager.Instance.e_Temp = Junpyo.CHARACTERNAME.PENUKUE;
            PhotonNetwork.LoadLevel("03_2. Penukeu");
        }

        public void OnClickTrueLove()
        {
            GameManager.Instance.e_Temp = Junpyo.CHARACTERNAME.TRUELOVE;
            PhotonNetwork.LoadLevel("03_3. TrueLove");
        }

        public void OnClickDuseonIn()
        {
            GameManager.Instance.e_Temp = Junpyo.CHARACTERNAME.DUSEONIN;
            PhotonNetwork.LoadLevel("03_4. Duseonin");
        }

        public void OnClickPatal()
        {
            GameManager.Instance.e_Temp = Junpyo.CHARACTERNAME.PATAL;
            PhotonNetwork.LoadLevel("03_5. Patal");
        }
    }
}
