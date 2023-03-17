using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Stack : MonoBehaviour
{
    [SerializeField] private Transform _stakHolder;
    [SerializeField, Min(0f)] private float _blockOffset = 1;
    [SerializeField] private int _maxBlocksInStack = 40;
    [SerializeField] private float _moveDelta = 5; 
    [SerializeField] private float _timeDelta = 5;

    private PlayerTouchMovement _playerTouchMovement;
    private List<WheatBlock> _blocks;
    private SaveManager _saveManager;
    private ObjectPool _wheatPool;
    private StackCanvas _stackCanvas;

    public List<WheatBlock> Blocks => _blocks;
    public bool IsNotEmpty => _blocks.Count > 0;
    public bool CanTake => _blocks.Count < _maxBlocksInStack;

    [Inject]
    private void Construct(ObjectPool pool, StackCanvas stack, SaveManager saveManager)
    {
        _wheatPool = pool;
        _stackCanvas = stack; 
        _saveManager = saveManager;
    }
        
    private void Awake()
    {
        _playerTouchMovement = GetComponent<PlayerTouchMovement>();

        _stackCanvas.SetMaxValue(_maxBlocksInStack);

        _blocks = new List<WheatBlock>();

        _saveManager.Load();
        AddBlocksOnStart(_saveManager.SaveData.BlocksAmount);

        _stackCanvas.SetCurrentValue(_blocks.Count);

    }

    private void Update()
    {
        MoveBlocks();
    }

    private void MoveBlocks() 
    {
        if (_blocks.Count < 2) return;
        
        var delta = Time.deltaTime * _timeDelta;

        for (int i = 1; i < _blocks.Count; i++)
        {
            
            var firstBlock = _blocks[i - 1];
            var secondBlock = _blocks[i];

            Vector3 newPos = _playerTouchMovement.PlayerCameraRelativeDirection;

            newPos *= -1;
            newPos *= _moveDelta;

            secondBlock.transform.localPosition =
                new Vector3(secondBlock.transform.localPosition.x,
                secondBlock.transform.localPosition.y,
                Mathf.Lerp(secondBlock.transform.localPosition.z, firstBlock.transform.localPosition.z - newPos.magnitude, (newPos == Vector3.zero)? delta*2 : delta));
        }
    }

    public WheatBlock GetBlockForSale() 
    {
        var block = _blocks[_blocks.Count - 1];
        _blocks.RemoveAt(_blocks.Count - 1);
        block.transform.parent = null;

        SaveBlocks();

        _stackCanvas.SetCurrentValue(_blocks.Count);
        return block;
    }

    public void AddBlock(WheatBlock block) 
    {
        block.transform.SetParent(_stakHolder);
        
        Vector3 position;
        if (IsNotEmpty)
        {
            position = Vector3.up * _blockOffset * _blocks.Count;
        }
        else 
        {
            position = Vector3.zero;
        }

        block.transform.DOLocalJump(position, 2f, 0, 0.3f).SetEase(Ease.OutQuad);
        block.transform.rotation = _stakHolder.rotation;

        _blocks.Add(block);

        SaveBlocks();
        _stackCanvas.SetCurrentValue(_blocks.Count);
    }

    private void AddBlocksOnStart(int amount)
    {
        WheatBlock block;

        for (int i = 0; i < amount; i++)
        {
            block = _wheatPool.GetPooledObject().GetComponent<WheatBlock>();

            block.transform.SetParent(_stakHolder);
            block.Collider.enabled = false;
            Vector3 position;
            if (IsNotEmpty)
            {
                position = Vector3.up * _blockOffset * _blocks.Count;
            }
            else
            {
                position = Vector3.zero;
            }

            //block.transform.DOLocalJump(position, 2f, 0, 0.3f).SetEase(Ease.OutQuad);
            block.transform.localPosition = position;
            block.transform.rotation = _stakHolder.rotation;

            _blocks.Add(block);

            SaveBlocks();
            _stackCanvas.SetCurrentValue(_blocks.Count);
            block.gameObject.SetActive(true);
        }
    }

    private void SaveBlocks()
    {
        _saveManager.SaveData.BlocksAmount = _blocks.Count;
        _saveManager.Save();
    }
}
