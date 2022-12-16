using UnityEngine;

public class FractalCell : MonoBehaviour
{

    public void Start()
    {
        transform.position += new Vector3(0, Offset(transform.position.x, transform.position.z), 0);
    }

    public unsafe float Offset(float x, float z)
    {
        if (x == 0 && z == 0)
            return 0;
        uint a = (*(uint*)&x) ^ (*(uint*)&z);
        float b = (*(float*)&a) / Mathf.Sqrt(x*x+z*z);
        a = (*(uint*)&b) % 1024;
        b = (float)a / 1024;
        return .5f * b - .25f;
    }

}
