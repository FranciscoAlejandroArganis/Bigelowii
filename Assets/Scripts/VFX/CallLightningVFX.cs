using DigitalRuby.LightningBolt;
using UnityEngine;

public class CallLightningVFX : Wrapper
{

    public Vector3 positionStart;

    public Vector3 positionEnd;

    public LightningBoltScript template;

    private LightningBoltScript instance;

    private float time;

    private float speed;

    public void Start()
    {
        time = 1;
        speed = 3;
    }

    public override void Update()
    {
        if (CameraController.state == CameraController.State.Fixed)
        {
            if (time < 1)
            {
                instance.EndObject.transform.position = Vector3.Lerp(positionStart, positionEnd, time);
                time += speed * Time.deltaTime;
                if (time >= 1)
                    instance.EndObject.transform.position = positionEnd;
            }
            base.Update();
        }
    }

    public override void Play()
    {
        instance = Instantiate(template);
        instance.StartObject.transform.position = positionStart;
        instance.EndObject.transform.position = positionStart;
        time = 0;
    }

    public override void Stop()
    {
        Destroy(instance.gameObject);
        instance = null;
    }

}
