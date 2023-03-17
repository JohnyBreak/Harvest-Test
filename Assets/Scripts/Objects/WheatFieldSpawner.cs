#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
public class WheatFieldSpawner : MonoBehaviour
{   
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _plant;
    [SerializeField] private int _row;
    [SerializeField] private int _column;
    [SerializeField] private float _spacing;

    public void Spawn() 
    {
        var field = new GameObject("WheatField");//Instantiate( _spawnPosition.position, Quaternion.identity);
        field.AddComponent<WheatField>();
        field.transform.parent = this.transform;
        field.transform.position = _spawnPosition.position;
        for (int y = 0; y < _column; y++)
        {
            for (int x = 0; x < _row; x++)
            {
                var plant = Instantiate(_plant, field.transform);

                Vector3 pos = new Vector3(x, 0, y) * _spacing;
                plant.transform.localPosition = pos;
            }
        }


    }
}

[CustomEditor(typeof(WheatFieldSpawner))]
public class WheatFieldSpawnerEditor : Editor 
{
    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var spawner = (WheatFieldSpawner)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("SpawnField"))
        {
            spawner.Spawn();
            Debug.LogError("SpawnField");
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif