using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Container : ACollider2d<Gear>
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

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
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

    public event Action WrongItem; 
    public event Action CorrectItem; 
    
    protected override void OnTouch(Gear gear)
    {
        if (gear.Color != _color)
            WrongItem?.Invoke();
        else
            CorrectItem?.Invoke();
        
        Destroy(gear.gameObject);
    }
}
