using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frenzy/Playr/Gun")]
public class GunData : ScriptableObject
{
    [SerializeField] string _gunID;
    [SerializeField] int _clipSize;
    [SerializeField] float _damage;
    [SerializeField] List<AudioClip> _shootingSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> _reloadingSounds = new List<AudioClip>();

    public int ClipSize => _clipSize;
    public string GunID => _gunID;
    public List<AudioClip> ShootingSounds => _shootingSounds;
    public List<AudioClip> ReloadingSounds => _reloadingSounds;
    public float Damage => _damage;

}
