using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class ItemManager : MonoBehaviourPun
    {
        private static ItemManager _instance;

        public static ItemManager Instance
        {
            get
            {
                // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(ItemManager)) as ItemManager;

                    if (_instance == null)
                        Debug.Log("no Singleton obj");
                }
                return _instance;
            }
        }

        public string s_Getitem = null;
        public GameObject s_PlayerNum = null;

        [SerializeField] private float f_RCoolTime;
        [SerializeField] private float f_DCoolTime;

        [SerializeField] private List<GameObject> RandomitemStation;
        [SerializeField] private List<GameObject> RandomitemCoolTime;
        [SerializeField] private List<GameObject> BombitemStation;
        [SerializeField] private List<GameObject> BombitemCoolTime;

        private void Awake()
        {
            Singleton();
        }

        private void Update()
        {
            CoolTime();
        }

        private void Singleton()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void CoolTime()
        {
            for(int i = 0; i < RandomitemStation.Count; ++i)
            {
                if(RandomitemStation[i].GetComponent<CapsuleCollider>().enabled == false)
                {
                    RandomitemCoolTime[i].GetComponent<Image>().fillAmount += Time.deltaTime / f_RCoolTime;
                }
            }

            for (int i = 0; i < BombitemStation.Count; ++i)
            {
                if (BombitemStation[i].GetComponent<CapsuleCollider>().enabled == false)
                {
                    BombitemCoolTime[i].GetComponent<Image>().fillAmount += Time.deltaTime / f_DCoolTime;
                }
            }

            RespawnItem();
        }

        private void RespawnItem()
        {
            for (int i = 0; i < RandomitemStation.Count; ++i)
            {
                if (RandomitemCoolTime[i].GetComponent<Image>().fillAmount >= 1)
                {
                    int i_Random = Random.Range(0, 2);

                    Debug.Log(i_Random);

                    RandomitemCoolTime[i].GetComponent<Image>().fillAmount = 0;

                    switch (i_Random)
                    {
                        case 0:
                            RandomitemStation[i].transform.GetChild(0).gameObject.SetActive(true);
                            RandomitemStation[i].GetComponent<CapsuleCollider>().enabled = true;

                            break;

                        case 1:
                            RandomitemStation[i].transform.GetChild(1).gameObject.SetActive(true);
                            RandomitemStation[i].GetComponent<CapsuleCollider>().enabled = true;

                            break;
                    }
                }
            }

            for (int i = 0; i < BombitemStation.Count; ++i)
            {
                if (BombitemCoolTime[i].GetComponent<Image>().fillAmount >= 1)
                {
                    BombitemCoolTime[i].GetComponent<Image>().fillAmount = 0;

                    BombitemStation[i].transform.GetChild(0).gameObject.SetActive(true);
                    BombitemStation[i].GetComponent<CapsuleCollider>().enabled = true;
                }
            }
        }
    }
}
