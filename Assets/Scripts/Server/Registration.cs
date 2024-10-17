using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Registration : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public TMP_Text errorField;

    public int MaxNameLength;
    public int MinPasswordLength;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form)) // Release resources associated with UnityWebRequest to prevent memory leaks
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Check the response from the server
                string response = www.downloadHandler.text.Trim();

                if (response.StartsWith("0"))
                {
                    Debug.Log("User created successfully");
                    FindObjectOfType<Login>().CallLogin();
                }

                else
                {
                    errorField.text = "User creation failed. Error: " + response;
                    Debug.Log("User creation failed. Error: " + response);
                }

            }
            else
            {
                errorField.text = "Network error: " + www.error;
                Debug.Log("Network error: " + www.error);
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = nameField.text.Length <= MaxNameLength && passwordField.text.Length >= MinPasswordLength;
    }
}
