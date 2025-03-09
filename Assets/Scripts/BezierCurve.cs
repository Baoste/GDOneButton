
using UnityEngine;

public class BezierCurve
{
    private Vector3 pos0;
    private Vector3 pos1; 
    private Vector3 pos2;
    public BezierCurve(Vector3 pos0, Vector3 pos1, Vector3 pos2)
    {
        this.pos0 = pos0;
        this.pos1 = pos1;
        this.pos2 = pos2;
    }

    public Vector3 GetPosition(float t)
    {
        return (1 - t) * (1 - t) * pos0 + 2 * t * (1 - t) * pos1 + t * t * pos2;
    }

    public Vector3 GetTangent(float t)
    {
        Vector3 vec = (-2 + 2 * t) * pos0 + (2 - 4 * t) * pos1 + 2 * t * pos2; 
        vec.Normalize(); 
        return vec;
    }
}
