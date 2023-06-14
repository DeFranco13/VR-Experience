using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public List<GameObject> Prefabs = new List<GameObject>();
    public GameObject NameTagObject;
    private TextMeshProUGUI nameTag;
    private int _index = 0;
    private GameObject currentPrefab;

    private int index
    {
        get { return _index; }
        set { 
            _index = value;
            placeObject();
        }
    }


    public GameObject CurrentPrefab
    {
        get { return Prefabs[index]; }
    }

    public GameObject ObjectParent;

    public void Start()
    {
        if (NameTagObject != null)
            nameTag = NameTagObject.GetComponent<TextMeshProUGUI>();
        placeObject();
    }

    private void placeObject()
    {
        if (currentPrefab != null)
            Destroy(currentPrefab);
        GameObject newObject = Instantiate(CurrentPrefab, ObjectParent.transform.position, Quaternion.identity);
        newObject.AddComponent<PrefabSpin>();
        currentPrefab = newObject;
        setNameTag();
    }

    private void setNameTag()
    {
        if (nameTag != null)
        {
            String name = currentPrefab.name;
            if (name.Contains("(Clone)"))
                name = name.Substring(0, name.Length - 7);
            nameTag.text = name;
        }
    }

    public void NextPrefab()
    {
        
        if (Prefabs.Count == index+1)
            index = 0;
        else
            index++;
    }

    public void PreviousPrefab()
    {
        if (index-1 < 0)
            index = Prefabs.Count-1;
        else
            index--;
    }
}
