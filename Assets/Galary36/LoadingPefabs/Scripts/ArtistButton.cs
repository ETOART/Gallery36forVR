using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtistButton : MonoBehaviour
{
    [SerializeField] public Text paintingsCountText;
    [SerializeField] public Text artistTextField;
   // [SerializeField] private string artistUrl;
    [SerializeField] public GameObject loadingImage;
    public int _webPaintingsCount = 0;
    public int _devicePaintingsCount= 0;
    public LoadingPrefabs _loadingPrefabs;
    public string ArtistName;
    public string FileName;
    public void Click()
    {
        loadingImage.SetActive(true);
        StartCoroutine(GetPaintingsJsonRoutine());
    }

    void Start()
    {
        SetFileName();
        loadingImage.SetActive(true);
        _loadingPrefabs = FindObjectOfType<LoadingPrefabs>();
       // artistTextField.text = FileName;
        StartCoroutine(GetPaintingsCountRoutine());
    }
    
    public virtual IEnumerator GetPaintingsCountRoutine()
    {
        StartCoroutine(_loadingPrefabs.GetPaintingsIds(ArtistName));
        while (!_loadingPrefabs.aristsPaintingsCount.ContainsKey(ArtistName))
        {
            yield return null;
        }
        Debug.Log("get");
        _webPaintingsCount = _loadingPrefabs.aristsPaintingsCount[ArtistName];
        loadingImage.SetActive(false);
        ShowPaintingsCount();
    }


    public virtual IEnumerator GetPaintingsJsonRoutine()
    {
        if (_devicePaintingsCount != _webPaintingsCount)
        {
            _loadingPrefabs.DeleteJson(FileName);
            StartCoroutine(_loadingPrefabs.SendRequestRoutine(paintingsCountText, FileName));
            while (!_loadingPrefabs.isJsonDownloaded(FileName))
            {
                yield return null;
            }
        }
        loadingImage.SetActive(false);
        ShowPaintingsCount();
    }

    public void ShowPaintingsCount()
    {
        if (!_loadingPrefabs.isJsonDownloaded(FileName))
        {
            _devicePaintingsCount = 0;
        }
        else
        {
            List<PaintingBase> paintings = _loadingPrefabs.GetDownloadedArtistJson(FileName);
            _devicePaintingsCount = paintings.Count;
        }
        paintingsCountText.text = $"{_devicePaintingsCount}/{_webPaintingsCount}";
    }
        
    public virtual void SetFileName()
    {
        FileName = ArtistName;
    }
}
