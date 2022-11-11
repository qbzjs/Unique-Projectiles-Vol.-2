using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Junpyo;
using Photon.Pun;


namespace Wooseok
{
    public class InteractionManager : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        PlayerController_FSM player0;
        PlayerController_FSM player1;
        PlayerController_FSM player2;
        PlayerController_FSM player3;
        PlayerController_FSM player4;
        PlayerController_FSM player5;



        void Start()
        {
            player0 = GameManager.Instance.Players[0].GetComponent<PlayerController_FSM>();
            player1 = GameManager.Instance.Players[1].GetComponent<PlayerController_FSM>();
            player2 = GameManager.Instance.Players[2].GetComponent<PlayerController_FSM>();
            player3 = GameManager.Instance.Players[3].GetComponent<PlayerController_FSM>();
            player4 = GameManager.Instance.Players[4].GetComponent<PlayerController_FSM>();
            player5 = GameManager.Instance.Players[5].GetComponent<PlayerController_FSM>();
            //���⼭ ���� �Ŵ������� �÷��̾� 6�� ���� �޾ƿ��� GameManager.Instance();    
        }

        public void Getitem(PlayerController_FSM player, FieldItem item)
        { 
            //�÷��̾��� perkcheck
            //item�� ȿ�� �÷��̾�� ����Ű��.
            Destroy(item.gameObject);
        }

        public void Hurt(PlayerController_FSM caster, PlayerController_FSM target, float damage, Vector3 direction, float xVelocity = 0f, float yVelocity = 0f, ATTACKTYPE ATKtype = ATTACKTYPE.LIGHTATTACK, float duration = 0f)
        {
            float resultdmg = damage;
            // 1. ĳ���Ϳ� Ÿ���� �� Ȯ���ϱ�
            // 2. �ܰ� ĳ����, Ÿ���� �ܿ� �ǰ��� ������, ���ư��� ����, cc ���� �� Ȯ���ϱ�.
            // 3. ���������� target���� ��ų ȿ���� ������.
            // 4. �Ʒ� RPC��� �̸����� �Լ��� ���� ����ȭ ��.
        }

        public void AddonCheck(PlayerController_FSM target, ATTACKTYPE type)
        {
            //���⼭ �Ծ�� �� ȿ���� � ȿ������ üũ�ϰ�, ���� �ܵ��� üũ��.
        }

        void RPC()
        {
            //�̰� ���� Hurt���� ����ȭ �����ϰ� �ٲ��ֱ�.
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}