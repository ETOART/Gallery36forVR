using UnityEngine;
using UnityEngine.UI;

public class ChangeGeneralText : MonoBehaviour
{
    private Text myText;

    public void ChangeText(PaintingData paintingData){
        myText = GetComponent<Text>();
        SetData(paintingData.Price);
    }
    
    private void SetData(string actualText){
        myText.text =  actualText;
    }
}
