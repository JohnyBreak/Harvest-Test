using DG.Tweening;
using System.Collections;
using Zenject;
using UnityEngine;

public class FlyingCoin : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _parent;
    //[SerializeField] 
    private Vector3 _coinStartPosition;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private RectTransform _walletCoinTransform;

    private Wallet _wallet;
    private SaleManager _saleManager;
    private Vector2 _finishPosition;

    [Inject]
    private void Construct(SaleManager manager, Wallet wallet)
    {
        _wallet = wallet;
        _saleManager = manager;
    }

    private IEnumerator Start()
    {
        yield return null;
        Vector2 tempPos = new Vector2(_walletCoinTransform.position.x, _walletCoinTransform.position.y);
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, tempPos, _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out pos);
        _finishPosition = pos;

        _saleManager.SaleEvent += MoveCoinToWallet;

        //MoveCoinToWallet();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) MoveCoinToWallet();
    }

    public void MoveCoinToWallet(Vector3 startPos, int moneyAmount) 
    {
        _coinStartPosition = startPos;

        var coin = Instantiate(_coinPrefab, transform);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_coinStartPosition);//Input.mousePosition;
        coin.transform.position = screenPos;
        coin.transform.localScale = Vector3.zero;

        coin.transform.DOScale(1f, 0.1f).SetEase(Ease.OutBack);
        coin.GetComponent<RectTransform>().DOAnchorPos(_finishPosition, 0.8f)/*
            .SetDelay(0.5f)*/.SetEase(Ease.InBack).OnComplete(() => _wallet.AddMoney(moneyAmount));
        coin.transform.DOScale(0f, 0.3f).SetDelay(1f).SetEase(Ease.OutBack);
    }

    private void OnDestroy()
    {
        _saleManager.SaleEvent -= MoveCoinToWallet;
    }

}
