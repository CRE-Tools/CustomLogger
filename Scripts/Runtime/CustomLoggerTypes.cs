using System;
using UnityEngine;

namespace PUCPR.CustomLogger
{
   [Serializable]
    public class CustomLoggerTypes
    {
        public string keyName;
        public Color color;
        public bool showLog;

        public CustomLoggerTypes()
        {
            this.keyName = "newType";
            this.color = Color.white;
            this.showLog = true;
        }
    }
}
