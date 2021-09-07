using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class JenkinsAdapter
{
    [MenuItem("Jenkins/JenkinsBuild")]
    public static void Build()
    {
        Debug.Log("HOMEDRIVE = " + System.Environment.GetEnvironmentVariable("HOMEDRIVE"));
        Debug.Log("HOMEPATH = " + System.Environment.GetEnvironmentVariable("HOMEPATH"));

        EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup: BuildTargetGroup.Android, target: BuildTarget.Android);

        List<string> sceneList = new List<string>();
        EditorBuildSettingsScene[] temp = EditorBuildSettings.scenes;
        for (int i = 0, iMax = temp.Length; i < iMax; ++i)
            sceneList.Add(temp[i].path);
        //DateTime date3 = DateTime.Now;
        //string timeInfo = date3.ToString("yyyy.MM.dd_HH.mm.ss");

        //BuildPipeline.BuildPlayer(sceneList.ToArray(), "E:/MiniGameBuildOutputs/CatClub_" + timeInfo + ".apk", BuildTarget.Android, BuildOptions.None);
        BuildPipeline.BuildPlayer(sceneList.ToArray(), "E:/MiniGameBuildOutputs/CatClub.apk", BuildTarget.Android, BuildOptions.None);
    }
}