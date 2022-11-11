using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Juhyung
{
    public class UltiPack_Item : Item
    {
        [SerializeField] GameObject ItemStation;

        public override void DestroyItem()
        {
            if (ItemStation.name.Equals(ItemManager.Instance.s_Getitem))
            {
                ItemManager.Instance.s_Getitem = null;
                ItemStation.GetComponent<CapsuleCollider>().enabled = false;
                RunItem();
                gameObject.SetActive(false);
            }
        }

        public override void RunItem()
        {
            if (ItemManager.Instance.s_PlayerNum.transform.GetComponent<Junpyo.PlayerController_FSM>().playerInformation.Cur_UltGage + 40.0f
                >= ItemManager.Instance.s_PlayerNum.GetComponent<Junpyo.PlayerController_FSM>().playerInformation.UltGage_Max)
            {
                ItemManager.Instance.s_PlayerNum.GetComponent<Junpyo.PlayerController_FSM>().playerInformation.Cur_UltGage
                    = ItemManager.Instance.s_PlayerNum.GetComponent<Junpyo.PlayerController_FSM>().playerInformation.UltGage_Max;
            }
            else
            {
                ItemManager.Instance.s_PlayerNum.GetComponent<Junpyo.PlayerController_FSM>().playerInformation.Cur_UltGage += 40.0f;
            }
        }
    }
}
