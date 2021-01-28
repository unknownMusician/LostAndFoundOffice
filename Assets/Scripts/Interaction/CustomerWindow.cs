using Interaction.Service;
using UnityEngine;

namespace Interaction {
    class CustomerWindow : MonoBehaviour, IReceivable {

        public InteractionType[] HowToInteract() {
            throw new System.NotImplementedException();
        }

        public void Receive(IGrabbable placeable) {
            throw new System.NotImplementedException();
        }

        public bool TryReceive() {
            throw new System.NotImplementedException();
        }
    }
}
