using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace UIKit
{
    [Serializable]
    public sealed class UIViewControllerConfiguration
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public string Identifier = string.Empty;

        /// <summary>
        /// The view title for header to display
        /// </summary>
        public string Title = string.Empty;

        /// <summary>
        /// Whether or not to hide the top and bottom gradient overlay when presenting
        /// </summary>
        public bool HideNavigationOverlays;

        /// <summary>
        /// Whether or not to hide the bottom close button when presenting
        /// </summary>
        public bool HideNavigationCloseButton;

        public bool HideNavigationTitle;

        public bool DestroyOnClose = true;
    }

    [RequireComponent( typeof( CanvasGroup ) )]
    public abstract class UIViewController : MonoBehaviour
    {
        #region Properties

        [Header( "UIViewController" )]
        public UIViewControllerConfiguration Configuration;

        [SerializeField, Range( 0.1f, 1f )]
        private float m_presentationDuration = 0.2f;

        /// <summary>
        /// Indicator for whether or not currently fading
        /// </summary>
        private bool m_isFading;

        private CanvasGroup m_canvasGroup;

        #endregion

        #region Public Methods

        public virtual void ViewWillDisappear()
        {
        }

        /// <summary>
        /// Present another view controller
        /// </summary>
        /// <param name="viewController"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Present( [NotNull] UIViewController viewController,
            float? duration = null, Action onComplete = null )
        {
        }

        /// <summary>
        /// Dismiss self
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public virtual void Dismiss( float? duration = null, Action onComplete = null )
        {
        }

        /// <summary>
        /// Fade the view in
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onComplete">callback handler</param>
        public void FadeIn( float? duration, Action onComplete = null )
        {
            if ( m_canvasGroup != null )
            {
                m_canvasGroup.alpha = 0f;
            }

            FadeTo( 1f, duration ?? m_presentationDuration, onComplete );
        }

        /// <summary>
        /// Fade the view out
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onComplete">callback handler</param>
        public void FadeOut( float? duration, Action onComplete = null )
        {
            if ( m_canvasGroup != null )
            {
                m_canvasGroup.alpha = 1f;
            }

            FadeTo( 0f, duration ?? m_presentationDuration, onComplete );
        }

        /// <summary>
        /// Fade alpha to a certain value
        /// </summary>
        /// <param name="opacity"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public void FadeTo( float opacity, float duration, Action onComplete = null )
        {
            if ( m_isFading || !gameObject.activeInHierarchy ) return;

            StartCoroutine( FadeOpacityTo( opacity, duration, onComplete ) );
        }

        #endregion

        #region MonoBehaviour Methods

        protected void Awake()
        {
            GetCanvasGroup();
            FindNavigationController();

            ViewWillLoad();
        }

        protected void Start()
        {
            ViewDidLoad();
        }

        protected void OnEnable()
        {
            ViewIsEnabled();
        }

        protected void OnDisable()
        {
            ViewIsDisabled();
        }

        protected void OnDestroy()
        {
            ViewWillUnload();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Will be called on Awake
        /// </summary>
        protected virtual void ViewWillLoad()
        {
        }

        protected virtual void ViewDidLoad()
        {
        }

        /// <summary>
        /// Will be called on OnEnable
        /// </summary>
        protected virtual void ViewIsEnabled()
        {
        }

        /// <summary>
        /// Will be called on OnDisable
        /// </summary>
        protected virtual void ViewIsDisabled()
        {
        }

        /// <summary>
        /// Will be called on OnDestroy
        /// </summary>
        protected virtual void ViewWillUnload()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create a canvas group if there is none
        /// </summary>
        private void GetCanvasGroup()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
        }

        private void FindNavigationController()
        {
        }

        /// <summary>
        /// Coroutine: Fade alpha to a certain value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        private IEnumerator FadeOpacityTo( float value, float duration, [CanBeNull] Action onComplete )
        {
            m_isFading = true;

            float t = 0f;
            float alpha = m_canvasGroup.alpha;
            float startAlpha = alpha;
            float deltaAlpha = value - alpha;

            while ( t <= 1 )
            {
                t = Time.deltaTime / duration;
                m_canvasGroup.alpha = startAlpha + t * deltaAlpha;

                yield return null;
            }

            m_canvasGroup.alpha = value;

            m_isFading = false;

            if ( onComplete != null ) onComplete();
        }

        #endregion
    }
}
