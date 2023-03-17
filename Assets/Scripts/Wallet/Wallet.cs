using UnityEngine;
using System;
using Zenject;

public class Wallet : MonoBehaviour
{

    public Action ButtonPushEvent;
    public Action<int> AmountChangeEvent;

    public Action<int> AmountSetEvent;

    public Action<int> AmountRemoveEvent;

    //[SerializeField] private int _startAmount;
    private SaveManager _saveManager;
    private int _money;
    //private string _moneyString = "WalletAmount";

    public int CurrentMoney => _money;

    [Inject]
    private void Construct(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    private void Awake()
    {
        _saveManager.Load();
        SetMoney(_saveManager.SaveData.MoneyAmount);

    }

    public void SetMoney(int amount) 
    {
        _money = amount;
        SaveMoney();
        AmountSetEvent?.Invoke(amount);
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        SaveMoney();
        AmountChangeEvent?.Invoke(amount);
    }

    public bool RemoveMoney(int amount)
    {
        if (amount > _money) return false;

        ButtonPushEvent?.Invoke();

        _money -= amount;
        SaveMoney();
        AmountChangeEvent?.Invoke(amount);
        return true;
    }

    public bool CheckMoney(int amount)
    {
        if (amount > _money) return false;

        return true;
    }

    private void SaveMoney()//int amount)
    {
        _saveManager.SaveData.MoneyAmount = _money;
        _saveManager.Save();
    }

}
