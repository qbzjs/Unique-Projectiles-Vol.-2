using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Junpyo
{
    public enum EFFECT { DUST, DEAD }
    public class EffectManager : MonoBehaviourPunCallbacks
    {
        private static EffectManager _instance;

        [SerializeField] GameObject DustEffect;
        [SerializeField] GameObject DeadEffect;

        public static EffectManager Instance
        {
            get
            {
                // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(EffectManager)) as EffectManager;
                }

                return _instance;
            }
        }

        public void EffectInst(EFFECT type, Vector3 pos)
        {
            photonView.RPC("EffectInstRPC", RpcTarget.All, type, pos);
        }

        [PunRPC]
        public void EffectInstRPC(EFFECT type, Vector3 pos)
        {
            switch (type)
            {
                case EFFECT.DUST:
                    MonoBehaviour.Instantiate(DustEffect, pos, Quaternion.Euler(Vector3.zero));
                    break;
                case EFFECT.DEAD:
                    MonoBehaviour.Instantiate(DeadEffect, pos + new Vector3(0, -50, 0), Quaternion.Euler(90, 0, 0));
                    break;
            }
        }
    }
}
