using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] private List<Image> _hearts;

    public int Count
    {
        get => _hearts.Count(x => x.IsActive());
        set => SetHearts(value);
    }

    private void SetHearts(int value)
    {
        for (var i = 0; i < _hearts.Count; i++)
            _hearts[i].gameObject.SetActive(value > i);
    }
}
