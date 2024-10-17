using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public TMP_Text errorField;

    public int MaxNameLength;
    public int MinPasswordLength;

    public string SceneToLoad;

    public Button submitButton;

    public void VerifyInputs()
    {
        submitButton.interactable = nameField.text.Length <= MaxNameLength && passwordField.text.Length >= MinPasswordLength;
    }

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    private IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))   // Release resources associated with UnityWebRequest to prevent memory leaks
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text.Trim();

                if (response.StartsWith("0"))     // Trimming because the response has a newline character at the end 0 
                {
                    DBManager.username = nameField.text;

                    // Extract the score from the response
                    if (int.TryParse(response.Split('\t')[1], out int score))
                    {
                        DBManager.score = score;

                        // Load the next scene if the scene switcher is found
                        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();

                        if (sceneSwitcher != null)
                            sceneSwitcher.LoadSceneByName(SceneToLoad);

                        else
                        {
                            Debug.LogWarning("SceneSwitcher not found.");
                        }

                    }

                    else
                    {
                        errorField.text = "Failed to parse score from response: " + response;
                        Debug.Log("Failed to parse score from response: " + response);
                    }
                }

                else
                {
                    errorField.text = "User login failed. Error #" + www.downloadHandler.text;
                    Debug.Log("User login failed. Error #" + www.downloadHandler.text);
                }

            }

            else
            {
                errorField.text = "Network error: " + www.error;
                Debug.Log("Network error: " + www.error);
                yield return null;
            }
        }
    }
}
