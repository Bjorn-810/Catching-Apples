using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public TMP_Text _scoreText;
    public float _collectTime = 0.2f;

    private void Start()
    {
        _scoreText.text = DBManager.score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            StartCoroutine(Collect(other));
        }
    }

    IEnumerator Collect(Collider collision)
    { 
        yield return new WaitForSeconds(_collectTime);

        Destroy(collision.gameObject);
        DBManager.score++;
        _scoreText.text = DBManager.score.ToString();
    }
}
