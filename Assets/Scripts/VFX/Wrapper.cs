using UnityEngine;

public abstract class Wrapper : MonoBehaviour
{

    public Action action;

    private float time;

    private float speed;

    public virtual void Update()
    {
        if (action != null)
        {
            time += speed * Time.deltaTime;
            if (time >= 1)
            {
                action.Execute();
                action = null;
            }
        }
    }

    public void Timer(float time, Action action)
    {
        this.action = action;
        this.time = 0;
        speed = 1 / time;
    }

    public abstract void Play();

    public abstract void Stop();

}
