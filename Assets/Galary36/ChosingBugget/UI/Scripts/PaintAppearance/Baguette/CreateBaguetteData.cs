using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBaguetteData : MonoBehaviour
{
    [SerializeField] private List<GameObject> baguetteObjects;
    [SerializeField] private List<Sprite> baguetteSprites;
    [SerializeField] private CreatePanelBaguette scrollerContent;
    private List<BaguetteData> baguetteDatas;
    private PaintingsManager paintingsManager;
    private  void Start(){
        // paintingsManager = FindObjectOfType<PaintingsManager>();
        baguetteDatas = new List<BaguetteData>();
        for (int i = 0; i < baguetteObjects.Count; i++)
        {
            baguetteDatas.Add(new BaguetteData());
            baguetteDatas[i].sprite = baguetteSprites[i];
            baguetteDatas[i].objectPrefab = baguetteObjects[i];
            baguetteDatas[i].ID = i;
        }
        scrollerContent.CreatPanel(baguetteDatas);
       // StartCoroutine(GetArtistsNamesList());
    }
    private IEnumerator GetArtistsNamesList()
    {
        while (!paintingsManager.isArtistNamesRedy)
        {

            yield return null;
        }
        baguetteDatas = new List<BaguetteData>();
        for(int i = 0; i < baguetteObjects.Count;i++){
            baguetteDatas.Add(new BaguetteData());
            baguetteDatas[i].sprite = baguetteSprites[i];
            baguetteDatas[i].objectPrefab = baguetteObjects[i];
            baguetteDatas[i].ID = i;
        }
        scrollerContent.CreatPanel(baguetteDatas);
    }
}
