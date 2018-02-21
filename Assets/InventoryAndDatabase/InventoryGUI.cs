using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUI : MonoBehaviour {

    public GameObject inventoryGUI;
    public Inventory inv;
    public GameObject slot;

    public Canvas invCanvas;

    public int row_size;

    public float initial_x;
    public float initial_y;

    public float x_gap;
    public float y_gap;

    public int width = 32;
    public int height = 32;

    public void Awake()
    {
        initGui();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryGUI.SetActive(!inventoryGUI.activeInHierarchy);
        }
    }

    private void initGui()
    {
        inventoryGUI = GameObject.Find("InventoryGUI");
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        invCanvas = inventoryGUI.GetComponent<Canvas>();

        float last_x = initial_x;
        float last_y = initial_y;

        for (int i = 1; i <= inv.items.Capacity; i++)
        {
            GameObject temp = Instantiate(slot);
            GUISlot gslot = temp.GetComponent<GUISlot>();
            gslot.index = i - 1;

            RectTransform rtemp = temp.GetComponent<RectTransform>();
            temp.transform.SetParent(inventoryGUI.transform);

            rtemp.anchoredPosition = new Vector3(last_x, last_y);
            last_x = last_x + width + x_gap;

            if(i % row_size == 0)
            { 
                last_y = last_y - height - y_gap;
                last_x = initial_x;
            }
        }
    }

    private void updateGui()
    {

    }
}
