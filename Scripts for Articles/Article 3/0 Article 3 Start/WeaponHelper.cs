using UnityEngine;

public class WeaponHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject m_weapon;

    public void ToggleWeapon(bool val)
    {
        m_weapon.SetActive(val);
    }
}
