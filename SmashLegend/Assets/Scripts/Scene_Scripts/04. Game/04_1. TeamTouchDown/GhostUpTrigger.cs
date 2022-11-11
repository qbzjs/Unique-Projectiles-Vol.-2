using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Juhyung
{
    public class GhostUpTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Red"))
            {
                TTDManager.Instance.Up_RedTeamPlayer++;
            }

            if (other.tag.Equals("Blue"))
            {
                TTDManager.Instance.Up_BlueTeamPlayer++;
            }
            Debug.Log("레드: " + TTDManager.Instance.Up_RedTeamPlayer + "블루" + TTDManager.Instance.Up_BlueTeamPlayer);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Red"))
            {
                TTDManager.Instance.Up_RedTeamPlayer--;
            }

            if (other.tag.Equals("Blue"))
            {
                TTDManager.Instance.Up_BlueTeamPlayer--;
            }
            Debug.Log("레드: " + TTDManager.Instance.Up_RedTeamPlayer + "블루" + TTDManager.Instance.Up_BlueTeamPlayer);
        }
    }
}
