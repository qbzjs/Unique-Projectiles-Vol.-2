using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class OccupationGage : MonoBehaviourPun
    {
        [SerializeField] private float Set1stGageSpeed;                                 // FirstGage Fill Up Speed
        [SerializeField] private float Set2ndGageSpeed;                                 // SecondGage Fill Up Speed
        [SerializeField] private float Set3rdGageSpeed;                                 // ThirdGage Fill Up Speed

        [SerializeField] private Image Red1stGage_Fill;
        [SerializeField] private Image Blue1stGage_Fill;
        [SerializeField] private Image Red2stGage_Fill;
        [SerializeField] private Image Blue2stGage_Fill;
        [SerializeField] private Image Red3stGage_Fill;
        [SerializeField] private Image Blue3stGage_Fill;
        [SerializeField] private Image RedKDGage_Fill;
        [SerializeField] private Image BlueKDGage_Fill;

        [SerializeField] private int MaxScore = 4;                                      // Score of Victory

        [SerializeField] private List<Sprite> i_Num;                                    // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 sprite

        [SerializeField] private GameObject BlankGage;
        [SerializeField] private GameObject RedGage;
        [SerializeField] private GameObject BlueGage;
        [SerializeField] private GameObject k_BlankGage;
        [SerializeField] private GameObject k_RedGage;
        [SerializeField] private GameObject k_BlueGage;
        [SerializeField] private GameObject Red_MatchPoint;
        [SerializeField] private GameObject Blue_MatchPoint;
        [SerializeField] private GameObject Red_MatchOver;
        [SerializeField] private GameObject Blue_MatchOver;

        [SerializeField] private List<Image> RedScore_blank;                            // RedScore Blank image
        [SerializeField] private List<Image> BlueScore_blank;                           // BlueScore Blank image
        [SerializeField] private Sprite RedScore_Fill;                                  // RedScore Fill Sprite
        [SerializeField] private Sprite BlueScore_Fill;                                 // BlueScore Fill Sprite

        [SerializeField] private Image RedScoreBox_Num;                                 // RedTeam Score Num
        [SerializeField] private Image BlueScoreBox_Num;                                // BlueTeam Score Num

        private bool FirstGage = true;                                                  // Setting FirstGage bool

        private int RedOrBlue = 0;                                                      // RedTeam Occupate or BlueTeam Occupate 1 is Red, 2 is Blue

        private float Red1stGage = 0.0f;                                                // First Occupate RedTeam Gage
        private float Blue1stGage = 0.0f;                                               // First Occupate BlueTeam Gage

        private float Red2ndGage = 0.0f;                                                // Second Occupate RedTeam Gage
        private float Blue2ndGage = 0.0f;                                               // Second Occupate BlueTeam Gage

        private float Red3rdGage = 0.0f;                                                // Third Occupate RedTeam Gage
        private float Blue3rdGage = 0.0f;                                               // Third Occupate BlueTeam Gage

        private float GameEndTime;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OccupateGageLogic();
        }

        private void OccupateGageLogic()
        {
            if (OccupationManager.Instance.RedScore != MaxScore && OccupationManager.Instance.BlueScore != MaxScore)
            {
                if (FirstGage)
                {
                    FirstGageLogic();
                }
                else
                {
                    Red1stGage = 0.0f;
                    Blue1stGage = 0.0f;

                    SecondGageLogic();
                    ThirdGageLogic();
                }
            }
            else
            {
                Time.timeScale = 0;

                if(OccupationManager.Instance.RedScore == MaxScore)
                {
                    Red_MatchPoint.SetActive(true);

                    GameEndTime += Time.unscaledDeltaTime;

                    if(GameEndTime >= 2.0f)
                    {
                        Red_MatchPoint.SetActive(false);
                        Red_MatchOver.SetActive(true);

                        if(GameEndTime >= 4.0f)
                        {
                            Time.timeScale = 1;

                            OccupationManager.Instance.GameSet();
                        }
                    }
                }
                else if (OccupationManager.Instance.BlueScore == MaxScore)
                {
                    Blue_MatchPoint.SetActive(true);

                    GameEndTime += Time.unscaledDeltaTime;

                    if (GameEndTime >= 2.0f)
                    {
                        Blue_MatchPoint.SetActive(false);
                        Blue_MatchOver.SetActive(true);

                        if (GameEndTime >= 4.0f)
                        {
                            Time.timeScale = 1;

                            OccupationManager.Instance.GameSet();
                        }
                    }
                }
            }
        }

        private void FirstGageLogic()
        {
            // Under 100%
            if (Red1stGage + Blue1stGage < 100.0f)
            {
                if (OccupationManager.Instance.RedTeamPlayer > 0)
                {
                    Red1stGage += Time.deltaTime * Set1stGageSpeed;
                }

                if (OccupationManager.Instance.BlueTeamPlayer > 0)
                {
                    Blue1stGage += Time.deltaTime * Set1stGageSpeed;
                }

                if (OccupationManager.Instance.RedTeamPlayer == 0 && OccupationManager.Instance.BlueTeamPlayer == 0)
                {
                    if (Red1stGage > 0)
                    {
                        Red1stGage -= Time.deltaTime * Set1stGageSpeed;
                    }

                    if (Blue1stGage > 0)
                    {
                        Blue1stGage -= Time.deltaTime * Set1stGageSpeed;
                    }
                }
            }
            // Over 100%
            else
            {
                if (Red1stGage < 100.0f && Blue1stGage < 100.0f)
                {
                    if (OccupationManager.Instance.RedTeamPlayer == 0 && OccupationManager.Instance.BlueTeamPlayer > 0)
                    {
                        if (Red1stGage > 0)
                        {
                            Red1stGage -= Time.deltaTime * Set1stGageSpeed;
                        }

                        Blue1stGage += Time.deltaTime * Set1stGageSpeed;
                    }

                    else if (OccupationManager.Instance.RedTeamPlayer > 0 && OccupationManager.Instance.BlueTeamPlayer == 0)
                    {
                        Red1stGage += Time.deltaTime * Set1stGageSpeed;

                        if (Blue1stGage > 0)
                        {
                            Blue1stGage -= Time.deltaTime * Set1stGageSpeed;
                        }
                    }

                    else if (OccupationManager.Instance.RedTeamPlayer == 0 && OccupationManager.Instance.BlueTeamPlayer == 0)
                    {
                        if (Red1stGage > 0)
                        {
                            Red1stGage -= Time.deltaTime * Set1stGageSpeed;
                        }

                        if (Blue1stGage > 0)
                        {
                            Blue1stGage -= Time.deltaTime * Set1stGageSpeed;
                        }
                    }
                }
                else
                {
                    FirstGage = false;

                    if (Red1stGage >= 100.0f)
                    {
                        RedOrBlue = 1;
                        BlankGage.SetActive(false);
                        RedGage.SetActive(true);
                    }
                    else if (Blue1stGage >= 100.0f)
                    {
                        RedOrBlue = 2;
                        BlankGage.SetActive(false);
                        BlueGage.SetActive(true);
                    }
                }
            }

            Red1stGage_Fill.fillAmount = Red1stGage / 100.0f;
            Blue1stGage_Fill.fillAmount = Blue1stGage / 100.0f;
        }

        private void SecondGageLogic()
        {
            switch (RedOrBlue)
            {
                case 1: // Red
                    if (OccupationManager.Instance.RedScore != 3)
                    {
                        if (Red2ndGage < 100.0f)
                        {
                            if (OccupationManager.Instance.RedTeamPlayer > 0)
                            {
                                Red2ndGage += Time.deltaTime * Set2ndGageSpeed;
                            }
                            else
                            {
                                if (Red2ndGage > 0)
                                {
                                    Red2ndGage -= Time.deltaTime * Set2ndGageSpeed;
                                }
                            }
                        }
                        else
                        {
                            RedScore_blank[OccupationManager.Instance.RedScore].sprite = RedScore_Fill;
                            OccupationManager.Instance.RedScore++;
                            RedScoreBox_Num.sprite = i_Num[OccupationManager.Instance.RedScore];
                            Red2ndGage = 0.0f;
                        }
                    }
                    else
                    {
                        if (Red2ndGage < 100.0f)
                        {
                            if (OccupationManager.Instance.RedTeamPlayer > 0 && OccupationManager.Instance.BlueTeamPlayer == 0)
                            {
                                k_BlankGage.SetActive(true);
                                k_RedGage.SetActive(true);
                                k_BlueGage.SetActive(false);

                                Red2ndGage += Time.deltaTime * Set2ndGageSpeed;
                            }
                            else if (OccupationManager.Instance.RedTeamPlayer == 0)
                            {
                                if (Red2ndGage > 0)
                                {
                                    Red2ndGage -= Time.deltaTime * Set2ndGageSpeed;
                                }
                            }

                            RedKDGage_Fill.fillAmount = Red2ndGage / 100.0f;
                        }
                        else
                        {
                            RedScore_blank[OccupationManager.Instance.RedScore].sprite = RedScore_Fill;
                            OccupationManager.Instance.RedScore++;
                            RedScoreBox_Num.sprite = i_Num[OccupationManager.Instance.RedScore];

                            k_BlankGage.SetActive(false);
                            k_RedGage.SetActive(false);
                            k_BlueGage.SetActive(false);
                        }
                    }
                    Red2stGage_Fill.fillAmount = Red2ndGage / 100.0f;

                    break;

                case 2: // Blue
                    if (OccupationManager.Instance.BlueScore != 3)
                    {
                        if (Blue2ndGage < 100.0f)
                        {
                            if (OccupationManager.Instance.BlueTeamPlayer > 0)
                            {
                                Blue2ndGage += Time.deltaTime * Set2ndGageSpeed;
                            }
                            else
                            {
                                if (Blue2ndGage > 0)
                                {
                                    Blue2ndGage -= Time.deltaTime * Set2ndGageSpeed;
                                }
                            }
                        }
                        else
                        {
                            BlueScore_blank[OccupationManager.Instance.BlueScore].sprite = BlueScore_Fill;
                            OccupationManager.Instance.BlueScore++;
                            BlueScoreBox_Num.sprite = i_Num[OccupationManager.Instance.BlueScore];
                            Blue2ndGage = 0.0f;
                        }
                    }
                    else
                    {
                        if (Blue2ndGage < 100.0f)
                        {
                            if (OccupationManager.Instance.BlueTeamPlayer > 0 && OccupationManager.Instance.RedTeamPlayer == 0)
                            {
                                k_BlankGage.SetActive(true);
                                k_RedGage.SetActive(false);
                                k_BlueGage.SetActive(true);

                                Blue2ndGage += Time.deltaTime * Set2ndGageSpeed;
                            }
                            else if (OccupationManager.Instance.BlueTeamPlayer == 0)
                            {
                                if (Blue2ndGage > 0)
                                {
                                    Blue2ndGage -= Time.deltaTime * Set2ndGageSpeed;
                                }
                            }

                            BlueKDGage_Fill.fillAmount = Blue2ndGage / 100.0f;
                        }
                        else
                        {
                            BlueScore_blank[OccupationManager.Instance.BlueScore].sprite = BlueScore_Fill;
                            OccupationManager.Instance.BlueScore++;
                            BlueScoreBox_Num.sprite = i_Num[OccupationManager.Instance.BlueScore];

                            k_BlankGage.SetActive(false);
                            k_RedGage.SetActive(false);
                            k_BlueGage.SetActive(false);
                        }
                    }
                    Blue2stGage_Fill.fillAmount = Blue2ndGage / 100.0f;
                    
                    break;

                default:
                    FirstGage = true;

                    break;
            }
        }

        private void ThirdGageLogic()
        {
            switch (RedOrBlue)
            {
                case 1:
                    if (Blue3rdGage < 100.0f)
                    {
                        if (OccupationManager.Instance.BlueTeamPlayer > 0 && OccupationManager.Instance.RedTeamPlayer == 0)
                        {
                            Blue3rdGage += Time.deltaTime * Set3rdGageSpeed;
                        }
                        else if (OccupationManager.Instance.BlueTeamPlayer == 0)
                        {
                            if (Blue3rdGage > 0)
                            {
                                Blue3rdGage -= Time.deltaTime * Set3rdGageSpeed;
                            }
                        }
                    }
                    else
                    {
                        Red2ndGage = 0.0f;
                        Blue3rdGage = 0.0f;
                        RedOrBlue = 2;

                        RedGage.SetActive(false);
                        BlueGage.SetActive(true);

                        k_BlankGage.SetActive(false);
                        k_RedGage.SetActive(false);
                        k_BlueGage.SetActive(false);

                    }
                    Blue3stGage_Fill.fillAmount = Blue3rdGage / 100.0f;

                    break;

                case 2:
                    if (Red3rdGage < 100.0f)
                    {
                        if (OccupationManager.Instance.RedTeamPlayer > 0 && OccupationManager.Instance.BlueTeamPlayer == 0)
                        {
                            Red3rdGage += Time.deltaTime * Set3rdGageSpeed;
                        }
                        else if (OccupationManager.Instance.RedTeamPlayer == 0)
                        {
                            if (Red3rdGage > 0)
                            {
                                Red3rdGage -= Time.deltaTime * Set3rdGageSpeed;
                            }
                        }
                    }
                    else
                    {
                        Blue2ndGage = 0.0f;
                        Red3rdGage = 0.0f;
                        RedOrBlue = 1;

                        BlueGage.SetActive(false);
                        RedGage.SetActive(true);

                        k_BlankGage.SetActive(false);
                        k_RedGage.SetActive(false);
                        k_BlueGage.SetActive(false);
                    }
                    Red3stGage_Fill.fillAmount = Red3rdGage / 100.0f;

                    break;
            }
        }
    }
}