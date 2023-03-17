using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleManager : MonoBehaviour
{
    public Action<Vector3, int> SaleEvent;


    public void OnBlockSale(Vector3 finishPos, int moneyAmount) 
    {
        SaleEvent?.Invoke(finishPos, moneyAmount);
    }
}
