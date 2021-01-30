using Interaction;
using Interaction.Service;
using System.Collections.Generic;
using UnityEngine;

public class CustomerButton : MonoBehaviour, IMessageable {
    public InteractionType[] HowToInteract() {
        var types = new List<InteractionType>();

        if (CustomerWindow.Window.TryReceive()) { types.Add(InteractionType.Message); };

        return types.ToArray();
    }

    public void Message() => CustomerWindow.Window.DeclineOrder();
}
