using UnityEngine;

public class SetColor : PropertyAttribute
{
    public readonly float r , g , b;

    public SetColor(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
   
}
