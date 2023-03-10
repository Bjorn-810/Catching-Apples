//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.Serialization;
//using Unity.VisualScripting.Antlr3.Runtime.Misc;
//using UnityEngine;


//public class FruitDrop : MonoBehaviour
//{
//    [SerializeField] private GameObject[] _fruits;
//    private Transform _fruitContainer;

//    public Transform test;
    
//    private Transform[] _slots;
//    private Transform _slotContainer;

//    [SerializeField] private int maxCurrentAmmount;
//    private int _currentAmmount;

//    [SerializeField] private float _minSpawnTime;
//    [SerializeField] private float _maxSpawnTime;


//    public Slot[] fruitSlot;
//    void Start()
//    {
        

//        //_slotContainer = GameObject.Find("Slots").transform;
//        //_fruitContainer = GameObject.Find("fruitContainer").transform;

//        //_slots = new Transform[_slotContainer.childCount];

//        //_slotState = new SlotState[_slots.Length];

//        //for (int i = 0; i < _slots.Length; i++)
//        //{
//        //    _slots[i] = _slotContainer.GetChild(i);
//        //}

//        //StartCoroutine(DropFruit());
//    }

//    private void Update()
//    {
//     //   fruitSlot[0] = new Slot(Slot.SlotState.Open, _slots[0]);

//     //   for (int i = 0; i < fruitSlot.Length; i++)
//    //    {
//     //      if (fruitSlot[i]._slotState == Slot.SlotState.Open)
//            {
//       //         Debug.Log("Slot " + i + "is open");
//            }

//       //     else
//         //       Debug.Log("Slot " + i + "is closed");
//        }



//    }

//    private IEnumerator DropFruit()
//    {
//        //    int chosenSlot = Random.Range(0, _slots.Length);


//        //    _currentAmmount = _fruitContainer.childCount;

//        //    if (_slotState[chosenSlot] == SlotState.Open)
//        //    {
//        //        if (_currentAmmount <= maxCurrentAmmount)
//        //        {
//        //            _slotState[chosenSlot] = SlotState.Taken;

//        //            int randomFruit = Random.Range(0, _fruits.Length);
//        //            Instantiate(_fruits[randomFruit], _slots[chosenSlot].transform.position, _slots[chosenSlot].transform.rotation, _fruitContainer);
//        //        }

//        //        StartCoroutine(DropFruit());

//        //        float nextSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
//        //        yield return new WaitForSeconds(nextSpawn);
//        //    }

//        //    else
//        //    {
//        //        StartCoroutine(DropFruit());
//        yield return new WaitForSeconds(0.01f);
//        //    }
//    }
//}
