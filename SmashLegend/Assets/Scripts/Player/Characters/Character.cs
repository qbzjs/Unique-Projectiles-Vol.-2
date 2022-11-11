using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace Junpyo
{
    public enum CHARACTERNAME { CHEPESYU, GANGNIM, PENUKUE, TRUELOVE, DUSEONIN, PATAL }
    [Serializable]
    public struct PlayerInformation
    {
        //ĳ�����̸�
        public CHARACTERNAME Name;

        //HP
        public float HP_Max;
        public float Cur_HP;

        //Ư����ų 
        public float Skill_Time; //�ñر� ���� ä�����ϴ� ��
        public bool SkillOn;
        public bool SkillImotal;
        public bool IsSkillUplayer;

        //�ñر�
        public float UltGage_Max;
        public float Cur_UltGage;
        public bool UltimateOn;
        public bool UtimatePrepare;
        public bool UltimateImotal;

        //ADD_ON
        public Wooseok.Add_On Common;
        public Wooseok.Add_On Special;
        public Wooseok.Add_On Ultimate;

        //�̵�
        public float RunSpeed;
        public float CurRunSpeed;

        //����
        public float JumpDistance;
        public float CurJumpDistance;
        public float JumpPower;
        public bool IsAttackUplayer;
        public bool JumpSkillPrepare;

        //���� Effect
        public PlayerSkill Skills;

        //���� �̻�
        public bool Fear;
        public bool Curce;

        //�Ŵ޸��� ��
        public float HangSlope;
        public float HangStrigh;
        public float Hang_Y;

        //���� �� ���۾Ƹ����� Ȯ���ϱ� ���� ����
        public bool IsImotal_Skill;
        public bool IsImotal_Ultimate;


        public void Initialization(CHARACTERNAME name)
        {
            //CHARACTERNAME�� ���� �ʱ�ȭ�� �ٸ��� ����
            switch (name)
            {
                case CHARACTERNAME.CHEPESYU:
                    {
                        ChePeSyu();
                    }
                    break;

                case CHARACTERNAME.GANGNIM:
                    {
                        Gangnim();
                    }
                    break;

                case CHARACTERNAME.PENUKUE:
                    {
                        PenuKue();
                    }
                    break;

                case CHARACTERNAME.TRUELOVE:
                    {
                        TrueLove();
                    }
                    break;

                case CHARACTERNAME.DUSEONIN:
                    {
                        Duseonin();
                    }
                    break;

                case CHARACTERNAME.PATAL:
                    {
                        Patal();
                    }
                    break;
                default:
                    break;
            }

        }

        public void ChePeSyu()
        {
            Name = CHARACTERNAME.CHEPESYU;

            HP_Max = 2000;
            Cur_HP = 2000;

            Skill_Time = 5;
            SkillOn = true;
            JumpSkillPrepare = false;

            IsSkillUplayer = true;
            IsAttackUplayer = true;

            UltGage_Max = 100;
            Cur_UltGage = 0;
            UltimateOn = false;
            UtimatePrepare = true;

            Common = null;
            Special = null;
            Ultimate = null;

            CurRunSpeed = 4.0f;
            RunSpeed = CurRunSpeed;

            CurJumpDistance = 2.5f;
            JumpDistance = CurJumpDistance;
            JumpPower = 400.0f;

            Skills = new ChePeSyuSkill();

            Fear = false;
            Curce = false;

            HangSlope = 0.25f;
            HangStrigh = 0.2f;
            Hang_Y = -1.43f;
        }

        public void PenuKue()
        {
            Name = CHARACTERNAME.PENUKUE;

            HP_Max = 2000;
            Cur_HP = 2000;

            Skill_Time = 2;
            SkillOn = true;
            JumpSkillPrepare = false;

            IsSkillUplayer = false;
            IsAttackUplayer = true;


            UltGage_Max = 100;
            Cur_UltGage = 0;
            UltimateOn = false;
            UtimatePrepare = false;

            Common = null;
            Special = null;
            Ultimate = null;

            CurRunSpeed = 4.0f;
            RunSpeed = CurRunSpeed;

            CurJumpDistance = 2.5f;
            JumpDistance = CurJumpDistance;
            JumpPower = 400.0f;

            Skills = new ChePeSyuSkill();

            Fear = false;
            Curce = false;

            HangSlope = 0.25f;
            HangStrigh = 0.2f;
            Hang_Y = -1.58f;
        }

        public void Gangnim()
        {
            Name = CHARACTERNAME.GANGNIM;

            HP_Max = 2000;
            Cur_HP = 2000;

            Skill_Time = 5;
            SkillOn = true;
            JumpSkillPrepare = true;

            IsSkillUplayer = false;
            IsAttackUplayer = false;

            UltGage_Max = 100;
            Cur_UltGage = 0;
            UltimateOn = false;
            UtimatePrepare = false;

            Common = null;
            Special = null;
            Ultimate = null;

            CurRunSpeed = 4.0f;
            RunSpeed = CurRunSpeed;

            CurJumpDistance = 2.5f;
            JumpDistance = CurJumpDistance;
            JumpPower = 400.0f;

            Skills = new ChePeSyuSkill();

            Fear = false;
            Curce = false;

            HangSlope = 0.5f;
            HangStrigh = 0f;
            Hang_Y = -1.4f;
        }

        public void Duseonin()
        {
            Name = CHARACTERNAME.DUSEONIN;

            HP_Max = 2000;
            Cur_HP = 2000;

            Skill_Time = 10;
            SkillOn = true;
            JumpSkillPrepare = false;

            IsSkillUplayer = false;
            IsAttackUplayer = true;

            UltGage_Max = 100;
            Cur_UltGage = 0;
            UltimateOn = false;
            UtimatePrepare = false;

            Common = null;
            Special = null;
            Ultimate = null;

            CurRunSpeed = 4.0f;
            RunSpeed = CurRunSpeed;

            CurJumpDistance = 2.5f;
            JumpDistance = CurJumpDistance;
            JumpPower = 400.0f;

            Skills = new ChePeSyuSkill();

            Fear = false;
            Curce = false;

            HangSlope = 0.5f;
            HangStrigh = 0f;
            Hang_Y = -1.58f;
        }

        public void TrueLove()
        {
            Name = CHARACTERNAME.TRUELOVE;

            HP_Max = 2000;
            Cur_HP = 2000;

            Skill_Time = 3;
            SkillOn = true;
            JumpSkillPrepare = false;

            IsSkillUplayer = true;
            IsAttackUplayer = false;

            UltGage_Max = 100;
            Cur_UltGage = 0;
            UltimateOn = true;
            UtimatePrepare = false; 

            Common = null;
            Special = null;
            Ultimate = null;

            CurRunSpeed = 4.0f;
            RunSpeed = CurRunSpeed;

            CurJumpDistance = 2.5f;
            JumpDistance = CurJumpDistance;
            JumpPower = 400.0f;

            Skills = new ChePeSyuSkill();

            Fear = false;
            Curce = false;

            HangSlope = 0.5f;
            HangStrigh = 0.0f;
            Hang_Y = -1.6f;

        }

        public void Patal()
        {
            Name = CHARACTERNAME.PATAL;

            HP_Max = 2000;
            Cur_HP = 2000;

            Skill_Time = 5;
            SkillOn = true;
            JumpSkillPrepare = false;

            IsSkillUplayer = true;
            IsAttackUplayer = false;

            UltGage_Max = 100;
            Cur_UltGage = 0;
            UltimateOn = false;
            UtimatePrepare = false;

            Common = null;
            Special = null;
            Ultimate = null;

            CurRunSpeed = 4.0f;
            RunSpeed = CurRunSpeed;

            CurJumpDistance = 2.5f;
            JumpDistance = CurJumpDistance;
            JumpPower = 400.0f;

            Skills = new ChePeSyuSkill();

            Fear = false;
            Curce = false;

            HangSlope = 0.2f;
            HangStrigh = 0.0f;
            Hang_Y = -1.77f;

        }
    }
}
