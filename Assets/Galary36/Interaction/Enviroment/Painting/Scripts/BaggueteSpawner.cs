using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaggueteSpawner : MonoBehaviour
{
    [SerializeField] public GameObject Corner;
    [SerializeField] public GameObject Frame;
    [SerializeField] private GameObject Painting;
    [SerializeField] private GameObject FrameTransform;
    [SerializeField] public GameObject baguet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void SetCorner()
    {
        GameObject obj = Instantiate(Corner, FrameTransform.transform);
        Vector3 halfScale = Painting.transform.localScale / 2;
       // Debug.Log(halfScale);
        obj.transform.localPosition = new Vector3(halfScale.x, halfScale.y, 0);
        obj.transform.localRotation = Quaternion.Euler(0, -90, 90);
       // Debug.Log(obj.transform.position);
        obj = Instantiate(Corner, FrameTransform.transform);
        obj.transform.localPosition = new Vector3(-halfScale.x, -halfScale.y, 0);
        obj.transform.localRotation = Quaternion.Euler(0, 90, -90);
        obj = Instantiate(Corner, FrameTransform.transform);
        obj.transform.localPosition = new Vector3(-halfScale.x, halfScale.y, 0);
        obj.transform.localRotation = Quaternion.Euler(90, 180, 0);
        obj = Instantiate(Corner, FrameTransform.transform);
        obj.transform.localPosition = new Vector3(halfScale.x, -halfScale.y, 0);
        obj.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        //SetFrame();
    }

    public void SetFrame()
    {

        SetFrameX(1);
        SetFrameX(-1);

        SetFrameY(1);
        SetFrameY(-1);
        SetCorner();
    }
    private void SetFrameX(int v)
    {
        Vector3 halfScale = Painting.transform.localScale / 2;
        float offset = 0.065f;
        Quaternion rotation = Quaternion.Euler(-180f * v, 90f * v, -90f * v);
        Vector3 position = new Vector3(-halfScale.x * v, (halfScale.y - offset), 0);

        CreateFramePice(position, rotation);
        if (Painting.transform.localScale.y > offset * 2)
        {
            position.y *= -1;
            CreateFramePice(position, rotation);
            float len = offset * 4;
            while (Painting.transform.localScale.y > len)
            {
                len += offset*2;
                position.y += offset*2;
                CreateFramePice(position, rotation);
            }
        }

    }

    private void SetFrameY(int v)
    {
        Vector3 halfScale = Painting.transform.localScale / 2;
        float offset = 0.065f;
        
        Vector3 position = new Vector3(-1 * (halfScale.x - offset), halfScale.y * v, 0);
        Quaternion rotation;
        if (v == 1)
             rotation = Quaternion.Euler(-90, 0f, 0);
        else
             rotation = Quaternion.Euler(90, 0f, 180);

        CreateFramePice(position, rotation);

        if (Painting.transform.localScale.x > offset * 2)
        {
            position.x *= -1;
            CreateFramePice(position, rotation);
            float len = offset * 4;
            while(Painting.transform.localScale.x > len)
            {
                len += offset*2;
                position.x -= offset*2;
                CreateFramePice(position, rotation);
            }
        }
        
    }
    private void CreateFramePice(Vector3 position,Quaternion rotation)
        {
            GameObject obj = Instantiate(Frame, FrameTransform.transform);
            obj.transform.localPosition = position;
            obj.transform.localRotation = rotation;
        }
    public void ChangeBagguete(GameObject bagguet)
    {
        baguet = bagguet;
        foreach (Transform child in FrameTransform.transform)
            Destroy(child.gameObject);
        
        Corner = bagguet.transform.GetChild(0).gameObject;
        Frame = bagguet.transform.GetChild(1).gameObject;
        SetFrame();
    }

    public void ChangeBaggueteFrom()
    {
       
        foreach (Transform child in FrameTransform.transform)
            Destroy(child.gameObject);

        Corner = baguet.transform.GetChild(0).gameObject;
        Frame = baguet.transform.GetChild(1).gameObject;
        SetFrame();
    }
}
