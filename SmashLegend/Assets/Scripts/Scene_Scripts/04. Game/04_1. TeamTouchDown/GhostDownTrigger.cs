using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Juhyung
{
    public class GhostDownTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Red"))
            {
                TTDManager.Instance.Down_RedTeamPlayer++;
            }

            if (other.tag.Equals("Blue"))
            {
                TTDManager.Instance.Down_BlueTeamPlayer++;
            }
            Debug.Log("레드: " + TTDManager.Instance.Down_RedTeamPlayer + "블루" + TTDManager.Instance.Down_BlueTeamPlayer);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Red"))
            {
                TTDManager.Instance.Down_RedTeamPlayer--;
            }

            if (other.tag.Equals("Blue"))
            {
                TTDManager.Instance.Down_BlueTeamPlayer--;
            }
            Debug.Log("레드: " + TTDManager.Instance.Down_RedTeamPlayer + "블루" + TTDManager.Instance.Down_BlueTeamPlayer);
        }
    }
}
