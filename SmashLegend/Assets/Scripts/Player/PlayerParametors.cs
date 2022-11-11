using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerParametors : MonoBehaviourPun , IPunObservable
{
    public Image Hp_Bar;
    public Canvas UI_Canvas;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //체력 동기화
            stream.SendNext(Hp_Bar.fillAmount);

            //레이어 동기화
            stream.SendNext(gameObject.layer);

            //Ui위치 동기화
            stream.SendNext(UI_Canvas.transform.localPosition);
        }
        else
        {
            //체력 동기화
            Hp_Bar.fillAmount = (float)stream.ReceiveNext();

            //레이어 동기화
            gameObject.layer = (int)stream.ReceiveNext();

            //레이어 동기화
            UI_Canvas.transform.localPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
