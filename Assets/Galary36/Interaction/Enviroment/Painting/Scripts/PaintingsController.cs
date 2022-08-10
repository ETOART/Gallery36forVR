using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingsController : MonoBehaviour
{
    [SerializeField] private GameObject _paintingPrefab;
    [SerializeField] private List<Texture> _images;
    private List<GameObject> _paintingGameObjs = new List<GameObject>();
    private float _paintingOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        PaintingData paintingData = new PaintingData(_images[0], (float)_images[0].width / _images[0].height * 0.01f, (float)_images[0].height / _images[0].height * 0.01f);
        GameObject paintingGameObj = Instantiate(_paintingPrefab, gameObject.transform.position, Quaternion.identity);
        //paintingGameObj.transform.parent = gameObject.transform;
        _paintingGameObjs.Add(paintingGameObj);
        Painting painting = paintingGameObj.GetComponent<Painting>();
        painting.Initialize(paintingData);
        GameObject prevPainting = paintingGameObj;
        PaintingData prevPaintingData = paintingData;
        for (int i = 1; i < _images.Capacity; i++)
        {
            //paintingData = new PaintingData(_images[i], (float)_images[i].width / _images[i].height, (float)_images[i].height / _images[i].height);
            paintingData = new PaintingData(_images[i], Random.Range(1, 10) * 0.01f, Random.Range(1, 10) * 0.01f);
            paintingGameObj = Instantiate(_paintingPrefab, prevPainting.transform.position + new Vector3(paintingData.width / 2f * 10 + prevPaintingData.width / 2f * 10 +_paintingOffset, 0, 0), Quaternion.identity);
            //paintingGameObj.transform.parent = gameObject.transform;
            _paintingGameObjs.Add(paintingGameObj);
            painting = paintingGameObj.GetComponent<Painting>();
            painting.Initialize(paintingData);
            prevPainting = paintingGameObj;
            prevPaintingData = paintingData;
        }
        foreach (GameObject obj in _paintingGameObjs)
        {
            obj.transform.parent = gameObject.transform;
        }
        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
