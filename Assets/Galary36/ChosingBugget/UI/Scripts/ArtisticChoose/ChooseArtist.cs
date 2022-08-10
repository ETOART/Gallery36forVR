using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseArtist : MonoBehaviour
{
    [SerializeField] private CreateArtisticButtons CreateArtisticButtons;
    private PaintingsManager paintingsManager;
    private string oldName = "kielczynski";

    public delegate void ChangeArtistPreView(List<PaintingData> paintingData);
    public event ChangeArtistPreView changeArtistPreView;
    public void  ChooseArt(string painterName){
        if(painterName!=oldName){
            oldName = painterName;
            StartCoroutine(GetPaintings(painterName));
        }
    }

    private void Awake(){
        paintingsManager = FindObjectOfType<PaintingsManager>();
        StartCoroutine(GetArtistsNamesList());
        
    }

    private IEnumerator GetPaintings(string painterName)
    {
        while (!paintingsManager.Paintings.ContainsKey(painterName))
        {
            yield return null;
        }
        List<PaintingData> paintingData = paintingsManager.GetArtistPaintings(painterName,1000);
        changeArtistPreView?.Invoke(paintingData);
    }
    private IEnumerator GetArtistsNamesList()
    {
        while (!paintingsManager.isArtistNamesRedy)
        {

            yield return null;
        }
        List<string> _artistsNames = paintingsManager.ArtistsNames;
        CreateArtisticButtons.CreatPanel(_artistsNames);

        ChooseArt(_artistsNames[0]);
    }
}
