using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class CIBuilder{
    static string[] scenes = FindEnabledEditorScenes();

    static string appname = "YourProject";
    static string targetPath = "AutoBuild";

    private static string[] FindEnabledEditorScenes() {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void PerformPCBuild() {
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.targetGroup = BuildTargetGroup.Standalone;
        options.target = BuildTarget.StandaloneWindows;
        options.locationPathName = targetPath + "/" + appname + ".exe";
        options.options = BuildOptions.None;
        Build(options);
    }

    static void ExportToAndroidStudio() {
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.targetGroup = BuildTargetGroup.Android;
        options.target = BuildTarget.Android;
        options.options = BuildOptions.AcceptExternalModificationsToPlayer;
        options.locationPathName = targetPath + "/" + appname;
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
