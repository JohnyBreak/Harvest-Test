using UnityEngine;
using Zenject;

public class WheatManager : MonoBehaviour
{
    private ObjectPool _wheatPool;

    [Inject]
    private void Construct(ObjectPool pool)
    {
        _wheatPool = pool;
    }

    public void DropBlock(Vector3 position) 
    {
        WheatBlock block = _wheatPool.GetPooledObject().GetComponent<WheatBlock>();
        block.transform.position = position;
        block.transform.localScale = block.InitialScale;
        block.Collider.enabled = true;
        block.gameObject.SetActive(true);
    }
}
