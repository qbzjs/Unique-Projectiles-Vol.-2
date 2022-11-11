using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Junpyo
{
    public class UltimateUI : MonoBehaviour
    {
        [SerializeField] private Image SkillImage;
        [SerializeField] private Image GageImage;
        [SerializeField] private Text SkillText;
        int CurGage;

        public void UseUtimate()
        {
            CurGage = 0;
            SkillText.text = 0.ToString();
            SkillText.enabled = true;
            SkillImage.enabled = false;
        }

        public void UtimateGageUp(int gage)
        {
            CurGage += gage;

            if (CurGage < 100)
            {
                SkillText.text = CurGage.ToString();
                GageImage.fillAmount = CurGage / 100;
            }
            else
            {
                SkillText.enabled = false;
                SkillImage.enabled = true;
            }
        }
    }
}

