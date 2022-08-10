using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisButtonsCreator : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;
    private LoadingPrefabs loadingPrefabs;
    // Start is called before the first frame update
    void Start()
    {
         loadingPrefabs = FindObjectOfType<LoadingPrefabs>();
        StartCoroutine(CreateButtons());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CreateButtons()
    {
        loadingPrefabs.GetArtistsNames();
        while (!loadingPrefabs.isArtistListReady)
        {
            yield return null;
        }
        foreach(string artists in loadingPrefabs.Artists)
        {
            Debug.Log(artists);
            GameObject button = Instantiate(ButtonPrefab);
            button.transform.parent = gameObject.transform;
            button.name = artists;
            button.GetComponent<ArtistButton>().ArtistName = artists;
        }
       foreach(string artists in loadingPrefabs.GetDownloadedArtitsNames())
        {
            Debug.Log(artists+ "Downloaded");
        }

    }
}
