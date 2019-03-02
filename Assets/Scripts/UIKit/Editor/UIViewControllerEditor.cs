using UnityEditor;
using UnityEngine;
using JetBrains.Annotations;

namespace UIKit
{
    [CustomEditor( typeof( UIViewController ), true )]
    public sealed class UIViewControllerEditor : Editor
    {
        [NotNull]
        private UIViewController m_viewController
        {
            get { return ( UIViewController ) target; }
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            if ( GUILayout.Button( "Generate ID", new GUIStyle( GUI.skin.button )
                {normal = {textColor = new Color( 0.85f, 0.92f, 0.73f )}, fixedHeight = 25f} ) )
            {
                if ( string.IsNullOrEmpty( m_viewController.Configuration.Identifier ) )
                {
                    m_viewController.Configuration.Identifier =
                        m_viewController.GetType().Name.Replace( "ViewController", "" ).ToLower();
                }
            }

            if ( GUILayout.Button( "Reset Bounds",
                new GUIStyle( GUI.skin.button )
                    {normal = {textColor = new Color( 0.92f, 0.54f, 0.4f )}, fixedHeight = 25f} ) )
            {
                RectTransform transform = m_viewController.transform as RectTransform;
                if ( transform )
                {
                    transform.anchorMin = Vector2.zero;
                    transform.anchorMax = Vector2.one;
                    transform.offsetMin = Vector2.zero;
                    transform.offsetMax = Vector2.zero;
                }
            }

            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }
}
