using UnityEngine;
 
public static class RectTransformExt 
{
    public static Rect GetWorldRect(this RectTransform rt) 
    {
        var corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0], rt.rect.size);
    }
}