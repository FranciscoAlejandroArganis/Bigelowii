using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel que muestra la informaci�n de una unidad y su tarjeta de comandos
/// </summary>
public class UnitPanel : Panel
{

    /// <summary>
    /// Componente <c>Image</c> que muestra el sprite de la unidad
    /// </summary>
    public Image image;

    /// <summary>
    /// Componente <c>Slider</c> que muestra la barra de salud de la unidad
    /// </summary>
    public Slider slider;

    /// <summary>
    /// La unidad que se muestra actualmente en el panel
    /// </summary>
    public Unit unit;

    /// <summary>
    /// Arreglo con los botones de la tarjeta de comandos
    /// </summary>
    private CommandButton[] commandCard;

    public void Start()
    {
        commandCard = GetComponentsInChildren<CommandButton>();
    }

    public void Update()
    {
        if (state == State.Timer)
        {
            time += speed * Time.deltaTime;
            if (time >= 1)
            {
                unit = null;
                panel.SetActive(false);
                state = State.Hidden;
            }
        }
    }

    public override void Show()
    {
        UpdatePanel();
        panel.SetActive(true);
        state = State.Visible;
    }

    public override void Hide()
    {
        if (state == State.Visible)
        {
            time = 0;
            state = State.Timer;
        }
    }

    /// <summary>
    /// Actualiza el panel con la informaci�n de la unidad actual
    /// </summary>
    public void UpdatePanel()
    {
        image.sprite = unit.GetUnitSprite();
        slider.value = unit.maxHealth == 0 ? 0 : (float)unit.health / unit.maxHealth;
        SetCommandCard(0);
    }

    /// <summary>
    /// Hace que el panel muestre la tarjeta con el �ndice especificado
    /// </summary>
    /// <param name="card">El �ndice de la tarjeta que se mostrar�</param>
    public void SetCommandCard(uint card)
    {
        uint button = 0;
        while (button < 16)
        {
            CommandButton commandButton = commandCard[button];
            unit.SetCommandButton(commandButton, card, button);
            commandButton.button.interactable = Turn.activeUnit == unit && commandButton.type != CommandButton.Type.Empty;
            button++;
        }
    }

}