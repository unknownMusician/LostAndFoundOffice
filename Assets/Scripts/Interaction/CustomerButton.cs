using Interaction;
using Interaction.Service;
using System.Collections.Generic;
using UnityEngine;

public class CustomerButton : MonoBehaviour, IMessageable
{

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public InteractionType[] HowToInteract() {
        var types = new List<InteractionType>();

        if (CustomerWindow.Window.TryReceive()) { types.Add(InteractionType.Message); };

        _animator.SetBool("Press", true);

        return types.ToArray();
    }

    public void Message() => CustomerWindow.Window.DeclineOrder();
}
