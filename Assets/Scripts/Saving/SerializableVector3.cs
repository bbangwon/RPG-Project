using UnityEngine;

public struct SerializableVector3
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }


    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }
}
