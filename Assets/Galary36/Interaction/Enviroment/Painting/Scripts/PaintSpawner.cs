using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaintSpawner : MonoBehaviour
{
    [SerializeField] private GameObject centerSpawnDummy;   
    [SerializeField] private GameObject paintPrefab;
    [SerializeField] private int paitingsCount= 0;
    [SerializeField] private float wallHeight;
    [SerializeField] private float wallWidth;

    [SerializeField] private float minOffset;
    [SerializeField] private float maxOffset;

    PaintingsManager paintingsManager;
    private List<PaintingData> processingPaints;
    private List<List<PaintingData>> paintsTable = new List<List<PaintingData>>();
    [SerializeField] private string artistName = "Jarosław Kiełczyński";
    private void Start()
    {
        paintingsManager = FindObjectOfType<PaintingsManager>();
        //startRotation = transform.rotation.eulerAngles*-1;
         StartCoroutine(GetPaintings());
       // List<PaintingData> paintingData = paintingsManager.GetArtistPaintings(artistName, 30, paintingsManager.count);
       // paintingsManager.count += 30;
       // StartCoroutine(StartSpawn(paintingData));
    }

    private IEnumerator GetPaintings()
    {
        Debug.Log(" Start Get Paintings");
        while (!paintingsManager.Paintings.ContainsKey(artistName))
        {
            yield return null;
        }
        Debug.Log("Get Paintings");
        List<PaintingData> paintingData = paintingsManager.GetArtistPaintings(artistName, paitingsCount, paintingsManager.count);
        paintingsManager.count += paitingsCount;
        StartCoroutine(StartSpawn(paintingData));
    }

    public IEnumerator StartSpawn(List<PaintingData> paints)
    {
        Debug.Log("Start Spwan");
        if (paints == null || paints.Count == 0)
            throw new System.Exception("Paints not exist. Add paints using method \"AddPaintingData\"");

        processingPaints = paints.OrderBy(a=>a.width*a.height).ToList();

        float paintsWidth = paints.Sum(a => a.width);
        
        //place in one row (randomly) if width available
        if (paintsWidth < wallWidth)
        {
            Debug.Log("good width");
            paintsTable.Add(paints.OrderBy(a => System.Guid.NewGuid()).ToList());
       }
        else
        {
            while (processingPaints.Count > 0 && paintsTable.Sum(a => a.Max(h => h.height)) < wallHeight)
            {
                List<PaintingData> rowPaints = new List<PaintingData>();
                while (rowPaints.Sum(a => a.width) < wallWidth -rowPaints.Count*maxOffset && processingPaints.Count > 0)
                {
                    rowPaints.Add(processingPaints.First());
                    processingPaints.RemoveAt(0);
                   
                }
               
                    

                if (paintsTable.Sum(a => a.Max(h => h.height)) < wallHeight)
                    paintsTable.Add(rowPaints.OrderBy(a => System.Guid.NewGuid()).ToList());
                //      throw new System.Exception("Wall size so small for spawning all paints");
            }
        }
        var currentPosition = centerSpawnDummy.transform.position;
        var initialPosition = currentPosition;
        float halfHeight = paintsTable.Sum(a => a.Max(h => h.height)) / 2;
        GameObject firstPaiting = null;
        GameObject prevPainting = null;
        GameObject NextPainting;
        for (int i = 0; i < paintsTable.Count; i++)
        {
            float halfWidth = (paintsTable[i].Sum(a => a.width)+ paintsTable[i].Count*maxOffset) / 2;
            var difference = centerSpawnDummy.transform.position - initialPosition;
            
            for (int j = 0; j < paintsTable[i].Count; j++)
            {
                yield return new WaitForSeconds(0.1f);
                difference = centerSpawnDummy.transform.position - initialPosition; 
                var painting = Instantiate(
                        paintPrefab,
                        difference + currentPosition - transform.up * halfHeight - transform.right * halfWidth,
                        transform.rotation                       
                    ).GetComponent<Painting>();

                painting.transform.SetParent(this.transform, true);
               
               // painting.transform.localScale = new Vector3(paintsTable[i][j].width, paintsTable[i][j].height, painting.transform.localScale.z);
                painting.Initialize(paintsTable[i][j]);
                if (j == 0)
                {
                    firstPaiting = painting.transform.gameObject;

                }
                else
                {
                    prevPainting.GetComponent<Painting>().NextPainting = painting.transform.gameObject;
                    painting.GetComponent<Painting>().PrevPainting = prevPainting;
                }
                if (j == paintsTable[i].Count - 1)
                {
                    firstPaiting.GetComponent<Painting>().PrevPainting = painting.transform.gameObject;
                    painting.GetComponent<Painting>().NextPainting = firstPaiting;
                }
              
                    prevPainting = painting.transform.gameObject;
                    currentPosition += transform.right * (paintsTable[i][j].width / 2);
                if (j + 1 != paintsTable[i].Count)
                {
                    currentPosition += transform.right * Random.Range(minOffset, maxOffset);
                    currentPosition += transform.right * (paintsTable[i][j + 1].width / 2);
                }
            }
            currentPosition.x = (centerSpawnDummy.transform.position-difference).x;
            currentPosition.z = (centerSpawnDummy.transform.position-difference).z;
            currentPosition += transform.up * Random.Range(minOffset, maxOffset);
            currentPosition += transform.up * paintsTable[i].Max(a => a.height / 2);
            if (i + 1 != paintsTable.Count)
                currentPosition += transform.up * paintsTable[i + 1].Max(a => a.height / 2);
        }
    }

  
}
