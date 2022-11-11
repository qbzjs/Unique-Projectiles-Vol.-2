using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class Bomb_Item : Item
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

        }
    }
}

