using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    private Dictionary<GameObject, bool> _buttons = new Dictionary<GameObject, bool>();
    [SerializeField] public  UnityEvent<bool> InteractableStateEvent;
    void Start()
    {
        foreach (Transform button in gameObject.transform)
        {
            _buttons.Add(button.gameObject, false);
        }
    }

    public void OffAllButtons()
    {

        List<GameObject> keys = new List<GameObject>(_buttons.Keys);
        foreach (GameObject button in keys)
        {

            _buttons[button] = false;

            SetButtonColor(button);
        }
        InteractableStateEvent.Invoke(false);
    }

    public void Click()
    {
        GameObject currentbutton = EventSystem.current.currentSelectedGameObject;

        bool state = _buttons[currentbutton];
        _buttons[currentbutton] = !state;

        List<GameObject> keys = new List<GameObject>(_buttons.Keys);
        foreach (GameObject button in keys)
        {
            if (button != currentbutton)
            {
                _buttons[button] = false;
            }
            SetButtonColor(button);
        }

        InteractableStateEvent.Invoke(_buttons.Any((s) => s.Value == true));
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.active);
        Debug.Log(gameObject.active);
        OffAllButtons();
    }

    private void SetButtonColor(GameObject button)
    {
        button.GetComponent<Image>().color = _buttons[button] ? new Color(1, 0, 0) : new Color(0, 0, 0);
    }
}