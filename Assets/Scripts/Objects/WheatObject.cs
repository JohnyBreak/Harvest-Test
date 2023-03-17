using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class WheatObject : MonoBehaviour, ICuttable
{
    [SerializeField] private Transform _wheatTop;

    private Collider _collider;
    private Vector3 _startScale;
    private Vector3 _cuttedScale;
    private WheatManager _wheatManager;


    [Inject]
    private void Construct(WheatManager manager)
    {
        _wheatManager = manager;
    }

    private void Awake()
    {
        _startScale = _wheatTop.localScale;
        _cuttedScale = new Vector3(_startScale.x, 0, _startScale.z);

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        //_collider.enabled = false;
        
    }

    public void Cut() 
    {
        StartGrow();
        _wheatManager.DropBlock(transform.position + Vector3.up);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Cut();
    }


    private void StartGrow()
    {
        _collider.enabled = false;
        _wheatTop.localScale = _cuttedScale;
        _wheatTop.DOScale(_startScale, 10).OnComplete(StopGrow);
    }

    private void StopGrow()
    {
        _collider.enabled = true;
    }
}
