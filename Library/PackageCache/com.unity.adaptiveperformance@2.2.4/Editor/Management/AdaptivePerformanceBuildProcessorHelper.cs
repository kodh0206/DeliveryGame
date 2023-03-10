using System;
using System.Linq;

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

using UnityEngine;

namespace UnityEditor.AdaptivePerformance.Editor
{
    /// <summary>
    /// Base abstract class that provides some common functionality for providers seeking to integrate with management assisted build.
    /// </summary>
    /// <typeparam name="T">The type parameter that will be used as the base type of the settings.</typeparam>
    public abstract class AdaptivePerformanceBuildHelper<T> : IPreprocessBuildWithReport, IPostprocessBuildWithReport where T : UnityEngine.Object
    {
        /// <summary>Override of base IPreprocessBuildWithReport.</summary>
        /// <returns>The callback order.</returns>
        public virtual int callbackOrder { get { return 0; } }

        /// <summary>Override of base IPreprocessBuildWithReport.</summary>
        /// <returns>String that specifies the key to be used to set or get settings in EditorBuildSettings.</returns>
        public abstract string BuildSettingsKey { get; }

        /// <summary>Helper function to return current settings for a specific build target.</summary>
        ///
        /// <param name="buildTargetGroup">An enum that specifies which platform group this build is for.</param>
        /// <returns>A Unity object that represents the settings instance data for that build target, or null if not found.</returns>
        public virtual UnityEngine.Object SettingsForBuildTargetGroup(BuildTargetGroup buildTargetGroup)
        {
            UnityEngine.Object settingsObj = null;
            EditorBuildSettings.TryGetConfigObject(BuildSettingsKey, out settingsObj);
            if (settingsObj == null || !(settingsObj is T))
                return null;

            return settingsObj;
        }

        void CleanOldSettings()
        {
            BuildHelpers.CleanOldSettings<T>();
        }

        void SetSettingsForRuntime(UnityEngine.Object settingsObj)
        {
            // Always remember to clean up preloaded assets after build to make sure we don't
            // dirty later builds with assets that may not be needed or are out of date.
            CleanOldSettings();

            if (settingsObj == null)
                return;

            if (!(settingsObj is T))
            {
                Type typeOfT = typeof(T);
                Debug.LogErrorFormat("Settings object is not of type {0}. No settings will be copied to runtime.", typeOfT.Name);
                return;
            }

            UnityEngine.Object[] preloadedAssets = PlayerSettings.GetPreloadedAssets();

            if (!preloadedAssets.Contains(settingsObj))
            {
                var assets = preloadedAssets.ToList();
                assets.Add(settingsObj);
                PlayerSettings.SetPreloadedAssets(assets.ToArray());
            }
        }

        /// <summary>
        /// Override of base IPreprocessBuildWithReport.
        /// </summary>
        /// <param name="report">BuildReport instance passed in from the build pipeline.</param>
        public virtual void OnPreprocessBuild(BuildReport report)
        {
            SetSettingsForRuntime(SettingsForBuildTargetGroup(report.summary.platformGroup));
        }

        /// <summary>
        /// Override of base IPostprocessBuildWithReport.
        /// </summary>
        /// <param name="report">BuildReport instance passed in from build pipeline.</param>
        public virtual void OnPostprocessBuild(BuildReport report)
        {
            // Always remember to clean up preloaded assets after build to make sure we don't
            // dirty later builds with assets that may not be needed or are out of date.
            CleanOldSettings();
        }
    }
}
