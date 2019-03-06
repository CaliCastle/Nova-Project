using System;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Nova
{
    public interface INovaLaunchable
    {
        /// <summary>
        /// Entry point for when the UI is configured and prepared.
        /// </summary>
        /// <param name="window">The key window</param>
        void LiftOff( UIWindow window );
    }

    public class UIWindow : MonoBehaviour
    {
        #region Properties

        public UIView View => m_view;

        [SerializeField]
        private UIView m_view;

        [SerializeField]
        private bool m_shouldResetView = true;

        [SerializeField]
        private List<UIViewController> m_viewControllerPrefabPool;

        /// <summary>
        /// View controller references for resetting only.
        /// </summary>
        private readonly List<UIViewController> m_viewControllers = new List<UIViewController>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preparation"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [CanBeNull]
        public T Present<T>( Action<T> preparation = null, Action onComplete = null ) where T : UIViewController
        {
            T prefab = m_viewControllerPrefabPool.Find( vc => vc is T ) as T;

            if ( prefab == null )
            {
                return null;
            }

            T controller = Instantiate( prefab, m_view.transform );
            controller.ResetBounds();
            controller.transform.SetAsLastSibling();

            preparation?.Invoke( controller );

            return controller;
        }

        public void Inject( [NotNull] UIView view )
        {
            m_view = view;
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if ( m_view == null )
            {
                Debug.LogErrorFormat( "<b>{0}</b> doesn't have `m_view` field assigned.", gameObject.name );
                return;
            }

            ResetView();
            Launch();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Destroy any
        /// </summary>
        private void ResetView()
        {
            if ( !m_shouldResetView )
            {
                return;
            }

            m_view.GetComponentsInChildren( true, m_viewControllers );
            foreach ( UIViewController controller in m_viewControllers )
            {
                if ( controller )
                {
                    Destroy( controller.gameObject );
                }
            }
        }

        /// <summary>
        /// Once everything is ready, launch the NovaLaunchable implemented script.
        /// </summary>
        private void Launch()
        {
            INovaLaunchable nova = gameObject.GetComponent<INovaLaunchable>();
            nova?.LiftOff( this );
        }

        #endregion
    }
}
