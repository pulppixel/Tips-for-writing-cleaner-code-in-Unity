using UnityEngine;

namespace Tips.Part_3_End
{
    /// <summary>
    /// Defines an interaction where object can be picked up and is destroyed
    /// afterwards
    /// </summary>
    public class WeaponPickUpInteraction : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject interactor)
        {
            WeaponHelper weaponHelper;
            if (weaponHelper = interactor.GetComponent<WeaponHelper>())
            {
                weaponHelper.ToggleWeapon(true);
            }
            Destroy(gameObject);
        }
    }

}