using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallRaycast : MonoBehaviour
{

    [SerializeField] public CameraManager _cameraManager;
    private ChosenPainting chosenPainting;
    // Start is called before the first frame update
    void Start()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        chosenPainting = FindObjectOfType<ChosenPainting>();
        Swipe swipe = FindObjectOfType<Swipe>();
        swipe.rightSwipeEvent.AddListener(OnRightSwipe);
        swipe.leftSwipeEvent.AddListener(OnLeftSwipe);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onClick(Transform painting, PaintingData paintingData)
    {
        _cameraManager.ToSubCamera(painting, paintingData);
        chosenPainting.Painting = painting.gameObject;
    }
    public void OnLeftSwipe()
    {
        Debug.Log(chosenPainting.Painting.transform.parent.name);
        if (chosenPainting.Painting.transform.parent.gameObject == gameObject)
        {
            Debug.Log("right right");
            chosenPainting.Painting = chosenPainting.Painting.GetComponent<Painting>().PrevPainting;
            Debug.Log(chosenPainting.Painting);
            _cameraManager.ToSubCamera(chosenPainting.Painting.transform, chosenPainting.Painting.GetComponent<Painting>()._paintingData);
        }
    }
    public void OnRightSwipe()
    {
        Debug.Log(chosenPainting.Painting.transform.parent.name);
        if (chosenPainting.Painting.transform.parent.gameObject == gameObject)
        {
            Debug.Log("right right");
            chosenPainting.Painting = chosenPainting.Painting.GetComponent<Painting>().NextPainting;
            Debug.Log(chosenPainting.Painting);
            _cameraManager.ToSubCamera(chosenPainting.Painting.transform, chosenPainting.Painting.GetComponent<Painting>()._paintingData);
        }
    }
}
