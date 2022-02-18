using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionMesh : MonoBehaviour
{
    [SerializeField] float _detectionTime;

    public System.Action<IDetectable> onSeen;
    public System.Action<IDetectable> onDetected;

    private Dictionary<Transform, IDetectable> _cachedDetectables = new Dictionary<Transform, IDetectable>();
    private Dictionary<Transform, float> _detectedTimes = new Dictionary<Transform, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (!_cachedDetectables.ContainsKey(other.transform))
        {
            IDetectable detectable = other.GetComponent<IDetectable>();
            _cachedDetectables.Add(other.transform, detectable);
            _detectedTimes.Add(other.transform, 0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_cachedDetectables.ContainsKey(other.transform))
        {
            _detectedTimes[other.transform] += Time.deltaTime;
            if(_detectedTimes[other.transform] > _detectionTime)
            {
                onDetected?.Invoke(_cachedDetectables[other.transform]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_cachedDetectables.ContainsKey(other.transform))
        {
            _detectedTimes[other.transform] = 0f;
        }
    }
}
