using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Junpyo;
using Photon.Pun;

namespace Wooseok
{ 
    public class Skill_Coffin : MonoBehaviourPunCallbacks
    {

        CharacterController myController;
        public float MaxHP;
        public float CurHP;
        private void Awake()
        {
            CurHP = MaxHP;
            myController = this.gameObject.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(CurHP <= 0)
            {
                this.gameObject.SetActive(false);
            }
           // myController.SimpleMove(Vector3.zero);
        }
    }
}