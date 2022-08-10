using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KilzhinskyManagment : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private GameObject _room;
    private bool state = false;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        state = !state;
        _plane.SetActive(state);
        _room.SetActive(!state);
    }
}