﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Product  {

	// Use this for initialization
    public int IDProduct { set; get; }
    public List<int> listIDMaterial { set; get; }
    public string productionName { set; get; }
    public float productionTime { set; get; }
    public int productionCostShop { set; get; }
    public int productionCostMarket { set; get; }

	void Start () {
	
	}

    public Product(int IDProduct, List<int> listIDMaterial, 
        string productionName, float productionTime, 
        int productionCostShop, int productionCostMarket)
    {
        this.IDProduct = IDProduct;
        this.listIDMaterial = listIDMaterial;
        this.productionName = productionName;
        this.productionTime = productionTime;
        this.productionCostShop = productionCostShop;
        this.productionCostMarket = productionCostMarket;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
