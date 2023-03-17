using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class WalletCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    [SerializeField] private Animator _anim;

    private Wallet _wallet;
    private int _amount = 0;
    private int _remains = 0;
    private Coroutine _changeNumberRoutine;

    private void Awake()
    {
        _wallet.AmountChangeEvent += OnAmountChange;
        _amount = _wallet.CurrentMoney;
        _moneyText.text = _amount.ToString();
    }

    private void OnDestroy()
    {
        _wallet.AmountChangeEvent -= OnAmountChange;
    }

    [Inject]
    private void Construct(Wallet wallet) 
    {
        _wallet = wallet;
    }

    private void OnAmountChange(int amount) 
    {
        if (_changeNumberRoutine != null)
        {
            StopCoroutine(_changeNumberRoutine);
            _changeNumberRoutine = null;
        }

       _changeNumberRoutine = StartCoroutine(ChangeNumberRoutine(amount));
        
    }

    private IEnumerator ChangeNumberRoutine(int addMoney)
    {
        int sign = System.Math.Sign(addMoney);

        _remains += addMoney;
        int end = _remains + _amount;
        yield return null;
        _anim.SetBool("IsShaking", true);
        while (Mathf.Abs(_remains) != 0)
        {
            _amount += sign;
            _remains -= sign;
            _moneyText.text = _amount.ToString();
            yield return new WaitForSeconds(.015f);
        }
        _anim.SetBool("IsShaking", false);
    }
}
