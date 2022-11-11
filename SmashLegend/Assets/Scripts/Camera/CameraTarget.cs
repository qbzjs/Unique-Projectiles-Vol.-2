using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junpyo
{
    public class CameraTarget : MonoBehaviour
    {
        public Transform TagetObj;
        public bool BattleMode;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (TagetObj != null && !BattleMode)
            {
                transform.position = TagetObj.position;
            }
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {

        }
    }
}
