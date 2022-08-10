using UnityEngine;
using UnityEngine.UI;

public class ButtonArtist : MonoBehaviour
{
    public Vector2 _position{get;set;}
    private string artistName;
    private BagertteButtonController bagertteButtonController;

    public delegate void Choose(string name);
    public event Choose chooseNewArtist;

    public void SetName(string name){
        transform.name = name;
        artistName = name;
        transform.GetChild(0).GetComponent<Text>().text = name;
    }
    private void OnMouseUp() 
	{
        chooseNewArtist?.Invoke(artistName);
        bagertteButtonController.LoadPaintScreen();
	}
    private void Awake(){
       bagertteButtonController = FindObjectOfType<BagertteButtonController>();

    }
   
}
