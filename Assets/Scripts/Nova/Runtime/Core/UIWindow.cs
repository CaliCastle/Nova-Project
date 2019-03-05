using UnityEngine;

namespace Nova
{
    public class UIWindow : MonoBehaviour
    {
        [SerializeField]
        private Transform m_view;

        private void Awake()
        {
            if ( m_view == null )
            {
                Debug.LogErrorFormat( "<b>{0}</b> doesn't have `m_view` field assigned.", gameObject.name );
            }
        }
    }
}
