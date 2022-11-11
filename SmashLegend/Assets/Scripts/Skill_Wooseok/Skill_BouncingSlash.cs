using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Wooseok
{


    public class Skill_BouncingSlash : Skill_Slash
    {
        [SerializeField]
        Vector2 BouncingDirection;

        public Skill_BouncingSlash(GameObject ParentPlayer, Skill FollowUp) : base(ParentPlayer, FollowUp)
        {
        }
        protected override Vector3 DrawDirection(GameObject otherobj)
        {
            Vector3 direction = otherobj.transform.position - ParentPlayer.transform.position;
            direction.y = 0f;
            direction.Normalize();
            direction = direction * BouncingDirection.x + Vector3.down * BouncingDirection.y;

            return direction;
        }
    }
}