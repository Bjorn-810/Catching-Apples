using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawningFruits : MonoBehaviour
{
    [SerializeField] private GameObject[] _fruits;
    
    [SerializeField] private Transform _slotContainer;
    private Transform[] _slots;

    [SerializeField] private float _timeBetweenSpawns;

    void Start()
    {
        _slotContainer = GameObject.Find("Slots").transform;
        _slots = new Transform[_slotContainer.childCount];
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = _slotContainer.GetChild(i);
        }

        StartCoroutine(DropFruit());
    }

    private IEnumerator DropFruit()
    {
        yield return new WaitForSeconds(_timeBetweenSpawns);

        int chosenSlot = -1;
        int filledSlots = 0;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].childCount > 0)
                filledSlots++;
        }

        if (filledSlots == _slots.Length) // if all slots are full the loop won't be entered
        {
            StartCoroutine(DropFruit());
            yield break;
        }

        while (chosenSlot == -1) // is finding a random slot that is empty
        {
            int trySlot = Random.Range(0, _slots.Length);

            if (_slots[trySlot].childCount == 0)
                chosenSlot = trySlot;
        }

        Instantiate(_fruits[0], _slots[chosenSlot].transform.position, _slots[chosenSlot].transform.rotation, _slots[chosenSlot]);
        StartCoroutine(DropFruit());
    }
}
