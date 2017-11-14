using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    // リスト化をして下のvoid Start内でリストに追加
    public List<Item> items = new List<Item>();
	// Use this for initialization
	void Start () {
        items.Add(new Item("Memory", 0, 0, 30, 0, Item.ItemType.Memory));
        items.Add(new Item("Screw", 1, 30, 0, 0, Item.ItemType.Screw));
        items.Add(new Item("DryCell", 2, 0, 0, 30, Item.ItemType.DryCell));
        items.Add(new Item("IronPlate", 3, 50, 0, 0, Item.ItemType.IronPlate));
        items.Add(new Item("Semiconductor", 4, 0, 50, 0, Item.ItemType.Semiconductor));
        items.Add(new Item("Battery", 5, 0, 0, 50, Item.ItemType.Battery));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
