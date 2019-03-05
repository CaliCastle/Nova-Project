using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Nova.Editor
{
    public class NovaMenuItems
    {
        [MenuItem( "GameObject/Nova/UIWindow", false, 10 )]
        private static void CreateUIWindow( MenuCommand menuCommand )
        {
            GameObject window = CreateGameObject( menuCommand, "UIWindow", typeof( UIWindow ) );
            GameObject view = MakeUIView( menuCommand, "Main View" );
            view.transform.SetParent( window.transform );
            window.GetComponent<UIWindow>().AssignView( view.GetComponent<UIView>() );
        }

        [MenuItem( "GameObject/Nova/UIView", false, 10 )]
        private static void CreateUIView( MenuCommand menuCommand )
        {
            MakeUIView( menuCommand );
        }

        private static GameObject MakeUIView( MenuCommand menuCommand, string name = "UIView" )
        {
            GameObject gameObject = CreateGameObject( menuCommand, name, typeof( Canvas ), typeof( CanvasScaler ),
                typeof( CanvasGroup ), typeof( UIView ) );

            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            return gameObject;
        }

        private static GameObject CreateGameObject( MenuCommand menuCommand, string name, params Type[] types )
        {
            // create a custom view controller
            GameObject gameObject = new GameObject( name, types );
            // ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign( gameObject, menuCommand.context as GameObject );
            // register the creation in the undo system
            Undo.RegisterCreatedObjectUndo( gameObject, "Create " + gameObject.name );
            Selection.activeObject = gameObject;

            return gameObject;
        }
    }
}
