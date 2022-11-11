using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class CharInfoManager : MonoBehaviourPun
    {
        [SerializeField]
        public List<GameObject> avatars;

        [SerializeField]
        public RuntimeAnimatorController controller;

        private GameObject Avatar;

        [SerializeField]
        private GameObject AddOnInfoUI;

        private void Start()
        {
            SetCharacterAni();
        }

        public void OnClickExitBtn()
        {
            PhotonNetwork.LoadLevel("02_1. SelectCharacter_Scene");
        }

        public void OnClickSelectBtn()
        {
            GameManager.Instance.e_SetChar = GameManager.Instance.e_Temp;
            PhotonNetwork.LoadLevel("02_1. SelectCharacter_Scene");
        }

        public void OnClickAddOnBtn()
        {
            AddOnInfoUI.SetActive(true);
        }

        private void SetMiddleAvata(int _idx)
        {
            if (GameObject.Find("Avatar") != null)
            {
                Destroy(Avatar);
            }

            Avatar = Instantiate<GameObject>(avatars[_idx]);

            Avatar.transform.position = new Vector3(0.0f, -1.5f, 0.0f);
            Avatar.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
            Avatar.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);

            Avatar.GetComponent<Animator>().runtimeAnimatorController = controller;

            Avatar.AddComponent<CapsuleCollider>().isTrigger = true;
            Avatar.GetComponent<CapsuleCollider>().center = new Vector3(0.0f, 0.7f, 0.0f);
            Avatar.GetComponent<CapsuleCollider>().height = 2.0f;

            Avatar.AddComponent<ObjectRotate>();

            Avatar.name = "Avatar";
        }

        void SetCharacterAni()
        {
            switch (GameManager.Instance.e_Temp)
            {
                case Junpyo.CHARACTERNAME.GANGNIM:
                    SetMiddleAvata((int)Junpyo.CHARACTERNAME.GANGNIM);

                    break;

                case Junpyo.CHARACTERNAME.CHEPESYU:
                    SetMiddleAvata((int)Junpyo.CHARACTERNAME.CHEPESYU);

                    break;

                case Junpyo.CHARACTERNAME.PENUKUE:
                    SetMiddleAvata((int)Junpyo.CHARACTERNAME.PENUKUE);

                    break;

                case Junpyo.CHARACTERNAME.TRUELOVE:
                    SetMiddleAvata((int)Junpyo.CHARACTERNAME.TRUELOVE);

                    break;

                case Junpyo.CHARACTERNAME.DUSEONIN:
                    SetMiddleAvata((int)Junpyo.CHARACTERNAME.DUSEONIN);

                    break;

                case Junpyo.CHARACTERNAME.PATAL:
                    SetMiddleAvata((int)Junpyo.CHARACTERNAME.PATAL);

                    break;
            }
        }
    }
}
