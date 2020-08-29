using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour, IEnumerable<Color>
{
    [SerializeField] private PaletteAsset _palette;
    
    private static Palette instance;
    public static Palette Instance
    {
        get
        {
            if (instance)
                return instance;
            
            instance = FindObjectOfType<Palette>();
            if (instance)
                return instance;
                
            return instance = new GameObject {
                name = nameof(Palette)
            }.AddComponent<Palette>();
        }
    }
    
    protected virtual void Awake ()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy ( gameObject );
    }

    public IEnumerator<Color> GetEnumerator()
    {
        return _palette.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Color Random() => _palette.Random();
}