using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRayCast : MonoBehaviour
{
    [SerializeField] GameObject Marker;
    [SerializeField] GameObject xrOrigin;
    Vector3 worldPosition;
    Ray ray;
    private int fingerID = -1;
    private void Awake()
    {
#if !UNITY_EDITOR
     fingerID = 0; 
#endif
    }
    void Update()
    {

        for (var i = 0; i < Input.touchCount; ++i)
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hitData;
                if (Physics.Raycast(ray, out hitData))
                {
                    Debug.Log(hitData.transform.name);
                    if (hitData.transform.gameObject.tag == "Floor")
                    {
                        Debug.DrawRay(transform.position, hitData.point);
                        worldPosition = hitData.point;
                        GameObject obj = Instantiate(Marker, hitData.point, Quaternion.identity);
                        StartCoroutine(TeleportRoutine(hitData.point, obj));

                        Debug.Log(worldPosition);
                    }
                }
            }
    }
    IEnumerator TeleportRoutine(Vector3 pos, GameObject obj)
    {

        yield return new WaitForSeconds(0.5f);
        Vector3 delta = gameObject.transform.localPosition + gameObject.transform.parent.transform.localPosition;
        delta.y = pos.y;
        xrOrigin.transform.position = pos - delta;
        Destroy(obj);
    }
}