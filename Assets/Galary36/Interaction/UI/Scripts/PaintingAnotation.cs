using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PaintingAnotation : MonoBehaviour
{
    [SerializeField] private Text PaintingName;
    [SerializeField] private Text ArtistName;
    [SerializeField] private Text Price;
    [SerializeField] private Text Categories;
    public PaintingData PaintingData; 
    public void SetPaintingAnotation(PaintingData paintingData)
    {
        PaintingData = paintingData;
        PaintingName.text = paintingData.Name;
        ArtistName.text = paintingData.Name;
        Price.text = paintingData.Price.Split(".")[0]+ "zł";
    }
}
