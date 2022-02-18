using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster : MonoBehaviour, IInteractable
{
    [SerializeField] Door _door;
    [SerializeField] GameObject _body;
    [SerializeField] Vector3 _bodyAnimationStartPosition;
    [SerializeField] Vector3 _bodyAnimationEndPosition;
    [SerializeField] float _animationTime;

    private INTERACTABLE_TYPE _type = INTERACTABLE_TYPE.DUMPSTER;
    private InteractionData _interactionData;

    private bool _canInteract;

    private void Awake()
    {
        _body.SetActive(false);
        _interactionData = new InteractionData();
        _interactionData.type = _type;
    }

    public InteractionData GetInteractionMessage()
    {
        _interactionData.message = !_canInteract? "" : _door.IsOpen? "Hide body" : "";
        return _interactionData;
    }

    public void Interact()
    {
        if (!_canInteract) return;

        if (_door.IsOpen)
        {
            _body.transform.localPosition = _bodyAnimationStartPosition;
            _body.SetActive(true);
            _body.transform.DOLocalMove(_bodyAnimationEndPosition, _animationTime)
                .OnComplete(() => 
                { 
                    _body.SetActive(false); 
                });
        }
    }

    public void SetCanInteract(bool val)
    {
        _canInteract = val;
    }
}
