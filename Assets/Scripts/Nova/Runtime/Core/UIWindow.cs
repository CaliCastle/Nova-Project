using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField]
        private UIView m_view;

        [SerializeField]
        private bool m_shouldResetView = true;

        /// <summary>
        /// View controller references for resetting only.
        /// </summary>
        private readonly List<UIViewController> m_viewControllers = new List<UIViewController>();

        public void AssignView( UIView view )
        {
            if ( m_view == null && view != null )
            {
                m_view = view;
            }
        }

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
    }
}
