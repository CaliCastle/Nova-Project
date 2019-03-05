using System.Collections.Generic;
using UnityEngine;

namespace Nova
{
    public class UINavigationController : UIViewController
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
