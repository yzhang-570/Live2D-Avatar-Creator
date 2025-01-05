using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteSave : MonoBehaviour
{
    public void DeleteSelectedSave()
    {
        UnityEngine.Debug.Log("All Saved Avatars: " + PlayerPrefs.GetString("All Saved Avatars"));
        UnityEngine.Debug.Log("countSaves: " + PlayerPrefs.GetString("# of Avatars"));
        UnityEngine.Debug.Log("Save removed: " + EventSystem.current.currentSelectedGameObject.transform.parent.name);
        string saveToRemove = EventSystem.current.currentSelectedGameObject.transform.parent.name;

        int countSaves = int.Parse(PlayerPrefs.GetString("# of Avatars"));
        countSaves = countSaves - 1;
        PlayerPrefs.SetString("# of Avatars", countSaves.ToString());

        string existingSaves = PlayerPrefs.GetString("All Saved Avatars");
        existingSaves = existingSaves.Substring(0, existingSaves.IndexOf(saveToRemove)) + existingSaves.Substring(existingSaves.IndexOf(saveToRemove) + saveToRemove.Length + 1);
        PlayerPrefs.SetString("All Saved Avatars", existingSaves);

        UnityEngine.Debug.Log("All Saved Avatars: " + PlayerPrefs.GetString("All Saved Avatars"));
        UnityEngine.Debug.Log("countSaves: " + PlayerPrefs.GetString("# of Avatars"));
    }
}
