using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GearRemoveTrigger : ACollider2d<Gear>
{
    public event Action Trigger;
    
    protected override void Start()
    {
        base.Start();
        this.Delay(() =>
        {
            _collider.size = ((RectTransform) transform).rect.size;
        });
    }

    protected override void OnTouch(Gear gear)
    {
        Destroy(gear.gameObject);
        Trigger?.Invoke();
    }
}
