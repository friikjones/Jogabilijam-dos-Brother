using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour {

    
        public delegate void PreCullEvent();
        public static PreCullEvent onPreCull;

        void OnPreCull()
        {
            if (onPreCull != null)
            {
                onPreCull();
            }
        }
    }