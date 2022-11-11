using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Juhyung
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void DestroyItem();
        public abstract void RunItem();

        void Start()
        {
            
        }

        private void Update()
        {
            DestroyItem();
        }
    }
}
