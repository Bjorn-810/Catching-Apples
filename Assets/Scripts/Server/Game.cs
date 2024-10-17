using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    public TMP_Text playerDisplay;
    public TMP_Text scoreDisplay;

    private void Awake()
    {
        if(DBManager.username == null)
        {
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

    //    playerDisplay.text = "Player: " + DBManager.username;
     //   scoreDisplay.text = "Score: " + DBManager.score;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CallSaveData();
        }
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("score", DBManager.score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form))    // Release resources associated with UnityWebRequest to prevent memory leaks
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Check the response from the server
                string response = www.downloadHandler.text.Trim();

                if (response.StartsWith("0")) // Trimming because the response has a newline character at the end 0
                {
                    Debug.Log("Game saved");
                }
                    
                else
                    Debug.Log("User creation failed. Error: " + response);
            }
            else
            {
                Debug.Log("Network error: " + www.error);
            }

            DBManager.LogOut();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);  // Load the login scene

        }
    }
}
