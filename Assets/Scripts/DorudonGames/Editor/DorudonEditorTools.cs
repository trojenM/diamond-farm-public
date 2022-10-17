using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;

namespace DorudonGames.Editor
{
    public static class DorudonEditorTools
    {
        [MenuItem("Tools/Dorudon/Setup/Create Folders")]
        public static void CreateFolders()
        {
            
            CreateDir("Prefabs","Scripts","Materials", "Models", "Animations", "Particles", "Sprites/Textures", "Fonts");
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Dorudon/Setup/Install URP")]
        private static void InstallUniversalRenderPipeline()
        {
            Client.Add("com.unity.render-pipelines.universal");
            CreateDir("Urp");
        }

        private static void CreateDir(params string []dirName)
        {
            foreach (var d in dirName)
            {
                Directory.CreateDirectory(Path.Combine(Application.dataPath, d));
            }
            
        }
    }
}
