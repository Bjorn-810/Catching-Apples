using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    public TMP_Text highscoreText;

    void Start()
    {
        StartCoroutine(PostScores());
    }

    IEnumerator PostScores()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/sqlconnect/highscores.php");
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Parse the received data and store it in an array
            string[] webResults = request.downloadHandler.text.Split('\n');

            // Assuming your highscoreText is a UI Text component
            highscoreText.text = "\n";
            for (int i = 0; i < webResults.Length; i++)
            {
                string[] values = webResults[i].Split('\t');
                if (values.Length >= 2)
                {
                    highscoreText.text += values[0] + ": " + values[1] + "\n";
                }
            }
        }
    }
}
