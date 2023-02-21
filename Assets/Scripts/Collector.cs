using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public Text _scoreText;
    public int _score = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            _score++;
            _scoreText.text = "Apples collected: " + _score;
        }
    }
}
