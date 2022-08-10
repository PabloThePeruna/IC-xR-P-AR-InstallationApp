using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTestScript : MonoBehaviour
{
    /*
    private LineRenderer lr;
    private LineRenderer lr2;
    private LineRenderer lr3;
    private LineRenderer lr4;
    */
    [Range(1,100)]
    public static int TestLength = 100;

    [Range(-10f, 10f)]
    public float MinimumValue;
    [Range(-10f, 10f)]
    public float MaximumValue;
    public LineRenderer LineRendererPrefab;
    private LineRenderer[] LRStorage = new LineRenderer[TestLength];
    
    //private static LineRenderer[] lrArray=new LineRenderer[4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < TestLength; i++)
        {
            LRStorage[i] = Instantiate(LineRendererPrefab);
        }


        InvokeRepeating("RandomPlacement", 0.5f, 0.05f);
        
        
    }


    void RandomPlacement()
    {
        for(int i=0; i < TestLength; i++)
        {
           LRStorage[i].SetPosition(1, new Vector3(UnityEngine.Random.Range(MinimumValue, MaximumValue), UnityEngine.Random.Range(MinimumValue, MaximumValue), UnityEngine.Random.Range(MinimumValue, MaximumValue)));
           LRStorage[i].SetColors(new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f),0),new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f),1));
        }
    }
}
