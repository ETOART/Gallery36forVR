using HtmlAgilityPack;
using Newtonsoft.Json;
using OAuth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingPrefabs : MonoBehaviour
{
    [SerializeField] private Text progressText;
    [TextArea] public string authorsText;
    private readonly string oauth_consumerKey = "ck_29330e4ca43d3bdbfd475b2f76e180648872a5ff";
    private readonly string oauth_consumerSecret = "cs_229e1bb58c84695233491ae0624b362b136f4f18";

    public List<string> Artists = new List<string>();
    public bool isArtistListReady = false;
    public Dictionary<string, int> aristsPaintingsCount = new Dictionary<string, int>();


    private void Start()
    {
        StartCoroutine(GetArtistsNames());
    }

    public IEnumerator SendRequestRoutine(Text progressText, string artistName = "", string listName = "")
    {
        CheckFilesAmount();
        List<string> idsList;
        List<PaintingBase> result = new List<PaintingBase>();
        using (UnityWebRequest request = UnityWebRequest.Get(ArtistURL(artistName, listName)))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                idsList = SplitIds(GetIdsList(request.downloadHandler.text));
                foreach (string ids in idsList)
                {

                    Debug.Log(ids);
                    string uri = getOAuthDataUrl($"https://www.gallery36.pl/wp-json/wc/v3/products?include={ids}");
                    Debug.Log(uri);
                    using (UnityWebRequest request2 = UnityWebRequest.Get(uri))
                    {
                        request2.SetRequestHeader("Access-Control-Allow-Origin", "*");
                        yield return request2.SendWebRequest();

                        if (request2.isNetworkError)
                        {
                            Debug.Log(request2.error);
                        }
                        else
                        {
                            Debug.Log(request2.downloadHandler.text);
                            result.AddRange(JsonConvert.DeserializeObject<List<PaintingBase>>(request2.downloadHandler.text));
                        }


                    }
                }
            }
        }
        Debug.Log("ALL REQUESTS DONE_____________________________");
        List<PaintingData> paintingData = new List<PaintingData>();

        paintingData.AddRange(result.Select(a => new PaintingData(a)));
        // Debug.Log("PaintingData=" + paintingData.Count);

        while (paintingData.Count(a => a.texture == null) != 0)
        {
            foreach (var p in paintingData.Where(a => a.texture == null))
            {
                StartCoroutine(p.DownloadTexture(progressText));
                yield return new WaitForSecondsRealtime(0.5f);
            }

            CheckFilesAmount();
        }

        using (StreamWriter file = File.CreateText(JsonFileName(artistName == "" ? listName : artistName)))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, result);
        }
        //StartCoroutine(callback(paintingData));
    }

    public IEnumerator GetPaintingsIds(string artistName = "", string listName = "")
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ArtistURL(artistName, listName)))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(ArtistURL(artistName, listName));
                List<string> idsList = GetIdsList(request.downloadHandler.text);
                aristsPaintingsCount.Add(artistName == "" ? listName : artistName, idsList.Count);

            }
        }
    }

        private string ArtistURL(string artistName, string listName)
        {
            if (listName == "")
                return $"https://www.gallery36.pl/stock/?_sfm_author={artistName}&_sf_ppp=500";
            if (artistName == "")
                return $"https://www.gallery36.pl/stock/?_sft_list={listName}&_sf_ppp=500";
            return $"https://www.gallery36.pl/stock/?_sft_list={listName}&_sfm_author={artistName}&_sf_ppp=500";
        }
        private void CheckFilesAmount()
        {
            progressText.text = Convert.ToString(Directory.GetFiles(Application.persistentDataPath).Length);
        }

        public List<PaintingBase> GetDownloadedArtistJson(string artistName)
        {
            Debug.Log(JsonFileName(artistName));
            //Debug.Log()
            return JsonConvert.DeserializeObject<List<PaintingBase>>(File.ReadAllText(JsonFileName(artistName)));

        }

        public void DeleteJson(string artistName)
        {
            File.Delete(JsonFileName(artistName));
        }

        public bool isJsonDownloaded(string artistName)
        {
            return File.Exists(JsonFileName(artistName));
        }

        public List<string> GetDownloadedArtitsNames()
        {
            if (!isArtistListReady)
            {
                throw new System.Exception("List is't ready");
            }
            List<string> result = new List<string>();
            result.AddRange(Artists.Where(a => File.Exists(JsonFileName(a))));
            return result;
        }

        private string JsonFileName(string artistName)
        {

            return Application.persistentDataPath + "/" + artistName + ".json";
        }
        private string getOAuthDataUrl(string url)
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

    public IEnumerator GetArtistsNames()
    {
        string KielchynskiURl = "https://www.gallery36.pl/stock/?_sfm_author=Jaros%C5%82aw%20Kie%C5%82czy%C5%84ski&_sf_ppp=500";
        using (UnityWebRequest request = UnityWebRequest.Get(KielchynskiURl))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string ttt = request.downloadHandler.text;


                HtmlDocument doc = new HtmlDocument();


                doc.LoadHtml(authorsText);


                IEnumerable nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("sf-level-0"));

                foreach (HtmlNode item in nodes)
                {
                    if (item.InnerHtml != "Authors")
                    {
                        Artists.Add(item.InnerHtml.Split('&')[0]);
                    }
                }
                isArtistListReady = true;
            }
        }
    }

        private string GetIdsFromHTML(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.GetElementbyId("api_ids").InnerHtml;
        }
        private List<string> GetIdsList(string html)
        {

            string ids = GetIdsFromHTML(html);
            Debug.Log(ids);
            return ids.Split(',').ToList();
        }

        private List<string> SplitIds(List<string> _ids)
        {
            foreach (string s in _ids)
                Debug.Log(s);
            List<string> result = new List<string>();
            int counter = 0;
            if (_ids.Count < 9)
            {
                result.Add(String.Join(" ", _ids));
                return result;
            }
            var b = _ids.GroupBy(_ => counter++ / 10).Select(v => v.ToArray()).ToArray();
            foreach (var group in b)
            {
                result.Add(String.Join(" ", group));
            }
            Debug.Log("IDS=" + counter);
            return result;
        }
    }