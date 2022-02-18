using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum INTERACTABLE_TYPE { DOOR, LIGHT_SWITCH, DUMPSTER, OTHER, ENEMY_BODY}

public interface IInteractable
{
    public abstract InteractionData GetInteractionMessage();

    public abstract void Interact();

    public abstract void SetCanInteract(bool val);
}

public class InteractionData
{
    public INTERACTABLE_TYPE type;
    public string message;
}
