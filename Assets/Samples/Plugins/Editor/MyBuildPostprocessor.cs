using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class MyBuildPostprocessor
{
    [PostProcessBuildAttribute(1)]

    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        string path = Path.GetDirectoryName(pathToBuiltProject) + "\\data";
        if (Directory.Exists(path)) Directory.Delete(path, true);
        FileUtil.CopyFileOrDirectory("data", path);
    }
}
