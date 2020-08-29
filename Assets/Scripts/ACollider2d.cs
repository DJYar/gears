using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class ACollider2d<T> : MonoBehaviour where T : Component
{
    protected BoxCollider2D _collider;

    protected virtual void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        
        if (!other.IsTouching(_collider)) return;
    
        var component = other.GetComponent<T>();
        if (!component) return;

        OnTouch(component);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var component = other.GetComponent<T>();
        if (!component) return;

        OnLeave(component);
    }

    protected virtual void OnTouch(T component) { }
    protected virtual void OnLeave(T component) { }
}