using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public string _itemName;      // 名前
    public int _itemId;                 // アイテムID
    public int _exterior;               // 外装
    public int _machine;              // 機械
    public int _fuel;                      // 燃料
    public ItemType _itemType;  // アイテムの種類

    // アイテムタイプ
    public enum ItemType
    {
        Memory,
        Screw,
        DryCell,
        IronPlate,
        Semiconductor,
        Battery
    }

    // ここでリスト化時に渡す引数をあてがう
    public Item(string name, int id, int exterior, int machine, int fuel, ItemType item)
    {
        _itemName = name;
        _itemId = id;
        _exterior = exterior;
        _machine = machine;
        _fuel = fuel;
        _itemType = item;
    }
}
