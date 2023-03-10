using System.Collections;
using UnityEngine;

public class ClipConfig : ScriptableObject
{
    public int AvailableAmmo;

    public int ClipSize;
    public int CurrentClipAmmo;

    public float ReloadTime = 1.5f;
    public float UnloadTime = 0.5f;
    
    private IEnumerator Reload()
    {
        Debug.Log("Reloading clip...");

        int availableClipSize = ClipSize - CurrentClipAmmo;
        int maxReloadAmount = Mathf.Min(availableClipSize, AvailableAmmo);

        CurrentClipAmmo += maxReloadAmount;

        yield return new WaitForSeconds(ReloadTime);
    }
    
    private IEnumerator EmtyClip()
    {
        Debug.Log("Emtying clip...");

        yield return new WaitForSeconds(UnloadTime);
        CurrentClipAmmo = 0;
    }

    private void SwapAmmo()
    {
        
    }
}
