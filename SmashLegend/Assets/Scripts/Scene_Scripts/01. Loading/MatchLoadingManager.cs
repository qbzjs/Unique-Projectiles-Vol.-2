using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Juhyung
{
    public class MatchLoadingManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] float Speed;

        [SerializeField] private List<GameObject> GameMode_Image;

        [SerializeField] private Text Loading_Num;

        void Awake()
        {
            // All Client get Event
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            GameMode_Image[GameManager.Instance.i_GameMode].SetActive(true);

            Debug.Log("µé¾î°¬³ª¿ä" + GameManager.Instance.i_GameMode);

            switch (GameManager.Instance.i_GameMode)
            {
                case 0:
                    Debug.Log("µé¾î°¬³ª¿ä" + GameManager.Instance.i_GameMode);
                    StartCoroutine(LoadScene("04_2. OccupationScene"));

                    break;

                case 1:
                    StartCoroutine(LoadScene("04_1. TeamTouchDownScene"));

                    break;

                case 2:
                    StartCoroutine(LoadScene("04_2. OccupationScene"));

                    break;
            }
        }

        IEnumerator LoadScene(string nextScene) // Delay to Connect MasterServer and LoadScene "MainLobby"
        {
            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;
            Debug.Log("µé¾î°¬³ª¿ä" + op.isDone);
            while (!op.isDone)
            {
                yield return null;
                Debug.Log("µé¾î°¬³ª¿ä" + op.progress);
                if (op.progress < 0.9f)
                {
                    Loading_Num.text = ((int)(op.progress * 100)).ToString() + "%";
                }
                else
                {
                    StartCoroutine(Delay());

                    if (Loading_Num.text == "100%")
                    {
                        op.allowSceneActivation = true;

                        yield break;
                    }
                }
            }
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(2.0f);

            Loading_Num.text = "100%";
        }
    }
}
