using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerHotspot : ACollider2d<Gear>
{
    protected override void OnTouch(Gear gear)
    {
        gear.Hotspot = _collider;
        gear.AllowDrag = true;
    }

    protected override void OnLeave(Gear gear)
    {
        gear.Hotspot = null;
        gear.AllowDrag = false;
    }
}
