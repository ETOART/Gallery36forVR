using UnityEngine;
using UnityEngine.UI;

public class ChangeArtisticName : MonoBehaviour
{
    private Text myText;
    public void ChangeText(PaintingData paintingData){
        myText = GetComponent<Text>();
        SetData(paintingData.Name);
    }
    
    private void SetData(string actualText){
        myText.text =  actualText;
    }
}
