using Interaction;
using Interaction.Service;
using UnityEngine;

public class CustomerButton : MonoBehaviour, IMessageable
{
    public InteractionType[] HowToInteract() => new[] { InteractionType.Message };

    public void Message() => CustomerWindow.Window.DeclineOrder();
}
