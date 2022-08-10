using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreator : MonoBehaviour
{
    [SerializeField] private ChooseArtist chooseArtistComponent;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private float panelOffset;
    [SerializeField] private float resolutionFactor;


    private List<PanelData> panelData;

    private void Awake(){
        chooseArtistComponent.changeArtistPreView += SetArtist;
    }
    public void SetArtist(List<PaintingData> paintingData){
        print("Happend");
        DestroyPanel();
        CreatPanel(paintingData);
    }

    private void DestroyPanel(){
        while(transform.childCount != 0){
            Destroy(transform.GetChild(0));
        }   
        
    }
    private  void CreatPanel( List<PaintingData> paintingData){
        panelData = new List<PanelData>();
        int i = 0;

        foreach (PaintingData element in paintingData){
            GameObject instanteObject = Instantiate(panelPrefab,transform,false);

            PanelData instPanelData = instanteObject.AddComponent<PanelData>();
            panelData.Add(instPanelData);
            instPanelData.paintingData = element;
            instPanelData._ID = i;
            instPanelData._object = instanteObject;

            instanteObject.AddComponent<ScrollButton>();
            instanteObject.GetComponent<ScrollButton>().ID = i;


            Sprite mySpreite = Sprite.Create((Texture2D)element.texture, 
                                            new Rect(0.0f, 0.0f, ((Texture2D)element.texture).width, 
                                            ((Texture2D)element.texture).height), new Vector2(0.5f, 0.5f), 100.0f);
            instanteObject.GetComponent<RectTransform>().sizeDelta = new Vector2((
                                        mySpreite.bounds.size.x)*resolutionFactor, 
                                        (mySpreite.bounds.size.y)*resolutionFactor );
            instanteObject.GetComponent<Image>().sprite = mySpreite;
            
            if(i!=0){
                instanteObject.transform.localPosition = new Vector2((panelData[i-1]._object.transform.localPosition.x+
                (panelPrefab.GetComponent<RectTransform>().sizeDelta.x)-panelOffset), instanteObject.transform.localPosition.y);
            }
            panelData[i]._position = -instanteObject.transform.localPosition;

            i++;
        }
        SendDataToSnapingScrolling();
    }
    private void SendDataToSnapingScrolling(){
        SnapingScrolling snapingScrolling = GetComponent<SnapingScrolling>();
        snapingScrolling.panelData = panelData;
        snapingScrolling.StartMove(-1);
        
    }
}   
