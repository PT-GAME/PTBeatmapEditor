using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListMaker : MonoBehaviour
{
    public GameObject itemPrefab, noteListParent, eventListParent;
    public LevelDataContainer ldc;
    

    private void Awake()
    {
        LoadEvents.levelLoadComplete += MakeLists;
    }

    private void OnDisable()
    {
        LoadEvents.levelLoadComplete -= MakeLists;
    }

    private void MakeLists()
    {
        foreach (var data in ldc.level.note)
        {
            var cache = Instantiate(itemPrefab, noteListParent.transform);
            cache.GetComponentInChildren<Text>().text = data;
        }
        
        foreach (var data in ldc.level.events)
        {
            var cache = Instantiate(itemPrefab, eventListParent.transform);
            cache.GetComponentInChildren<Text>().text = data;
        }
    }
}