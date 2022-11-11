using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Juhyung
{
    public class Login : MonoBehaviourPun
    {
        [SerializeField] // Login ID Input
        public InputField Input_ID;
        
        [SerializeField] // Login PW Input
        public InputField Input_PW;
        
        [SerializeField] // Login Button
        public GameObject LoginBtn;
        
        [SerializeField] // On Click Login Button after Loading Image
        public GameObject LodingBtn;

        private string ID;
        private string PW;

        private void Awake()
        {
            Screen.SetResolution(1920, 1080, false);
        }

        bool SetIDPass() // Login ID, PW Blank Check
        {
            ID = Input_ID.text.Trim();
            PW = Input_PW.text.Trim();

            if (ID == "" || PW == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void OnClickLoginButton() // Send Order, ID, PW to DataBase Script
        {
            if (!SetIDPass())
            {
                print("ID, PW 비어있음.");
                return;
            }

            WWWForm form = new WWWForm();
            form.AddField("order", "login");
            form.AddField("ID", ID);
            form.AddField("PW", PW);

            StartCoroutine(Post(form));
        }

        public void OnClickRegisterBtn() // LoadScene "Regist_Scene"
        {
            PhotonNetwork.LoadLevel("00_0. Regist_Scene");
        }

        private IEnumerator Post(WWWForm form) // Send Login Info to DataBase Script
        {
            string URL = "https://script.google.com/macros/s/AKfycbxkp2_AAPOKmNt2aA27I6bzkeNEdcmzHmvx5G-z6GG5ffdnPIkW/exec";
            using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
            {
                LoginBtn.SetActive(false);
                LodingBtn.SetActive(true);

                yield return www.SendWebRequest();

                if (www.isDone)
                {
                    PhotonNetwork.NickName = www.downloadHandler.text;

                    if (!www.downloadHandler.text.Equals(""))
                    {
                        LoadingManager.LoadScene("02_0. MainLobby");
                    }
                    else
                    {
                        print("Login Error");
                    }
                }
                else
                {
                    print("Error");
                }
            }
        }
    }

}