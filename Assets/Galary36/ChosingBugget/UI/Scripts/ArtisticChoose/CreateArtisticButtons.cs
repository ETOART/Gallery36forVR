using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArtisticButtons : MonoBehaviour
{
    [SerializeField] private List<string> _artistsNames;
    [SerializeField] private PaintingsManager paintingsManager;
    [SerializeField] private ChooseArtist chooseArtist;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private float panelOffset;
    private List<ButtonArtist> buttonArtists;
    private List<GameObject> buttonObject;

    private void DestroyPanel(){
        while(transform.childCount != 0){
            Destroy(transform.GetChild(0));
        }   
        
    }
    public void CreatPanel( List<string> artistsNames){
        buttonArtists = new List<ButtonArtist>();
        buttonObject = new List<GameObject>();

        int i = 0;
        foreach (string name in artistsNames){
            GameObject instanteObject = Instantiate(panelPrefab,transform,false);
            buttonObject.Add(instanteObject);
            ButtonArtist instButtonArtist = instanteObject.AddComponent<ButtonArtist>();
            instButtonArtist.SetName(name);
            instButtonArtist.chooseNewArtist += chooseArtist.ChooseArt;
            
            if(i!=0){
                instanteObject.transform.localPosition = new Vector2(instanteObject.transform.localPosition.x,
                                                        (buttonObject[i-1].transform.localPosition.y-
                                                        (panelPrefab.GetComponent<RectTransform>().sizeDelta.y)-panelOffset));
            }
            instButtonArtist._position = -instanteObject.transform.localPosition;
            i++;
        }
    }
    
    

}
