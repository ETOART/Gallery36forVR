using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private float scaleoffset;
    [SerializeField] private Slider slider;
    private Transform _target;
    private float speed = 16f;
    private bool isInit = false;
    void Start()
    {

    }

    public void SetDestination(Transform targer)
    {
        transform.position = _cameraManager.CurrentCamera.transform.position;
        _target = targer;
        isInit = true;
    }


    void Update()
    {
        scaleoffset = slider.value;
        if (isInit)
        {
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_target.transform.rotation.eulerAngles-new Vector3(0, 0, 0)), 200 * Time.deltaTime);
            float scale = _target.localScale.x * 10 * scaleoffset;
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position + _target.TransformDirection(0f, 0f, -1*scale), step);
        }
    }
}