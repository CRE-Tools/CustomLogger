using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace PUCPR.CustomLogger.Editor
{
    public static class CodeGenerator
    {
        public static void CreateEnumFromList(string declaration, string filePath, List<string> list, string nameSpace = "")
        {
            var enumMembers = string.Join(",\n    ", list);
            
            var code = $@"using System;

namespace {nameSpace}
{{
    public enum {declaration}
    {{
        {enumMembers}
    }}
}}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(code);
            }
            AssetDatabase.Refresh();
        }
    }
}
