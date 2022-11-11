using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wooseok
{

    public class Skill_EternalSlash : Skill_Slash
    {
        public Skill_EternalSlash(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }

        private void FixedUpdate()
        {
        }

        public void OffSlash()
        {
            this.gameObject.SetActive(false);
        }

        public void OnSlash()
        {
            this.gameObject.SetActive(true);
        }
    }
}