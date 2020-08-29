using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Text _data;
    [SerializeField] private Button _submitButton;

    public string Data
    {
        get => _data.text;
        set => _data.text = value.Trim();
    }

    public event Action Submit;

    private void Start()
    {
        _submitButton.onClick.AddListener(() => {
            Submit?.Invoke();
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}