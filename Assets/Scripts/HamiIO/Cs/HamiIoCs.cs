using System.IO;
using HamiIO.Cs;
using UnityEditor;
using UnityEngine;

namespace HamiIO
{
    public class HamiIoCs
    {
        public static void WriteModel(string folderPath,
            string fileName,
            PropertiesModel[] properties,
            string nameSpace = null,
            string parent = null
        )
        {
            FileChecker.HasCs(folderPath + @"\Model", fileName, true);
            File.WriteAllText($@"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\Model\{fileName}.cs",
                GenerateBody(fileName, nameSpace, properties, null, parent: parent));
            MonoBehaviour.print(
                $@"CS has been created in {CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\Model\{fileName}");
        }


        public static void GenerateClass(
            string folderPath,
            string fileName,
            string parent = null,
            string nameSpace = null,
            PropertiesModel[] properties = null,
            MethodsModel[] method = null
        )

        {
            FileChecker.HasCs(folderPath, fileName, true);
            File.WriteAllText($@"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\{fileName}.cs",
                GenerateBody(fileName, nameSpace, properties, method, parent));
            MonoBehaviour.print(
                $@"CS has been created in {CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\{fileName}");
        }

        public static bool Has(string folderPath, string fileName) =>
            FileChecker.HasCs(folderPath, fileName);

        private static string GenerateBody(
            string fileName,
            string nameSpace,
            PropertiesModel[] properties,
            MethodsModel[] methods,
            string parent = ""
        )
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(nameSpace)) content += $"namespace {nameSpace}{{ \n";

            if (string.IsNullOrEmpty(parent))
                AddTxt(ref content, $"public class {fileName}{{", 2);
            else AddTxt(ref content, $"public class {fileName} : {parent}{{", 2);

            if (properties != null)
                foreach (PropertiesModel modelPropertiese in properties)
                {
                    AddTxt(ref content, modelPropertiese.Attribute, 1, 1, true);
                    AddTxt(ref content, "public", 0, 1);
                    if (modelPropertiese.IsStatic) AddTxt(ref content, "static");
                    if (modelPropertiese.ReadOnly) AddTxt(ref content, "readonly");
                    AddTxt(ref content, modelPropertiese.Type);
                    AddTxt(ref content, modelPropertiese.Name);
                    if (modelPropertiese.AutoProperty)
                        AddTxt(ref content, "{ get; set; }", 2);
                    if (modelPropertiese.Value != null)
                    {
                        switch (modelPropertiese.Type)
                        {
                            case "string":
                                AddTxt(ref content, $"=$\"{modelPropertiese.Value}\";", 2);
                                break;
                            case "float":
                                AddTxt(ref content, $"={modelPropertiese.Value}f;", 2);
                                break;
                            case "double":
                                AddTxt(ref content, $"={modelPropertiese.Value}d;", 2);
                                break;
                            default:
                                AddTxt(ref content, $"={modelPropertiese.Value};", 2);
                                break;
                        }
                    }
                    else if (!modelPropertiese.AutoProperty)
                        AddTxt(ref content, ";", 2);
                }

            if (methods != null)
                foreach (MethodsModel method in methods)
                {
                    if (method.Attribute != null)
                        foreach (var s in method.Attribute)
                            AddTxt(ref content, s, 1, 0, true);


                    AddTxt(ref content, method.AccessModifier.ToString().ToLower(), 0, 1);
                    if (method.IsStatic) AddTxt(ref content, "static");
                    else
                    {
                        if (method.IsVirtual) AddTxt(ref content, "virtual");
                        else if (method.IsOverride) AddTxt(ref content, "override");
                    }

                    AddTxt(ref content, method.ReturnType);
                    AddTxt(ref content, method.Name);
                    AddTxt(ref content, "(");
                    if (method.Arguments != null)
                        foreach (ArgumentModel methodArgument in method.Arguments)
                        {
                            if (methodArgument.IsRefs) AddTxt(ref content, "refs");
                            if (methodArgument.IsOut) AddTxt(ref content, "out");
                            if (methodArgument.IsParams) AddTxt(ref content, "params");
                            AddTxt(ref content, methodArgument.Type);
                            AddTxt(ref content, methodArgument.Name);
                        }

                    AddTxt(ref content, ")", 1);
                    AddTxt(ref content, "{", 1, 1);
                    if (string.IsNullOrEmpty(method.Body))
                        AddTxt(ref content, "throw new System.NotImplementedException();", 1, 2);
                    else
                        AddTxt(ref content, method.Body, 1);
                    AddTxt(ref content, "}", 1, 1);
                }

            content += string.IsNullOrEmpty(nameSpace) ? "}" : "}\n}";
            return content;
        }

        private static void AddTxt(ref string content, string toAdd, byte breakAfter = 0, byte tabIndentBefore = 0,
            bool isAttribute = false)
        {
            if (string.IsNullOrEmpty(toAdd)) return;
            for (int i = 0; i < tabIndentBefore; i++) content += "\t";
            content += isAttribute ? $"[{toAdd}]" : toAdd + " ";
            for (int i = 0; i < breakAfter; i++) content += "\n";
        }
    }
}