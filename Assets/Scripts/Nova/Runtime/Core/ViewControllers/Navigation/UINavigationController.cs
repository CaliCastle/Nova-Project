using System;
using UnityEngine;
using System.Collections.Generic;

namespace Nova
{
    public abstract class UINavigationController : UIViewController
    {
        #region Properties

        public Stack<UIViewController> ViewControllers { get; private set; }

        /// <summary>
        /// The initial view controller to be present on
        /// </summary>
        [SerializeField]
        private UIViewController m_initialViewController;

        #endregion Properties

        #region Public

        public void Push<T>( Action<T> preparation = null, Action onComplete = null ) where T : UIViewController
        {
//            ViewControllers.Push(  );
        }

        public UIViewController Pop()
        {
            return ViewControllers.Pop();
        }

        #endregion Public

        #region UIViewController

        protected override void ViewWillLoad()
        {
            base.ViewWillLoad();

            if ( m_initialViewController == null )
            {
                Debug.LogError( $"<b>{GetType()}</b> doesn't have <i>`m_initialViewController`</i> field assigned." );
            }
        }

        protected override void ViewDidLoad()
        {
            base.ViewDidLoad();

            PresentInitialViewController();
        }

        #endregion UIViewController

        #region Private

        private void PresentInitialViewController()
        {
            if ( m_initialViewController == null )
            {
                return;
            }
        }

        #endregion Private
    }
}
