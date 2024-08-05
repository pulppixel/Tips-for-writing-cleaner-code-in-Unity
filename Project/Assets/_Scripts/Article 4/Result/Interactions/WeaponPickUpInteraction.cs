using UnityEngine;

namespace Tips.Part_4_End
{
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