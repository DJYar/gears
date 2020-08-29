using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Belt : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0.01f, 60.0f)] private Vector2 _spawnTime = Vector2.one;
    [SerializeField, Range(0.0f, 100.0f)] private float _speed = 1.0f;
    [SerializeField] private GameObject _gearPrefab;
    [SerializeField] private GearRemoveTrigger _removeTrigger;

    public event Action GearRemove;
    
    /// <summary>
    /// This coeff we need to fix different UV and object move
    /// </summary>
    private const float K = 1.3333333333333333333333333333f;
    
    private Material _material;
    private float _offset;
    
    private void Start()
    {
        enabled = false;
        _removeTrigger.Trigger += OnGearRemove;
        
        this.Delay(() =>
        {
            var image = GetComponent<Image>();
            image.material = _material = new Material(image.material);
        });
    }

    private void OnGearRemove()
    {
        GearRemove?.Invoke();
    }

    private IEnumerator GearGenerator()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(Random.Range(_spawnTime.x, _spawnTime.y));
            var newGear = Instantiate(_gearPrefab, transform).GetComponent<Gear>();
            newGear.Color = Palette.Instance.Random();
        }
    }

    void Update()
    {
        if (!enabled) return;

        var delta = _speed * Time.deltaTime;
        _offset += delta;
        _material.mainTextureOffset = new Vector2(0, _offset);

        var gears = GetComponentsInChildren<Gear>();
        foreach (var gear in gears)
            gear.SetPosition(delta, K);
    }

    public void Stop()
    {
        enabled = false;
        var gears = GetComponentsInChildren<Gear>();
        foreach (var gear in gears)
            gear.enabled = false;
    }

    public void Restart()
    {
        var gears = GetComponentsInChildren<Gear>();
        foreach (var gear in gears)
            Destroy(gear.gameObject);

        enabled = true;
        StartCoroutine(GearGenerator());
    }
}
