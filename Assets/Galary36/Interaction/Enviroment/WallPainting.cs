using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallPainting : MonoBehaviour
{
    [SerializeField] private GameObject enviroment;
    [SerializeField] private GameObject xrOrigin;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainCanvas;
    public void Open()
    {
 

        ChosenPainting chosenPainting = FindObjectOfType<ChosenPainting>();
        enviroment.SetActive(false);
        canvas.SetActive(true);
        

        xrOrigin.GetComponent<CameraManager>().ToMainCamera();
        mainCanvas.SetActive(false);
        Debug.Log(chosenPainting.Painting.GetComponent<BaggueteSpawner>().baguet);
   
        
        }
    public void Close()
    {
        ChosenPainting chosenPainting = FindObjectOfType<ChosenPainting>();
        enviroment.SetActive(true);
        canvas.SetActive(false);
        xrOrigin.GetComponent<CameraManager>().ToSubCamera(chosenPainting.Painting.transform, chosenPainting.Painting.GetComponent<Painting>()._paintingData);
   
    }


}
