using TMPro;
using UnityEngine;

public class StackCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _maxValue;
    private int _currentValue;


    public void SetMaxValue(int value) 
    {
        _maxValue = value;
    }

    public void SetCurrentValue(int value)
    {
        _currentValue = value;

        _text.text = $"{_currentValue} / {_maxValue}";
    }
}
