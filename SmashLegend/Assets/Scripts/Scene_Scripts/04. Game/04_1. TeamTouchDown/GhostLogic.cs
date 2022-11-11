using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Juhyung
{
    public class GhostLogic : MonoBehaviourPun
    {
        #region Logic
        [SerializeField] private List<GameObject> Up_WayPoint;
        [SerializeField] private List<GameObject> Down_WayPoint;

        [SerializeField] private List<GameObject> Up_RedCheckPoint;
        [SerializeField] private List<GameObject> Up_BlueCheckPoint;
        
        [SerializeField] private List<GameObject> Down_RedCheckPoint;
        [SerializeField] private List<GameObject> Down_BlueCheckPoint;

        [SerializeField] private Image RedTeamFill;
        [SerializeField] private Image BlueTeamFill;

        [SerializeField] private float fillSpeed;

        private Vector3 cur_Pos;

        [SerializeField] private int Up_Way_Idx = 7;
        [SerializeField] private int Down_Way_Idx = 4;

        private bool Up_Blue = true;
        private bool Up_Red = true;
        private bool Down_Blue = true;
        private bool Down_Red = true;

        private float speed = 3.0f;

        private bool Up_Check;
        private bool Down_Check;
        #endregion Logic

        #region Up_UI
        [SerializeField] Image Up_Ghost;

        [SerializeField] Image Up_Red_Fill;
        [SerializeField] GameObject Up_Red_Occupation;

        [SerializeField] Image Up_Blue_Fill;
        [SerializeField] GameObject Up_Blue_Occupation;

        [SerializeField] Image Up_Red_Final_Fill;
        [SerializeField] Image Up_Blue_Final_Fill;

        #endregion Up_UI
        void Start()
        {
            
        }

        void Update()
        {
            if((Down_RedCheckPoint.Count != 0 && Down_BlueCheckPoint.Count != 0) && (Up_RedCheckPoint.Count != 0 && Up_BlueCheckPoint.Count != 0))
            {
                TTDLogic();
            }
            else
            {
                Time.timeScale = 0;
            }
        }


        private void TTDLogic()
        {
            if(transform.position.z > 1.5f)
            {
                if (Up_Red_Fill.fillAmount == 0 && Up_Red_Occupation.activeInHierarchy == true)
                {
                    Up_Red_Occupation.SetActive(false);
                    Up_Ghost.enabled = true;
                }

                if (Up_Blue_Fill.fillAmount == 0 && Up_Blue_Occupation.activeInHierarchy == true)
                {
                    Up_Blue_Occupation.SetActive(false);
                    Up_Ghost.enabled = true;
                }

                if (TTDManager.Instance.Up_BlueTeamPlayer > 0 && TTDManager.Instance.Up_RedTeamPlayer == 0)
                {
                    if (Up_Blue_Final_Fill.fillAmount == 0 && Up_BlueCheckPoint.Count == 1)
                    {
                        Up_Ghost.enabled = true;
                    }

                    if (RedTeamFill.fillAmount == 0.0f)
                    {
                        if (Up_Blue)
                        {
                            Up_Way_Idx++;

                            Up_Blue = false;
                            Up_Red = true;
                        }
                    }

                    if (Vector3.Distance(Up_WayPoint[Up_Way_Idx].transform.position, cur_Pos) == 0.0f)
                    {
                        UpGage();
                    }

                    Up_Ghost.color = new Color(0.0f, 0.0f, 255.0f, 255.0f);

                    UpMove(Up_Way_Idx);
                }
                else if (TTDManager.Instance.Up_BlueTeamPlayer == 0 && TTDManager.Instance.Up_RedTeamPlayer > 0)
                {
                    if (Up_Red_Final_Fill.fillAmount == 0 && Up_RedCheckPoint.Count == 1)
                    {
                        Up_Ghost.enabled = true;
                    }

                    if (BlueTeamFill.fillAmount == 0.0f)
                    {
                        if (Up_Red)
                        {
                            Up_Way_Idx--;

                            Up_Blue = true;
                            Up_Red = false;
                        }
                    }

                    if (Vector3.Distance(Up_WayPoint[Up_Way_Idx].transform.position, cur_Pos) == 0.0f)
                    {
                        UpGage();
                    }

                    Up_Ghost.color = new Color(255.0f, 0.0f, 0.0f, 255.0f);

                    UpMove(Up_Way_Idx);
                }
                else
                {
                    Up_Ghost.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                }
            }
            else if (transform.position.z < 1.5f)
            {
                if (TTDManager.Instance.Down_BlueTeamPlayer > 0 && TTDManager.Instance.Down_RedTeamPlayer == 0)
                {
                    if(RedTeamFill.fillAmount == 0.0f)
                    {
                        if (Down_Blue)
                        {
                            Down_Way_Idx++;

                            Down_Blue = false;
                            Down_Red = true;
                        }
                    }

                    if (Vector3.Distance(Down_WayPoint[Down_Way_Idx].transform.position, cur_Pos) == 0.0f)
                    {
                        DownGage();
                    }


                    DownMove(Down_Way_Idx);
                }
                else if (TTDManager.Instance.Down_BlueTeamPlayer == 0 && TTDManager.Instance.Down_RedTeamPlayer > 0)
                {
                    if(BlueTeamFill.fillAmount == 0.0f)
                    {
                        if (Down_Red)
                        {
                            Down_Way_Idx--;

                            Down_Blue = true;
                            Down_Red = false;
                        }
                    }

                    if (Vector3.Distance(Down_WayPoint[Down_Way_Idx].transform.position, cur_Pos) == 0.0f)
                    {
                        DownGage();
                    }


                    DownMove(Down_Way_Idx);
                }
                else
                {

                }
            }
        }

        private void UpMove(int Way_Idx)
        {
            cur_Pos = transform.position;

            if (Way_Idx < Up_WayPoint.Count)
            {
                float step = speed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(cur_Pos, Up_WayPoint[Way_Idx].transform.position, step);
            }
        }

        private void DownMove(int Way_Idx)
        {
            cur_Pos = transform.position;

            if (Way_Idx < Down_WayPoint.Count)
            {
                float step = speed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(cur_Pos, Down_WayPoint[Way_Idx].transform.position, step);
            }
        }
        private void UpGage()
        {
            if (TTDManager.Instance.Up_BlueTeamPlayer > 0 && TTDManager.Instance.Up_RedTeamPlayer == 0)
            {
                if (RedTeamFill.fillAmount == 0.0f)
                {
                    for (int i = 0; i < Up_RedCheckPoint.Count; ++i)
                    {
                        if (Vector3.Distance(Up_RedCheckPoint[i].transform.position, cur_Pos) == 0.0f)
                        {
                            Up_Ghost.enabled = false;
                            Up_Blue_Occupation.SetActive(true);

                            if (BlueTeamFill.fillAmount < 1.0f)
                            {
                                BlueTeamFill.fillAmount += Time.deltaTime / fillSpeed;
                                if(Up_RedCheckPoint.Count != 1)
                                {
                                    Up_Blue_Fill.fillAmount += Time.deltaTime / fillSpeed;
                                }
                                else
                                {
                                    Up_Blue_Occupation.SetActive(false);
                                    Up_Red_Final_Fill.fillAmount += Time.deltaTime / fillSpeed;
                                }
                                Up_Check = false;

                                break;
                            }
                            else
                            {
                                Up_RedCheckPoint.RemoveAt(i);
                                BlueTeamFill.fillAmount = 0.0f;
                                Up_Blue_Fill.fillAmount = 0.0f;

                                Up_Check = true;
                                
                                if (Up_RedCheckPoint.Count == 0)
                                {
                                    Up_Check = false;
                                    BlueTeamFill.fillAmount = 1.0f;
                                }

                                break;
                            }
                        }
                        else
                        {
                            Up_Check = true;
                        }
                    }
                }
                else
                {
                    if (Up_BlueCheckPoint.Count != 1)
                    {
                        RedTeamFill.fillAmount -= Time.deltaTime / fillSpeed;
                        Up_Red_Fill.fillAmount -= Time.deltaTime / fillSpeed;
                    }
                    else
                    {
                        RedTeamFill.fillAmount -= Time.deltaTime / fillSpeed;
                        Up_Blue_Final_Fill.fillAmount -= Time.deltaTime / fillSpeed;
                    }
                }

                if (Up_Check)
                {
                    Up_Way_Idx++;
                    Up_Check = false;
                }
            }
            else if (TTDManager.Instance.Up_BlueTeamPlayer == 0 && TTDManager.Instance.Up_RedTeamPlayer > 0)
            {
                if (BlueTeamFill.fillAmount == 0.0f)
                {
                    for (int i = 0; i < Up_BlueCheckPoint.Count; ++i)
                    {
                        if (Vector3.Distance(Up_BlueCheckPoint[i].transform.position, cur_Pos) == 0.0f)
                        {
                            Up_Ghost.enabled = false;
                            Up_Red_Occupation.SetActive(true);

                            if (RedTeamFill.fillAmount < 1.0f)
                            {
                                RedTeamFill.fillAmount += Time.deltaTime / fillSpeed;

                                if (Up_BlueCheckPoint.Count != 1)
                                {
                                    Up_Red_Fill.fillAmount += Time.deltaTime / fillSpeed;
                                }
                                else
                                {
                                    Up_Red_Occupation.SetActive(false);
                                    Up_Blue_Final_Fill.fillAmount += Time.deltaTime / fillSpeed;
                                }
                                Up_Check = false;

                                break;
                            }
                            else
                            {
                                Up_BlueCheckPoint.RemoveAt(i);
                                RedTeamFill.fillAmount = 0.0f;
                                Up_Red_Fill.fillAmount = 0.0f;

                                Up_Check = true;

                                if (Up_BlueCheckPoint.Count == 0)
                                {
                                    Up_Check = false;
                                    RedTeamFill.fillAmount = 1.0f;
                                }

                                break;
                            }
                        }
                        else
                        {
                            Up_Check = true;
                        }
                    }
                }
                else
                {
                    if (Up_RedCheckPoint.Count != 1)
                    {
                        BlueTeamFill.fillAmount -= Time.deltaTime / fillSpeed;
                        Up_Blue_Fill.fillAmount -= Time.deltaTime / fillSpeed;
                    }
                    else
                    {
                        BlueTeamFill.fillAmount -= Time.deltaTime / fillSpeed;
                        Up_Red_Final_Fill.fillAmount -= Time.deltaTime / fillSpeed;
                    }
                }

                if (Up_Check)
                {
                    Up_Way_Idx--;
                    Up_Check = false;
                }
            }
        }

        private void DownGage()
        {
            if (TTDManager.Instance.Down_BlueTeamPlayer > 0 && TTDManager.Instance.Down_RedTeamPlayer == 0)
            {
                if(RedTeamFill.fillAmount == 0.0f)
                {
                    for (int i = 0; i < Down_RedCheckPoint.Count; ++i)
                    {
                        if (Vector3.Distance(Down_RedCheckPoint[i].transform.position, cur_Pos) == 0.0f)
                        {
                            if (BlueTeamFill.fillAmount < 1.0f)
                            {
                                BlueTeamFill.fillAmount += Time.deltaTime / fillSpeed;
                                Down_Check = false;

                                break;
                            }
                            else
                            {
                                Down_RedCheckPoint.RemoveAt(i);
                                BlueTeamFill.fillAmount = 0.0f;

                                Down_Check = true;

                                if (Down_RedCheckPoint.Count == 0)
                                {
                                    Down_Check = false;
                                    BlueTeamFill.fillAmount = 1.0f;
                                }

                                break;
                            }
                        }
                        else
                        {
                            Down_Check = true;
                        }
                    }
                }
                else
                {
                    RedTeamFill.fillAmount -= Time.deltaTime / speed;
                }

                if (Down_Check)
                {
                    Down_Way_Idx++;
                    Down_Check = false;
                }
            }
            else if (TTDManager.Instance.Down_BlueTeamPlayer == 0 && TTDManager.Instance.Down_RedTeamPlayer > 0)
            {
                if(BlueTeamFill.fillAmount == 0.0f)
                {
                    for (int i = 0; i < Down_BlueCheckPoint.Count; ++i)
                    {
                        if (Vector3.Distance(Down_BlueCheckPoint[i].transform.position, cur_Pos) == 0.0f)
                        {
                            if (RedTeamFill.fillAmount < 1.0f)
                            {
                                RedTeamFill.fillAmount += Time.deltaTime / fillSpeed;
                                Down_Check = false;

                                break;
                            }
                            else
                            {
                                Down_BlueCheckPoint.RemoveAt(i);
                                RedTeamFill.fillAmount = 0.0f;
                                Down_Check = true;

                                if (Down_BlueCheckPoint.Count == 0)
                                {
                                    Down_Check = false;
                                    RedTeamFill.fillAmount = 1.0f;
                                }

                                break;
                            }
                        }
                        else
                        {
                            Down_Check = true;
                        }
                    }
                }
                else
                {
                    BlueTeamFill.fillAmount -= Time.deltaTime / speed;
                }

                if (Down_Check)
                {
                    Down_Way_Idx--;
                    Down_Check = false;
                }
            }
        }
    }
}