using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public enum AppleState { Spawning, WaitingForFall, Falling, Despawning };

    [Header("States")]
    public AppleState _appleState;

    [Header("Movement")]
    [SerializeField] private float _sideForce;

    [Header("Sizing")]
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _shrinkSpeed;

    private float _currentSize;
    private float _maxSize;

    [Header("Timing")]
    [SerializeField] private float _minFallTime;
    [SerializeField] private float _maxFallTime;
    [SerializeField] private float _despawnTime;

    private Rigidbody _rb;

    private void Start()
    {
        _maxSize = transform.localScale.x;

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
                Spawning();
                break;

            case AppleState.WaitingForFall:
                StartCoroutine(SelectingWaitTime());
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
            _appleState = AppleState.WaitingForFall;
    }

    private IEnumerator SelectingWaitTime()
    {
        float rnd = Random.Range(_minFallTime, _maxFallTime);
        yield return new WaitForSeconds(rnd);

        _appleState = AppleState.Falling; // sets state to falling
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
      //  _appleState = AppleState.Despawning;

   //    if (Physics.SphereCast(transform.position, 5f))
        {
    //           _rb.velocity = Vector3.zero;
    //            _rb.useGravity = false;
        }
    }

    private void Despawning()
    {
        _currentSize -= _shrinkSpeed * Time.deltaTime;
        transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);

        if (_currentSize <= 0) // destroy gameObject
            Destroy(gameObject);
    }
}
