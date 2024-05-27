using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum item_type { HEALTH, ATTACK_UP, DEFENSE_UP, DAMAGE };
    public item_type itemType;
    public float itemValue;

    public Item(item_type _itemType, float _itemValue)
    {
        itemType = _itemType;
        itemValue = _itemValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7)) UseItem(item_type.HEALTH);
        if (Input.GetKeyDown(KeyCode.Keypad8)) UseItem(item_type.ATTACK_UP);
        if (Input.GetKeyDown(KeyCode.Keypad9)) UseItem(item_type.DEFENSE_UP);
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) UseItem(item_type.DAMAGE);
    }

    void UseItem(item_type _itemType)
    {
        switch (_itemType)
        {
            case item_type.HEALTH:
                break;
            case item_type.ATTACK_UP:
                break;
            case item_type.DEFENSE_UP:
                break;
            case item_type.DAMAGE:
                break;
        }
        return;
    }
}
