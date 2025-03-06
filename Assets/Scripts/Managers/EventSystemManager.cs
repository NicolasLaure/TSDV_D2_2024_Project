using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Managers
{
    [RequireComponent(typeof(EventSystem))]
    public class EventSystemManager : MonoBehaviour
    {
        [SerializeField] private GameObjectEventChannel onNewPreselectedObjectEvent;
        //[SerializeField] private UI_Input uiInput;

        private InputSystemUIInputModule _uiInput;
        private EventSystem _eventSystem;
        private GameObject _lastPreselectedObject;

        private void OnEnable()
        {
            _eventSystem ??= GetComponent<EventSystem>();
            _uiInput ??= GetComponent<InputSystemUIInputModule>();

            _uiInput.move.action.performed += HandlePreselectFirstObject;
            //  uiInput.UI.Navigate.performed += HandlePreselectFirstObject;
            if (onNewPreselectedObjectEvent != null) onNewPreselectedObjectEvent.onGameObjectEvent += HandleNewPreselectedObject;
        }

        private void OnDisable()
        {
            _uiInput.move.action.performed -= HandlePreselectFirstObject;
            //uiInput.UI.Navigate.performed -= HandlePreselectFirstObject;
            if (onNewPreselectedObjectEvent != null) onNewPreselectedObjectEvent.onGameObjectEvent -= HandleNewPreselectedObject;
        }

        private void HandlePreselectFirstObject(InputAction.CallbackContext context)
        {
            if (_eventSystem.currentSelectedGameObject == null)
            {
                _eventSystem.SetSelectedGameObject(_lastPreselectedObject);
            }
        }

        private void HandleNewPreselectedObject(GameObject newObject)
        {
            _eventSystem.SetSelectedGameObject(newObject);
            _lastPreselectedObject = newObject;
        }
    }
}