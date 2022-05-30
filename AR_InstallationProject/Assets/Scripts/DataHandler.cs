using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    [SerializeField] private GameObject boilerIns;

    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContain;
    [SerializeField] private List<Boiler> boilers;

    private int currentId = 0;

    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }
    }

    private void Start()
    {
        boilers = new List<Boiler>();
        LoadBoiler();
        CreateButton();
    }

    void LoadBoiler()
    {
        var boiler_obj = Resources.LoadAll("Boilers", typeof(Boiler));
        foreach(var boiler in boiler_obj)
        {
            boilers.Add(boiler as Boiler);
        }
    }

    void CreateButton()
    {
        foreach(Boiler i in boilers)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContain.transform);
            b.BoilerID = currentId;
            b.ButtonTexture = i.boilerImage;
            currentId++;
        }
        //buttonContain.GetComponent<UIContentFitter>().Fit();
    }

    public void SetBoiler(int id)
    {
        boilerIns = boilers[id].boilerPrefab;
    }

    public GameObject GetBoiler()
    {
        return boilerIns;
    }
}
