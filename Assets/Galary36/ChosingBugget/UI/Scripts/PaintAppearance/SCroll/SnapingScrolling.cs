using System.Collections.Generic;
using UnityEngine;

public class SnapingScrolling : MonoBehaviour, IScrol
{
    [SerializeField] private float snapSpeed;
    

    public List<PanelData> panelData{ get; set;}
    private bool scrollWork = false;

    private int  selectPanelID;
    private int  oldSelectPanelID;
    private RectTransform contentRect;
    private Vector2 contentVector;
    private float step = 0f;

    public delegate void ChooseAppearance(PaintingData paintingData);
    public event ChooseAppearance chooseAppearance;

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
        

    }
    private void FixedUpdate(){
        if(scrollWork){
            SnapMove();
        }
    }
    private int FoundNearestPanel(){
        print(panelData.Count);
        float nearestPosition = float.MaxValue;
        int nearestID = 0;
        int i = 0;
        foreach(PanelData element in panelData){
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
        print(step);
        contentVector.x = Mathf.SmoothStep(contentVector.x, panelData[selectPanelID]._position.x, step);
        contentRect.anchoredPosition = contentVector;
        if(step>=1) StopMove();
    }   
    private void SetApperance(int ID){
        if(oldSelectPanelID != ID){
            chooseAppearance?.Invoke(panelData[ID].paintingData);
            oldSelectPanelID = ID;
        } 
    } 

}
