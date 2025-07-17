using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace PUCPR.CustomLogger
{
    public static class CodeGenerator
    {
        public static void CreateEnumFromList(string declaration, string filePath, List<string> list, string nameSpace = "")
        {
            var compileUnit = new CodeCompileUnit();
            var ns = new CodeNamespace(nameSpace);
            compileUnit.Namespaces.Add(ns);

            var classType = new CodeTypeDeclaration(declaration);
            classType.IsEnum = true;

            classType.Attributes = MemberAttributes.Public;
            foreach (var item in list)
            {
                CodeMemberField f = new CodeMemberField(declaration, item);
                classType.Members.Add(f);
            }
            ns.Types.Add(classType);

            if (File.Exists(filePath))
                File.Delete(filePath);

            var cCompiler = new CSharpCodeProvider();
            using (var writer = new StreamWriter(filePath))
            {
                cCompiler.GenerateCodeFromCompileUnit(compileUnit, writer,
                new CodeGeneratorOptions() { BracingStyle = "C" });
            }
            AssetDatabase.Refresh();
        }
    }
}
