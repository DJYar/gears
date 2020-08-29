using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ConveyorEventType
{
    Correct,
    Wrong
}

public class Conveyor : MonoBehaviour
{
    [Header("Prefabs")] 
    [SerializeField] private Container _container;
    
    [Header("Components")]
    [SerializeField] private List<RectTransform> _containerParents;
    [SerializeField] private Belt _belt;

    private List<Container> _containers = new List<Container>();
    
    public event Action<ConveyorEventType> ConveyorEvent;
    
    private void Start()
    {
        _belt.GearRemove += OnWrongItem;
        
        var it = _containerParents.GetEnumerator();
        var colors = Palette.Instance.ToArray();
        foreach (var color in colors)
        {
            it.MoveNext();
            if (!it.Current)
            {
                it = _containerParents.GetEnumerator();
                it.MoveNext();
            }

            var container = Instantiate(_container, it.Current).GetComponent<Container>();
            container.Color = color;
            container.CorrectItem += OnCorrectItem;
            container.WrongItem += OnWrongItem;
            container.enabled = false;
            _containers.Add(container);
        }
    }

    public void Stop()
    {
        foreach (var item in _containers)
            item.enabled = false;
        
        _belt.Stop();
    }

    public void Restart()
    {
        foreach (var item in _containers)
            item.enabled = true;
        
        _belt.Restart();
    }

    private void OnCorrectItem()
    {
        ConveyorEvent?.Invoke(ConveyorEventType.Correct);
    }

    private void OnWrongItem()
    {
        ConveyorEvent?.Invoke(ConveyorEventType.Wrong);
    }
}
