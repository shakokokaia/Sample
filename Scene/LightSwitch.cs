using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] bool _isActivated;
    [SerializeField] List<GameObject> _lights = new List<GameObject>();

    private INTERACTABLE_TYPE _type = INTERACTABLE_TYPE.LIGHT_SWITCH;
    private InteractionData _interactionData;

    private void Awake()
    {
        _interactionData = new InteractionData();
        _interactionData.type = _type;
    }

    public InteractionData GetInteractionMessage()
    {
        _interactionData.message = _isActivated ? "Turn off" : "Turn on";
        return _interactionData;
    }

    public void Interact()
    {
        _isActivated = !_isActivated;

        foreach (GameObject light in _lights) light.SetActive(_isActivated);
    }

    public void SetCanInteract(bool val)
    {

    }
}
