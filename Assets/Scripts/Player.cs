﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    
    public int level, experience, skillPoint,gold;
    public string[] items, equips;

    GameObject gameManager;
    Inventory playerInventory;
    Item item;
    itemManager itManager;
    Equipment[] currentEquipment;
    [SerializeField]
    PlayerData data;

    public object currentEquip { get; internal set; }

    public void Start() {
        GameObject goldTxt = StaticMethods.FindInActiveObjectByName("txtGold");
        goldTxt.GetComponent<Text>().text = gold.ToString();
    }

    public void SavePlayer() {

        GameObject itemler = GameObject.Find("ItemManager");
        itManager = itemler.GetComponent<itemManager>();
        gameManager = GameObject.Find("GameManager");
        playerInventory = gameManager.GetComponent<Inventory>();
        currentEquipment = gameManager.GetComponent<EquipmentManager>().CurrentEq();
        
        equips = new string[currentEquipment.Length];
        for(int i = 0; i < equips.Length; i++) {
            if(currentEquipment[i]!=null )
                equips[i] = currentEquipment[i].name;
            
        }
        
        items = new string[playerInventory.items.Count];
        for (int i = 0; i < items.Length; i++) {
            items[i] = playerInventory.items[i].name;
        }
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer() {
        GameObject itemler = GameObject.Find("ItemManager");
        itManager = itemler.GetComponent<itemManager>();
        data = SaveSystem.LoadPlayer();
        if(Inventory.instance.items.Count >0)
        Inventory.instance.items.Clear();

        level = data.level;
        skillPoint = data.skillPoint;
        experience = data.Experience;
        gold = data.gold;

        for (int i =0; i<itManager.items.Count;i++) {
            for(int j = 0; j < data.items.Length ;j++) {
                
                string a = itManager.items[i].name;
                string b = data.items[j].ToString();
                if ( a == b ) {
                    item = itManager.items[i];
                    Inventory.instance.Add(item);
                }
            }
        }

        for (int i = 0; i < itManager.items.Count; i++) {
            for (int j = 0; j < data.equips.Length; j++) {
                if (data.equips[j] != null) {
                    string a = itManager.items[i].name;
                    string b = data.equips[j].ToString();

                    if (a == b) {
                        //item = itManager.items[i];
                        Equipment eq1 = (Equipment)itManager.items[i];
                        EquipmentManager.instance.Equip(eq1);
                    }
                }
            }
        }


        GameObject itemManager = GameObject.Find("itemManager");

        GameObject goldTxt = StaticMethods.FindInActiveObjectByName("txtGold");
        goldTxt.GetComponent<Text>().text = gold.ToString();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}