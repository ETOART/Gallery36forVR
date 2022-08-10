using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStart : MonoBehaviour
{

    [SerializeField] Transform start;
    [SerializeField] Transform finish;
    [SerializeField] private float speed=2;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(finish.transform.rotation.eulerAngles - new Vector3(0, 0, 0)), 200 * Time.deltaTime);
       // float scale = finish.localScale.;
        transform.position = Vector3.MoveTowards(transform.position, finish.transform.position + finish.TransformDirection(0f, 0f, -1 ), step);

    }
}
