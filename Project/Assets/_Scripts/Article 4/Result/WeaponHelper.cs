using UnityEngine;

namespace Tips.Part_4_End
{

    /// <summary>
    /// Helper script to visualize the result of our WeaponPickUpInteraction by enabling the weapon object on the back of our avatar.
    /// </summary>
    public class WeaponHelper : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_weapon;

        public void ToggleWeapon(bool val)
        {
            m_weapon.SetActive(val);
        }
    }
}