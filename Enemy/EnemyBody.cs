using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IInteractable
{
    public System.Action onInteract;

    private INTERACTABLE_TYPE _type = INTERACTABLE_TYPE.ENEMY_BODY;
    private InteractionData _interactionData;
    private bool _canInteract;

    private void Awake()
    {
        _interactionData = new InteractionData();
        _interactionData.type = _type;
    }

    public InteractionData GetInteractionMessage()
    {
        _interactionData.message = !_canInteract ? "" : "Bag body";
        return _interactionData;
    }

    public void Interact()
    {
        onInteract?.Invoke();
    }

    public void SetCanInteract(bool val)
    {
        _canInteract = val;
    }
}
