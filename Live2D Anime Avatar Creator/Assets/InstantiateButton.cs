using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InstantiateButton : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Transform location;
    [SerializeField] private GameObject scriptHolder;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private GameObject colorManager;

    public void UpdateSaves()
    {
        //UnityEngine.Debug.Log("All Saved Avatars: " + PlayerPrefs.GetString("All Saved Avatars"));
        //UnityEngine.Debug.Log("countSaves: " + PlayerPrefs.GetString("# of Avatars"));

        int countSaves = int.Parse(PlayerPrefs.GetString("# of Avatars"));
        string[] existingSavesArr = PlayerPrefs.GetString("All Saved Avatars").Split(";");

        foreach(Transform child in GameObject.Find("Save Grid Content").transform)
        {
            Destroy(child.gameObject);
        }

        if(countSaves > 0)
        {
            for(int num = 0; num < existingSavesArr.Length; num++)
            {
                if(existingSavesArr[num] != "")
                {
                    location.position = new Vector3(0, 0, 0);
                    GameObject newAvatar = Instantiate(button, location.position, location.rotation);
                    newAvatar.SetActive(true);
                    newAvatar.name = existingSavesArr[num];
                    //newAvatar.GetComponentInChildren<TextMeshProUGUI>().text = existingSavesArr[num];
                    newAvatar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = existingSavesArr[num];
                    newAvatar.transform.SetParent(GameObject.Find("Save Grid Content").transform, false);
                    UnityEngine.Debug.Log("Save displayed: " + newAvatar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text);
                }
            }
        }
    }

    public void LoadAvatar()
    {
        string saveName = EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>().text;
        //UnityEngine.Debug.Log("avatar loading: " + saveName);
        headerText.text = saveName;
        foreach(Transform classType in GameObject.Find("Animation Archive").transform)
        {
            string allSaves = PlayerPrefs.GetString(saveName + "_" + classType);
            string[] objectSaved = allSaves.Split(";");
            foreach(string myObject in objectSaved)
            {
                if(!myObject.Equals("") && !myObject.Equals(" "))
                {
                    //UnityEngine.Debug.Log("current object:" + myObject);
                    if(classType.name != "Body_Animation (always displayed)" && classType.name != "EarDefault_Animation (always displayed)"
                    && classType.name != "Tint_Indicator" && classType.name != "Highlight_Indicator" && classType.name != "Shadow_Indicator"
                    && classType.name != "Lipstick_Indicator")
                    {
                        //UnityEngine.Debug.Log(classType + " saved: " + objectSaved);
                        foreach(Transform child in classType)
                        {
                            if(child.gameObject.name.IndexOf("bone") == -1)
                            {
                                if(allSaves.Contains(child.gameObject.name.Substring(0, child.gameObject.name.IndexOf("_Anim"))) == false && child.gameObject.activeSelf == true)
                                {
                                    child.gameObject.SetActive(false);
                                }
                                if(child.gameObject.name.Equals(myObject + "_Anim") == true)
                                {
                                    child.gameObject.SetActive(true);
                                    //get color of object and set color;
                                    string[] temp = PlayerPrefs.GetString(saveName + "_" + child.gameObject.name + "_Color").Split("+");
                                    ArrayList objectColors = removeSpaces(temp);

                                    if(objectColors.Count == 1)
                                    {
                                        string[] rgbaTemp1 = objectColors[0].ToString().Split(",");
                                        ArrayList rgbaValues1 = removeSpaces(rgbaTemp1);
                                        Color color1 = toColor(rgbaValues1);
                                        //UnityEngine.Debug.Log(rgbaValues1[0] + " and " + rgbaValues1[1] + " and " + rgbaValues1[2] + " and " + rgbaValues1[3]);
                                        colorManager.GetComponent<LoadColor>().LoadColors(child.gameObject, color1, 1);
                                        //UnityEngine.Debug.Log(objectColors[0]);
                                    }
                                    else if(objectColors.Count == 2)
                                    {
                                        string[] rgbaTemp1 = objectColors[0].ToString().Split(",");
                                        ArrayList rgbaValues1 = removeSpaces(rgbaTemp1);
                                        Color color1 = toColor(rgbaValues1);
                                        colorManager.GetComponent<LoadColor>().LoadColors(child.gameObject, color1, 1);

                                        string[] rgbaTemp2 = objectColors[1].ToString().Split(",");
                                        ArrayList rgbaValues2 = removeSpaces(rgbaTemp2);
                                        Color color2 = toColor(rgbaValues2);
                                        colorManager.GetComponent<LoadColor>().LoadColors(child.gameObject, color2, 2);
                                        //UnityEngine.Debug.Log(rgbaValues1[0] + " and " + rgbaValues1[1] + " and " + rgbaValues1[2] + " and " + rgbaValues1[3]);
                                        //UnityEngine.Debug.Log(rgbaValues2[0] + " and " + rgbaValues2[1] + " and " + rgbaValues2[2] + " and " + rgbaValues2[3]);
                                        //UnityEngine.Debug.Log(objectColors[0] + " and " + objectColors[1]);
                                    }
                                    else if(objectColors.Count == 3)
                                    {
                                        //UnityEngine.Debug.Log(objectColors[0] + " and " + objectColors[1] + " and " + objectColors[2]);
                                        string[] rgbaTemp1 = objectColors[0].ToString().Split(",");
                                        ArrayList rgbaValues1 = removeSpaces(rgbaTemp1);
                                        Color color1 = toColor(rgbaValues1);
                                        colorManager.GetComponent<LoadColor>().LoadColors(child.gameObject, color1, 1);

                                        string[] rgbaTemp2 = objectColors[1].ToString().Split(",");
                                        ArrayList rgbaValues2 = removeSpaces(rgbaTemp2);
                                        Color color2 = toColor(rgbaValues2);
                                        colorManager.GetComponent<LoadColor>().LoadColors(child.gameObject, color2, 2);

                                        string[] rgbaTemp3 = objectColors[2].ToString().Split(",");
                                        ArrayList rgbaValues3= removeSpaces(rgbaTemp3);
                                        Color color3 = toColor(rgbaValues3);
                                        colorManager.GetComponent<LoadColor>().LoadColors(child.gameObject, color3, 3);
                                        //UnityEngine.Debug.Log(rgbaValues1[0] + " and " + rgbaValues1[1] + " and " + rgbaValues1[2] + " and " + rgbaValues1[3]);
                                        //UnityEngine.Debug.Log(rgbaValues2[0] + " and " + rgbaValues2[1] + " and " + rgbaValues2[2] + " and " + rgbaValues2[3]);
                                        //UnityEngine.Debug.Log(rgbaValues3[0] + " and " + rgbaValues3[1] + " and " + rgbaValues3[2] + " and " + rgbaValues3[3]);
                                    }
                                    //UnityEngine.Debug.Log("added for " + saveName + "_" + child.gameObject.name + "_Color");

                                    //UnityEngine.Debug.Log("colors found for " + saveName + "_" + child.gameObject.name + "_Color" + ": " + objectColors);
                                    //UnityEngine.Debug.Log(classType + " loaded: " + child.gameObject.name);
                                }
                            }
                        }
                    }
                    if(classType.name.Equals("Tint_Indicator") || classType.name.Equals("Highlight_Indicator") || classType.name.Equals("Shadow_Indicator")
                    || classType.name.Equals("Lipstick_Indicator"))
                    {
                        if(classType.gameObject.activeSelf.ToString().Equals(myObject) == false)
                        {
                            //UnityEngine.Debug.Log("current state: " + classType.gameObject.activeSelf.ToString() + " vs. wanted state: " + objectSaved);
                            if(classType.name.Equals("Tint_Indicator"))
                            {
                                scriptHolder.GetComponent<ButtonInstantiationScript>().DisplayTints();
                            }
                            else if(classType.name.Equals("Highlight_Indicator"))
                            {
                                scriptHolder.GetComponent<ButtonInstantiationScript>().DisplayHighlights();
                            }
                            else if(classType.name.Equals("Shadow_Indicator"))
                            {
                                scriptHolder.GetComponent<ButtonInstantiationScript>().DisplayShadows();
                            }
                            else if(classType.name.Equals("Lipstick_Indicator"))
                            {
                                scriptHolder.GetComponent<ButtonInstantiationScript>().DisplayLipstick();
                            }
                        }
                    }
                }
            }
            //UnityEngine.Debug.Log("name: " + objectSaved);
        }
    }

    public ArrayList removeSpaces(string[] temp)
    {
        ArrayList newArr = new ArrayList();
        foreach(string myObj in temp)
        {
            if(!myObj.Equals("") && !myObj.Equals(" "))
            {
                newArr.Add(myObj);
            }
        }
        return newArr;
    }  

    public Color toColor(ArrayList rgbaValues)
    {
        return new Color(float.Parse(rgbaValues[0].ToString()), 
        float.Parse(rgbaValues[1].ToString()), 
        float.Parse(rgbaValues[2].ToString()), 
        float.Parse(rgbaValues[3].ToString()));
    }
}












//PlayerPrefs.SetString("# of Avatars", "0"); 
        //PlayerPrefs.SetString("All Saved Avatars", ""); 
        //UnityEngine.Debug.Log("All Saved Avatars: " + PlayerPrefs.GetString("All Saved Avatars"));
        //UnityEngine.Debug.Log("countSaves: " + PlayerPrefs.GetString("# of Avatars"));
        /*foreach(Transform child in newAvatar.transform)
        {
            if(child.gameObject.name.Equals("Name Text"))
            {
                //child.gameObject.GetComponent<TextMeshProUGUI>().text = existingSavesArr[num];
                //UnityEngine.Debug.Log("text changed!");
                string textToChange = child.gameObject.GetComponent<TextMeshProUGUI>().text; 
                UnityEngine.Debug.Log("current text: " + textToChange);
                //string newName = existingSavesArr[num];
                //UnityEngine.Debug.Log("new text: " + newName);
            }
        }*/
        //newAvatar.GetComponentInChildren<Text>().text = existingSavesArr[num];
        //newAvatar.transform.Find("Name Text").GetComponent<Text>().text = existingSavesArr[num];
