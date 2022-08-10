using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField] private GameObject _renderPlane;

    [SerializeField] private Texture _texture;
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    public PaintingData _paintingData { get;  set; }
    public GameObject NextPainting;
    public GameObject PrevPainting;
    public void Initialize(PaintingData paintingData)
    {
        Debug.Log("Set Painting" + paintingData.Name);
        _paintingData = paintingData;
        _texture = _paintingData.texture;
        _width = _paintingData.width;
        _height = paintingData.height;
        ChangePlaneScale();
        changePlaneTexture();
      //  transform.GetComponentInChildren<BaggueteSpawner>().SetCorner();
    }

    private void Awake()
    {
        _paintingData = new PaintingData(_texture,_width,_height);
        changePlaneTexture();
        ChangePlaneScale();
    }
    private void Update()
    {
        
    }

    private void changePlaneTexture()
    {
       // ChangePlaneScale();
        _renderPlane.GetComponent<Renderer>().material.mainTexture = _paintingData.texture;
        _renderPlane.GetComponent<Renderer>().material.SetTexture("_EmissionMap", _paintingData.texture);
    }

    public void ChangePlaneScale()
    {
        _renderPlane.gameObject.transform.localScale = new Vector3(_paintingData.width, _paintingData.height, 0.005f);
    }
}
