
using UnityEngine;

public class RandMath
{ 
    public Quaternion RandomRotation(float rangeL, float rangeR)
    {
        float angle = Random.Range(rangeL, rangeR);
        Quaternion q = Quaternion.Euler(0, 0, angle);
        return q * Quaternion.identity;
    }

    public Vector3 RandomPosition(float width, float height, Vector3 lb)
    {
        float x = lb.x + Random.Range(0f, width);
        float y = lb.y + Random.Range(0f, height);
        return new Vector3(x, y, 0);
    }
}
