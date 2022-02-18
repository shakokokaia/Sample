using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text _currentAmmo;
    [SerializeField] TMP_Text _visibility;
    [SerializeField] TMP_Text _interaction;

    public void UpdateInteractionText(string value)
    {
        _interaction.text = value;
    }

    public void UpdateCurrentAmmo(int currentAmmo, int maxAmmo)
    {
        _currentAmmo.text = $"{currentAmmo} / {maxAmmo}";
    }
}
