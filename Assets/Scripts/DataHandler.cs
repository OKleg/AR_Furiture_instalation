using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }

    }
     
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> _items;
    [SerializeField] private String label;

    private GameObject furniture;
    private int id = 0;

    void LoadItems()
    {
        var items_obj = Resources.LoadAll("Item", typeof(Item));
        foreach (var item in items_obj)
        {
            _items.Add(item as Item);
        }
    }
    void CreateButtons()
    {
        foreach (Item i in _items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = id;
           if(i.itemImage!= null) b.ButtonTexture = i.itemImage;
            id++;
        }
        buttonContainer.GetComponent<UIContentFitter>().Fit();
    } 
    public GameObject GetFurniture()
    {
        return furniture;
    }

    public void SetFurniture(int id)
    {
        furniture = _items[id].itemPrefab;//_furniture;
    }

    public  bool isSelectFurniture()
    {
        return furniture != null;
    }
    public  void CleanFurniture()
    {
        furniture = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadItems();

        CreateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
