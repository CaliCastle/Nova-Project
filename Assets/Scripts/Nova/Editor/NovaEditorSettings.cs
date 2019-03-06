using UnityEditor;
using UnityEngine;
using static System.Environment;

namespace Nova.Editor
{
    public sealed class NovaEditorSettings
    {
        public static readonly string NovaPath = $"{CurrentDirectory}/Packages/Nova";

        [MenuItem( "Nova/Print Nova Path" )]
        private static void PrintNovaPath()
        {
            Debug.Log( $"Nova path: {NovaPath}" );
        }
    }
}
