using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Juhyung
{
    public class LoadingManager : MonoBehaviourPunCallbacks
    {
        public static string nextScene;

        [SerializeField] // Loading Image
        public Text Loading_Text;
        [SerializeField] public Image progressBar;

        void Start()
        {
            // GameVersion
            PhotonNetwork.GameVersion = "1.13.1";

            // Master Server Connect
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
                StartCoroutine(LoadScene());
            }
            else
            {
                StartCoroutine(LoadScene());
            }
        }

        public static void LoadScene(string sceneName)
        {
            nextScene = sceneName;
            SceneManager.LoadScene("01_0. Loading_Scene");
        }

        IEnumerator LoadScene() // Delay to Connect MasterServer and LoadScene "MainLobby"
        {
            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;

            float timer = 0.0f;
            Debug.Log("들어왔나요.." + op.isDone);
            while (!op.isDone)
            {
                Debug.Log("들어왔나요.." + op.progress);
                yield return null;

                timer += Time.deltaTime;
                if(op.progress < 0.9f)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                    if(progressBar.fillAmount >= op.progress)
                    {
                        timer = 0.0f;
                    }
                }
                else
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, 3);
                    if(progressBar.fillAmount == 1.0f)
                    {
                        op.allowSceneActivation = true;

                        yield break;
                    }
                }
            }
        }
    }
}
