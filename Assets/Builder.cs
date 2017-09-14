using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

class CIBuilder{
    static string[] scenes = FindEnabledEditorScenes();

    static string appname = "YourProject";
    static string targetPath = "target";

    private static string[] FindEnabledEditorScenes() {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void PerformPCBuild() {
        string target = targetPath;
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.targetGroup = BuildTargetGroup.Standalone;
        options.target = BuildTarget.StandaloneWindows;
        options.locationPathName = targetPath + "/" + appname;
        options.options = BuildOptions.None;
        Build(options);
    }


    static void Build(BuildPlayerOptions options) {
        EditorUserBuildSettings.SwitchActiveBuildTarget(options.targetGroup, options.target);
        string res = BuildPipeline.BuildPlayer(options);
        if (res.Length > 0) {
            throw new Exception("BuildPlayer failure: " + res);
        }
    }
}
