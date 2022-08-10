using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaguetteSnapScrolling : MonoBehaviour, IScrol
{
    [SerializeField] private float snapSpeed;
    public List<BaguetteData> baguetteDatas{ get; set;}

    private bool scrollWork = false;
    private int  selectPanelID;
    private int  oldSelectPanelID;
    private RectTransform contentRect;
    private Vector2 contentVector;
    private float step = 0f;

    [SerializeField] public BaggueteChange ChooseBaguetteEvent;
    public ChosenPainting chosenPainting;
    public void StartMove(int index){
        
        if(index == -1){
            selectPanelID = FoundNearestPanel();
        }
        else{
            selectPanelID = index;            
        }
        scrollWork = true;
        SetApperance(selectPanelID);
    }
    private void StopMove(){
        scrollWork = false;
        step = 0f;
    }


    private void Awake(){
        contentRect = GetComponent<RectTransform>();
        chosenPainting = FindObjectOfType<ChosenPainting>();
    }
    private void FixedUpdate(){
        if(scrollWork){
            SnapMove();
        }
    }
    private int FoundNearestPanel(){
        float nearestPosition = float.MaxValue;
        int nearestID = 0;
        int i = 0;
        foreach(BaguetteData element in baguetteDatas){
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - element._position.x);
            if(nearestPosition > distance){
                nearestPosition = distance;
                nearestID = i;
            }
            i++;
        }
        return nearestID;
    }
    
    
    private void SnapMove(){
        contentVector = contentRect.anchoredPosition;
        step+=snapSpeed;
        
        contentVector.x = Mathf.SmoothStep(contentVector.x, baguetteDatas[selectPanelID]._position.x, step);
        contentRect.anchoredPosition = contentVector;
        if(step>=1) StopMove();
    }   
    private void SetApperance(int ID){
        if(oldSelectPanelID != ID){
            ChooseBaguetteEvent?.Invoke(baguetteDatas[ID]);
            Debug.Log("Change");
          //  BaggueteSpawner painting = chosenPainting.Painting.GetComponent<BaggueteSpawner>();
            //painting.ChangeBagguete(baguetteDatas[ID].objectPrefab);
            oldSelectPanelID = ID;
        } 
    }
}
[System.Serializable]
public class BaggueteChange: UnityEvent<BaguetteData> { }