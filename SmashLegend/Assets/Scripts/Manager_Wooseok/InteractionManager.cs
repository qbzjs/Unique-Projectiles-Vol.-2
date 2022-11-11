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
            //여기서 게임 매니저한테 플레이어 6명 정보 받아오기 GameManager.Instance();    
        }

        public void Getitem(PlayerController_FSM player, FieldItem item)
        { 
            //플레이어의 perkcheck
            //item의 효과 플레이어에게 일으키기.
            Destroy(item.gameObject);
        }

        public void Hurt(PlayerController_FSM caster, PlayerController_FSM target, float damage, Vector3 direction, float xVelocity = 0f, float yVelocity = 0f, ATTACKTYPE ATKtype = ATTACKTYPE.LIGHTATTACK, float duration = 0f)
        {
            float resultdmg = damage;
            // 1. 캐스터와 타겟의 퍽 확인하기
            // 2. 퍽과 캐스터, 타겟의 퍽에 의거해 데미지, 날아가는 높이, cc 길이 등 확인하기.
            // 3. 최종적으로 target에게 스킬 효과를 적용함.
            // 4. 아래 RPC라고 이름지은 함수를 통해 동기화 함.
        }

        public void AddonCheck(PlayerController_FSM target, ATTACKTYPE type)
        {
            //여기서 입어야 할 효과가 어떤 효과인지 체크하고, 관련 퍽들을 체크함.
        }

        void RPC()
        {
            //이거 위에 Hurt에서 동기화 가능하게 바꿔주기.
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}