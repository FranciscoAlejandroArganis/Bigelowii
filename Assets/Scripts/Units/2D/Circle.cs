using UnityEngine;

public class Circle : Unit2D
{

    public PrismaticDischargeVFX discharge;

    public ParticleSystemWrapper momentOfClarity;

    public ParticleSystem call;

    public CallLightningVFX lightning;

    public override void Start()
    {
        base.Start();
        agent = new CircleAgent(this);
    }

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.circle;
    }

    public override void SetCommandButton(CommandButton commandButton, uint card, int button)
    {
        commandButton.active = false;
        switch (card)
        {
            case 0:
                switch (button)
                {
                    case 0:
                        SetMoveButton(commandButton);
                        break;
                    case 1:
                        commandButton.image.sprite = UI.sprites.attack;
                        commandButton.action = new PrismaticDischarge(this, discharge);
                        commandButton.type = CommandButton.Type.Command;
                        commandButton.transition = 1;
                        break;
                    case 2:
                        commandButton.image.sprite = UI.sprites.momentOfClarity;
                        commandButton.action = new MomentOfClarity(this, momentOfClarity);
                        commandButton.type = CommandButton.Type.Command;
                        commandButton.transition = 1;
                        break;
                    case 3:
                        commandButton.image.sprite = UI.sprites.callLightning;
                        commandButton.action = new CallLightning(this, call, lightning);
                        commandButton.type = CommandButton.Type.Command;
                        commandButton.transition = 1;
                        break;
                    case 15:
                        SetEndTurnButton(commandButton);
                        break;
                    default:
                        SetEmptyButton(commandButton);
                        break;
                }
                break;
            case 1:
                switch (button)
                {
                    case 15:
                        SetCancelButton(commandButton);
                        break;
                    default:
                        SetEmptyButton(commandButton);
                        break;
                }
                break;
            case 2:
                switch (button)
                {
                    case 14:
                        SetConfirmButton(commandButton);
                        break;
                    case 15:
                        SetCancelButton(commandButton);
                        break;
                    default:
                        SetEmptyButton(commandButton);
                        break;
                }
                break;
        }
    }

}
