using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interaction;
using Interaction.Service;
using UnityEngine;

public class Decline : MonoBehaviour, IDeclinable
{
    public InteractionType[] HowToInteract()
    {
        var types = new List<InteractionType>();
        types.Add(InteractionType.Decline);
        return types.ToArray();
    }

    public void DeclineOrder()
    {
        CustomerWindow.Window.DeclineOrder();
    }
}
