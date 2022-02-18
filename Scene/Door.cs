using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] Vector3 _openRotation;
    [SerializeField] Vector3 _closedRotation;
    [SerializeField] bool _isOpen;
    [SerializeField] float _animationTime;

    private bool _isAnimating;
    private INTERACTABLE_TYPE _type = INTERACTABLE_TYPE.DOOR;
    private InteractionData _interactionData;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        _interactionData = new InteractionData();
        _interactionData.type = _type;
    }

    public InteractionData GetInteractionMessage()
    {
        _interactionData.message = _isAnimating ? "" : _isOpen ? "Close" : "Open";
        return _interactionData;
    }

    public void Interact()
    {
        if (_isAnimating) return;

        _isAnimating = true;
        _isOpen = !_isOpen;
        transform.DOLocalRotate(_isOpen ? _openRotation : _closedRotation, _animationTime)
            .OnComplete(() => 
            {
                _isAnimating = false;
            });
    }

    public void SetCanInteract(bool val)
    {

    }
}
