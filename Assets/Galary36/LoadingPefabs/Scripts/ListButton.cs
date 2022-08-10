using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : ArtistButton
{

    [SerializeField]public string ListName;

    void Start()
    {
        SetFileName();
        loadingImage.SetActive(true);
        _loadingPrefabs = FindObjectOfType<LoadingPrefabs>();
        //artistTextField.text = FileName;
        StartCoroutine(GetPaintingsCountRoutine());
    }

    override public IEnumerator GetPaintingsCountRoutine()
    {
        StartCoroutine(_loadingPrefabs.GetPaintingsIds("",ListName));
        while (!_loadingPrefabs.aristsPaintingsCount.ContainsKey(ListName))
        {
            yield return null;
        }
        Debug.Log("get");
        _webPaintingsCount = _loadingPrefabs.aristsPaintingsCount[ListName];
        loadingImage.SetActive(false);
        ShowPaintingsCount();
    }
    override public  IEnumerator GetPaintingsJsonRoutine()
    {
        if (_devicePaintingsCount != _webPaintingsCount)
        {
            _loadingPrefabs.DeleteJson(FileName);
            StartCoroutine(_loadingPrefabs.SendRequestRoutine(paintingsCountText,"", ListName));
            while (!_loadingPrefabs.isJsonDownloaded(FileName))
            {
                yield return null;
            }
        }
        loadingImage.SetActive(false);
        ShowPaintingsCount();
    }

    override public void SetFileName()
    {
        FileName = ListName;
    }
}
