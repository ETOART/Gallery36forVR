using UnityEngine;
using UnityEngine.EventSystems;

public class PaintRayCaster : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private GameObject _camera;
    //[SerializeField] private GameObject maincamera;
    //Detect if a click occurs
    private WallRaycast wall;
    void Start()
    {
        wall =  gameObject.GetComponentInParent<WallRaycast>();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {

        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        // Debug.Log(name + " Game Object Clicked!");
        // bool flag = gameObject.GetComponent<Animator>().GetBool("Expanded");
        // flag = !flag;
        // gameObject.GetComponent<Animator>().SetBool("Expanded", flag);
        Debug.Log(wall);
        wall.onClick(gameObject.transform, gameObject.GetComponent<Painting>()._paintingData);
        Debug.Log("Click");

    }
}
