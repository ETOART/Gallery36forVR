using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _subCamera;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _subCanvas;

    public GameObject CurrentCamera { get; set; }

    public void ToSubCamera(Transform target, PaintingData paintingData)
    {
        _subCamera.GetComponent<Camera>().enabled = true;
        _subCamera.GetComponent<Swipe>().enabled = true;
        _mainCamera.GetComponent<Camera>().enabled = false;
        _mainCamera.GetComponent<CameraRayCast>().enabled = false;
        _subCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        _mainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        _subCanvas.SetActive(true);
        _mainCanvas.SetActive(false);
        CurrentCamera = _subCamera;
        _subCanvas.transform.GetChild(1).GetComponent<PaintingAnotation>().SetPaintingAnotation(paintingData);

        _subCamera.GetComponent<CameraTransform>().SetDestination(target);
    }

    public void ToMainCamera()
    {
        _mainCamera.GetComponent<CameraRayCast>().enabled = true;
        _subCamera.GetComponent<Swipe>().enabled = false;
        _subCamera.GetComponent<Camera>().enabled = false;
        _mainCamera.GetComponent<Camera>().enabled = true;
        _subCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        _mainCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        _subCanvas.SetActive(false);
        _mainCanvas.SetActive(true);
        CurrentCamera = _mainCamera;
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentCamera = _mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
