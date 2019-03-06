using UnityEngine;
using System.Collections.Generic;

namespace Nova
{
    public abstract class UINavigationController : UIViewController
    {
        #region Properties

        public Stack<UIViewController> ViewControllers
        {
            get;
            private set;
        }

        /// <summary>
        /// The initial view controller to be present on
        /// </summary>
        [SerializeField]
        private UIViewController m_initialViewController;

        #endregion
        
        
    }
}
