using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public enum AppleState { Spawning, WaitingForFall, Falling, Jiggling, Despawning };

    [Header("States")]
    public AppleState _appleState;

    [Header("Movement")]
    [SerializeField] private float _sideForce;
    [SerializeField] private float _jiggleDuration;
    [SerializeField] private float _jiggleRange;

    [Header("Sizing")]
    [SerializeField] private float _growSpeed;

    private float _currentSize;
    private float _maxSize;

    [Header("Timing")]
    [SerializeField] private float _minFallTime;
    [SerializeField] private float _maxFallTime;
    [SerializeField] private float _despawnTime;

    private float elapsedTime;
    private Vector3 _originalPosition;

    private bool doOnce = false;
    private bool hasGrown = false;

    private Rigidbody _rb;

    private void Start()
    {
        _maxSize = transform.localScale.x;

        _originalPosition = transform.position;
        transform.localScale = new Vector3(0, 0, 0); //set fruit size to 0 when it gets instantiated

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleStates();
    }

    private void HandleStates()
    {
        switch (_appleState)
        {
            case AppleState.Spawning:
                if (hasGrown == false)
                    Spawning();
                break;

            case AppleState.WaitingForFall:
                // StartCoroutine(SelectingWaitTime());
                break;

            case AppleState.Jiggling:
                Jiggling();
                break;

            case AppleState.Falling:
                StartCoroutine(Falling());
                break;

            case AppleState.Despawning:
                Despawning();
                break;
        }
    }

    private void Spawning()
    {
        // scales up the size of the fruit
        _currentSize += _growSpeed * Time.deltaTime;
        transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);

        // changes state when reached max size
        if (_currentSize >= _maxSize)
        {
            if (doOnce == false)
            {
                StartCoroutine(SelectingWaitTime());
                hasGrown = true;
                doOnce = true;
            }

            doOnce = true;
        }

    }

    private void Jiggling()
    {
        if (elapsedTime < _jiggleDuration)
        {
            // Calculate random offset
            Vector3 randomOffset = _originalPosition + Random.insideUnitSphere * _jiggleRange;

            // Lerp towards the random offset
            float t = elapsedTime / _jiggleDuration;
            transform.position = Vector3.Lerp(_originalPosition, randomOffset, t);

            // Increase elapsed time
            elapsedTime += Time.deltaTime;
        }

        else
        {
            // Reset position after jiggle
            transform.position = _originalPosition;
            _appleState = AppleState.Falling;
        }
    }

    private IEnumerator SelectingWaitTime()
    {
        float rnd = Random.Range(_minFallTime, _maxFallTime);
        yield return new WaitForSeconds(rnd);

        _appleState = AppleState.Jiggling;
    }

    private IEnumerator Falling()
    {
        if (!_rb.useGravity)
        {
            Vector3 randomVelocity = new(Random.Range(-_sideForce, _sideForce), 0, Random.Range(-_sideForce, _sideForce));
            _rb.velocity = randomVelocity;
            _rb.useGravity = true;
        }

        yield return new WaitForSeconds(_despawnTime); // waits for an ammount of time since the drop has started
        _appleState = AppleState.Despawning;
    }

    private void Despawning()
    {
        _currentSize -= _growSpeed * Time.deltaTime;
        transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);

        if (_currentSize <= 0) // destroy gameObject
            Destroy(gameObject);
    }
}
