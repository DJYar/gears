using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private int _value; 
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            _scoreText.text = $"{_value:N0}";
        }
    }
}
