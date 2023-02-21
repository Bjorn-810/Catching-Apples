using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum SlotState { Open, Taken }

public class FruitDrop : MonoBehaviour
{
    [SerializeField] private GameObject[] _fruits;
    private Transform _fruitContainer;

    [SerializeField] private SlotState[] _slotState;
    private Transform[] _slots;
    private Transform _slotContainer;
    
    [SerializeField] private int maxCurrentAmmount;
    private int _currentAmmount;
    
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;


    private FruitDrop slot = new FruitDrop(SlotState.Open, null);

    void Start()
    {
        _slotContainer = GameObject.Find("Slots").transform;
        _fruitContainer = GameObject.Find("fruitContainer").transform;

        _slots = new Transform[_slotContainer.childCount];
        
        _slotState = new SlotState[_slots.Length];

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = _slotContainer.GetChild(i);
        }

        StartCoroutine(DropFruit());
    }

    public FruitDrop(SlotState state, GameObject fruit) // this should be made into a new script with its own constructor
    {
        
    }
    
    private IEnumerator DropFruit()
    {
        int chosenSlot = Random.Range(0, _slots.Length);
        

        _currentAmmount = _fruitContainer.childCount;

        if (_slotState[chosenSlot] == SlotState.Open)
        {
            if (_currentAmmount <= maxCurrentAmmount)
            {
                _slotState[chosenSlot] = SlotState.Taken;

                int randomFruit = Random.Range(0, _fruits.Length);
                Instantiate(_fruits[randomFruit], _slots[chosenSlot].transform.position, _slots[chosenSlot].transform.rotation, _fruitContainer);
            }

            StartCoroutine(DropFruit());
            
            float nextSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
            yield return new WaitForSeconds(nextSpawn);
        }

        else
        {
            StartCoroutine(DropFruit());
            yield return new WaitForSeconds(0.01f);
        }
    }
}
