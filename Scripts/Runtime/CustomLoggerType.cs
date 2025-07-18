using System;
using UnityEngine;

namespace PUCPR.CustomLogger
{
   [Serializable]
    public class CustomLoggerType
    {
        public string keyName;
        public Color color;
        public bool showLog;

        public CustomLoggerType()
        {
            this.keyName = "NewType";
            this.color = Color.white;
            this.showLog = true;
        }
    }
}
