using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaintingsManager : MonoBehaviour
{

    [SerializeField] private LoadingPrefabs loadingPrefabs;
    public Dictionary<string, List<PaintingBase>> Paintings = new Dictionary<string, List<PaintingBase>>();
    public List<string> ArtistsNames = new List<string>();
    [SerializeField] private string artistName = "kiełczyński";
    public int count = 0;
    public bool isArtistNamesRedy = false;
    // Start is called before the first frame update
    void Start()
    {
         Paintings.Add(artistName, loadingPrefabs.GetDownloadedArtistJson(artistName));
        Debug.Log($"Len: {Paintings[artistName].Count}");       
       // StartCoroutine(GetArtistsNames());
    }

    public List<PaintingData> GetArtistPaintings(string artistURL, int range, int start = 0)
    {
        try
        {
            range = range > Paintings[artistURL].Count ? Paintings[artistURL].Count : range;
            List<PaintingData> paintingData = new List<PaintingData>();
            paintingData.AddRange(Paintings[artistURL].GetRange(start, range).Select(a => new PaintingData(a)));
            return paintingData;
        } 
        catch (ArgumentException ex)
        {
            return new List<PaintingData>();
        }
    }


    private IEnumerator GetArtistsNames()
    {

        loadingPrefabs.GetArtistsNames();
        while (!loadingPrefabs.isArtistListReady)
        {
            yield return null;
        }
        ArtistsNames = loadingPrefabs.GetDownloadedArtitsNames();
        foreach (string artists in ArtistsNames)
        {

            Paintings.Add(artists, loadingPrefabs.GetDownloadedArtistJson(artists));
        }
        print(ArtistsNames.Count);
        foreach (string s in ArtistsNames)
            Debug.Log(s);
        isArtistNamesRedy = true;
    }
}