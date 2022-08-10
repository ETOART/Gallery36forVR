using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanelBaguette : MonoBehaviour
{
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private float panelOffset;
    [SerializeField] private float resolutionFactor;

    public  void CreatPanel( List<BaguetteData> baguetteData){
        int i = 0;

        foreach (BaguetteData element in baguetteData){
            GameObject instanteObject = Instantiate(panelPrefab,transform,false);
            element.objectpanel = instanteObject;


            instanteObject.AddComponent<ScrollButton>();
            instanteObject.GetComponent<ScrollButton>().ID = i;


            Sprite mySpreite = element.sprite;
            instanteObject.GetComponent<RectTransform>().sizeDelta = new Vector2((
                                        mySpreite.bounds.size.x)*resolutionFactor, 
                                        (mySpreite.bounds.size.y)*resolutionFactor );
            instanteObject.GetComponent<Image>().sprite = mySpreite;
            
            if(i!=0){
                instanteObject.transform.localPosition = new Vector2((baguetteData[i-1].objectpanel.transform.localPosition.x+
                (panelPrefab.GetComponent<RectTransform>().sizeDelta.x)-panelOffset), instanteObject.transform.localPosition.y);
            }
            element._position = -instanteObject.transform.localPosition;

            i++;
        }
        SendDataToSnapingScrolling(baguetteData);
    }
    private void SendDataToSnapingScrolling(List<BaguetteData> baguetteData){
        BaguetteSnapScrolling snapingScrolling = GetComponent<BaguetteSnapScrolling>();
        snapingScrolling.baguetteDatas = baguetteData;
        snapingScrolling.StartMove(-1);

    }
     private void DestroyPanel(){
        while(transform.childCount != 0){
            Destroy(transform.GetChild(0));
        }   
        
    }
    
}
