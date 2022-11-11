using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Juhyung
{
    public class Register : MonoBehaviour
    {
        [SerializeField] // Regist ID Input
        public InputField Input_ID;

        [SerializeField] // Regist PW Input
        public InputField Input_PW;

        [SerializeField] // Regist NickName Input
        public InputField Input_Name;

        [SerializeField] // Regist Button
        public GameObject RegistBtn;

        [SerializeField] // Regist Button
        public GameObject LodingBtn;

        private string ID;
        private string PW;
        private string NickName;

        bool SetIDPass() // Regist ID, PW Blank Check
        {
            ID = Input_ID.text.Trim();
            PW = Input_PW.text.Trim();
            NickName = Input_Name.text.Trim();

            if (ID == "" || PW == "" || NickName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void OnClickRegisterButton() // Send Order, ID, PW, NickName to DataBase Script
        {
            if (!SetIDPass())
            {
                print("ID, PW, Name 비어있음.");
                return;
            }

            WWWForm form = new WWWForm();
            form.AddField("order", "register");
            form.AddField("ID", ID);
            form.AddField("PW", PW);
            form.AddField("NickName", NickName);

            StartCoroutine(Post(form));
        }
        public void OnClickBackBtn() // LoadScene "Login_Scene"
        {
            SceneManager.LoadScene("00_1. Login_Scene");
        }

        private IEnumerator Post(WWWForm form) // Send Regist Info to DataBase Script
        {
            string URL = "https://script.google.com/macros/s/AKfycbxkp2_AAPOKmNt2aA27I6bzkeNEdcmzHmvx5G-z6GG5ffdnPIkW/exec";
            using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
            {
                RegistBtn.SetActive(false);
                LodingBtn.SetActive(true);

                yield return www.SendWebRequest();

                if (www.isDone)
                {
                    print(www.downloadHandler.text);

                    RegistBtn.SetActive(true);
                    LodingBtn.SetActive(false);

                    Input_ID.text = "";
                    Input_PW.text = "";
                    Input_Name.text = "";
                }
                else
                {
                    print("ERROR");
                }
            }
        }
    }
}