using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Gear : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    /*
     * COLOR OBJECT IMPLEMENTATION
     */
    public Color Color
    {
        get => _color;
        set => SetColor(value);
    }

    protected Image _image;
    protected Color _color;
    
    protected void SetColor(Color color)
    {
        _color = color;
        this.Delay(() =>
        {
            _image.color = _color;
        });
    }
    /*
     * << END
     */
        
    public bool AllowDrag { get; set; }
    public BoxCollider2D Hotspot { get; set; }

    private float _height;
    private Vector2 _pos;
    private bool _dragging;
    
    IEnumerator Start()
    {
        _image = GetComponent<Image>();
        yield return new WaitForEndOfFrame();
        _height = ((RectTransform) transform).rect.height;
        _pos = transform.localPosition;
    }
    
    public void SetPosition(float delta, float coeff)
    {
        _pos.y -= delta*coeff * _height;
        
        if (_dragging) return;
        transform.localPosition = _pos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!AllowDrag) return;
        _dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
        transform.localPosition = _pos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!AllowDrag && !_dragging) return;
        
        var pos = transform.localPosition;
        pos.x += eventData.delta.x;
        transform.localPosition = pos;
    }
}
