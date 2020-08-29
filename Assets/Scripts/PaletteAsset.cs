using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaletteAsset", menuName = "Palette asset", order = 0)]
public class PaletteAsset : ScriptableObject, IEnumerable<Color>
{
    [SerializeField] private List<Color> colors;
    
    public IEnumerator<Color> GetEnumerator()
    {
        return colors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Color Random()
    {
        if (colors.Count == 0) return Color.black;
        
        return colors[UnityEngine.Random.Range(0, colors.Count)];
    }
}