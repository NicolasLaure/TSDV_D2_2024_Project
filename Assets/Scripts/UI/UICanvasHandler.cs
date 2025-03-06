using Events;
using UnityEngine;

namespace UI
{
    public class UICanvasHandler : MonoBehaviour
    {
        [SerializeField] private GameObjectEventChannel onNewSelectedObjectEvent;
        [SerializeField] private GameObject enabledPreselectedGameObject;
        
        private void OnEnable()
        {
            if (enabledPreselectedGameObject != null)
            {
                onNewSelectedObjectEvent?.RaiseEvent(enabledPreselectedGameObject);
            }
        }
    }
}
