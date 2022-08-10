using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Collections.Generic;
using OAuth;
using System.Web;
using System;
using System.Linq;

public class NetworkManager : MonoBehaviour
{

    private string url = "https://www.gallery36.pl/stock/?_sfm_author=Jaros%C5%82aw%20Kie%C5%82czy%C5%84ski&_sf_ppp=500";
    private string oauth_consumerKey = "ck_29330e4ca43d3bdbfd475b2f76e180648872a5ff";
    private string oauth_consumerSecret = "cs_229e1bb58c84695233491ae0624b362b136f4f18";
    
    private List<PaintingBase> result;
    public bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {
        result = new List<PaintingBase>();
        StartCoroutine(SendRequestRoutine());
    }

    private IEnumerator SendRequestRoutine()
    {
       
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        List<string> idsList = GetIdsList(request.downloadHandler.text);

  

        Debug.Log(idsList);
        Debug.Log(idsList[0]);
        foreach(string ids in idsList)
        {

            string uri = getOAuthDataUrl($"https://www.gallery36.pl/wp-json/wc/v3/products?include={ids}");
            Debug.Log(uri);
            request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();

            result.AddRange(JsonConvert.DeserializeObject<List<PaintingBase>>(request.downloadHandler.text));

        }
        isDone = true;
        Debug.Log("DOOOOOONEEEEE");
    }

    public string getOAuthDataUrl(string url)
    {
        OAuthBase oAuth = new OAuthBase();
        Uri uri = new Uri(url);
        string nonce = oAuth.GenerateNonce();
        string timeStamp = oAuth.GenerateTimeStamp();
        string normalizedUrl, normalizedRequestParameters;

        string sig = oAuth.GenerateSignature(uri, oauth_consumerKey, oauth_consumerSecret, null, null, "GET", timeStamp, nonce, out normalizedUrl, out normalizedRequestParameters);
        sig = HttpUtility.UrlEncode(sig);
        return normalizedUrl + "?" + normalizedRequestParameters + "&oauth_signature=" + sig;
    }

    private string  GetIdsFromHTML(string html)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc.GetElementbyId("api_ids").InnerHtml;
    }
    private List<string> CreateParametrs(string ids)
    {
        List<string> parametrs = new List<string>();
        parametrs.Add($"include={ids}");
        Debug.Log(parametrs[0]);
        return parametrs;
    }

    private List<String> GetIdsList(string html)
    {
        string ids = GetIdsFromHTML(html);
        List<String> result = new List<string>();
        string[] _ids = ids.Split(',');
        int counter = 10;
        var b = _ids.GroupBy(_ => counter++ / 10).Select(v => v.ToArray()).ToArray();
        foreach (var group in b)
        {
            result.Add(String.Join(" ", group));
        }
        return result;
    }

    public List<PaintingBase> getPaintingData()
    {
        return result;
    }
}
