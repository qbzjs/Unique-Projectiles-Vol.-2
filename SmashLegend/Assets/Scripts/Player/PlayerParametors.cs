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
            //ü�� ����ȭ
            stream.SendNext(Hp_Bar.fillAmount);

            //���̾� ����ȭ
            stream.SendNext(gameObject.layer);

            //Ui��ġ ����ȭ
            stream.SendNext(UI_Canvas.transform.localPosition);
        }
        else
        {
            //ü�� ����ȭ
            Hp_Bar.fillAmount = (float)stream.ReceiveNext();

            //���̾� ����ȭ
            gameObject.layer = (int)stream.ReceiveNext();

            //���̾� ����ȭ
            UI_Canvas.transform.localPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
