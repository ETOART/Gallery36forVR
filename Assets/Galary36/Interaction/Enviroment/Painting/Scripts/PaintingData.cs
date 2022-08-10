using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class PaintingData
{
    public Texture texture;
    public string textureUrl;
    public string Name;
    public string Price;
    List<Category> Categories;
    public string texturePath
    {
        get
        {
            //  Debug.Log(Application.persistentDataPath + "/" + textureUrl.Split('/').Last());
            return Application.persistentDataPath + "/" + textureUrl.Split('/').Last();

        }
    }
    public float width;
    public float height;

    public PaintingData(Texture texture, float width, float height)
    {
        this.texture = texture;
        this.width = width;
        this.height = height;
        this.Name = "bataman";
        this.Price = "123";
    }

    public PaintingData(PaintingBase paintingBase)
    {
        Debug.Log(paintingBase.id);
        this.textureUrl = paintingBase.images.FirstOrDefault().src;
        LoadTextureFromDisk();

        Debug.Log(texturePath);
        this.width = float.Parse(paintingBase.dimensions.width) / 100f;
        this.height = float.Parse(paintingBase.dimensions.height) / 100f;
        this.Name = paintingBase.name;
        this.Price = paintingBase.price;
        this.Categories = paintingBase.categories;
    }

    public IEnumerator DownloadTexture(Text progressText)
    {
        if (!File.Exists(texturePath))
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(textureUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string allamount = progressText.text.Split('/')[1];
                if (Convert.ToInt32(allamount) >= Convert.ToInt32(progressText.text.Split('/')[0]))
                    progressText.text = $"{Convert.ToInt32(progressText.text.Split('/')[0]) + 1}/{allamount}";
                texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                SaveTextureOnDisk();
            }
            // else
            //      Debug.LogError(request.error+ textureUrl);
        }
        else
        {
            LoadTextureFromDisk();
        }
    }

    private void LoadTextureFromDisk()
    {
        if (File.Exists(texturePath))
        {
            byte[] textureBytes = File.ReadAllBytes(texturePath);
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(textureBytes);
            texture = loadedTexture;
            // Debug.Log($"File {texturePath} Loaded");
        }
    }

    private void SaveTextureOnDisk()
    {
        Texture2D texture2 = (Texture2D)texture;
        byte[] textureBytes = texture2.EncodeToJPG();
        File.WriteAllBytes(texturePath, textureBytes);
        Debug.Log($"File {textureUrl} written");
    }
}
