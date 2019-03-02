using System.Collections.Generic;
using UnityEngine;

namespace UIKit
{
    public class UINavigationController : MonoBehaviour
    {
        #region Properties

        public Stack<UIViewController> ViewControllers
        {
            get;
            private set;
        }

        /// <summary>
        /// The root canvas view for navigation controller
        /// </summary>
        [SerializeField]
        private RectTransform m_view;

        /// <summary>
        /// The initial view controller to be present on
        /// </summary>
        [SerializeField]
        private UIViewController m_initialViewController;

        #endregion
    }
}
