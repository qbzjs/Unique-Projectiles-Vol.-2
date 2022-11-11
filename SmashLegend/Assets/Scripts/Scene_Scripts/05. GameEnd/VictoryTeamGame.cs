using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class VictoryTeamGame : MonoBehaviourPun
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClickExitBtn()
        {
            LoadingManager.LoadScene("02_0. MainLobby");
        }
    }
}
