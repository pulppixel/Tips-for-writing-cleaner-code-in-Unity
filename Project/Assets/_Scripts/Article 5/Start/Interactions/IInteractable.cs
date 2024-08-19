using UnityEngine;

namespace Tips.Part_5_Start
{
    /// <summary>
    /// Interface defining the interactable item
    /// </summary>
    public interface IInteractable
    {
        void Interact(GameObject interactor);
    }
}

