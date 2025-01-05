using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveAvatar : MonoBehaviour
{
    
    [SerializeField] private GameObject UserInput;
    private GameObject errorMsg;
    private GameObject successMsg;
    private GameObject gameMenu;
    private GameObject saveMenu;
    [SerializeField] private GameObject homeScreen;
    
    void Start()
    {
        gameMenu = GameObject.Find("Game Screen");
        saveMenu = GameObject.Find("Save Menu");
        errorMsg = GameObject.Find("Rename Message");
        errorMsg.SetActive(false);
        successMsg = GameObject.Find("Saved Message");
        successMsg.SetActive(false);
    }

    public void createSave()
    {
        //loading data
        int countSaves = 0;
        if(PlayerPrefs.HasKey("# of Avatars"))
        {
            countSaves = int.Parse(PlayerPrefs.GetString("# of Avatars"));
        }
        else
        {
            PlayerPrefs.SetString("# of Avatars", countSaves.ToString());
        }

        string existingSaves = "";
        if(PlayerPrefs.HasKey("All Saved Avatars"))
        {
            existingSaves = PlayerPrefs.GetString("All Saved Avatars");
        }
        else
        {
            PlayerPrefs.SetString("All Saved Avatars", existingSaves);
        }
        
        //saving new data
        //saves name inputted by player
        string saveName = UserInput.GetComponent<TMP_InputField>().text;
        string[] temp = existingSaves.Split(";");
        UnityEngine.Debug.Log("existingSaves: " + existingSaves);
        bool duplicate = false;
        string dupName = "";
        int spaceCount = 0;
        foreach(string name in temp)
        {
            if(saveName.Equals(name) || saveName.Equals("Untitled Avatar"))
            {
                duplicate = true;
                //UnityEngine.Debug.Log("duplicate found");
                dupName = name;
            }
            if(name.Equals(""))
            {
                spaceCount++;
            }
        }

        int i = 0;
        string[] existingSavesArr = new string[temp.Length - spaceCount];
        foreach(string name in temp)
        {
            if(!name.Equals(""))
            {
                existingSavesArr[i] = name;
                //UnityEngine.Debug.Log("save entry: " + existingSavesArr[i]);
                i++;
            }
        }

        //existingSaves.IndexOf(saveName) != -1 || string.IsNullOrEmpty(saveName)
        if(duplicate == true /*or is an empty string*/)
        {
            errorMsg.SetActive(true);
        }
        else
        {
            errorMsg.SetActive(false);
            //update existing saves
            countSaves += 1;
            PlayerPrefs.SetString("# of Avatars", countSaves.ToString());
            existingSaves = existingSaves + saveName + ";";
            PlayerPrefs.SetString("All Saved Avatars", existingSaves);
            //saves name of avatar to a number
            PlayerPrefs.SetString(countSaves.ToString(), saveName);
            successMsg.SetActive(true);
            StartCoroutine(DisplayWait());
            UserInput.GetComponent<TMP_InputField>().text = "";

            //the name of each object can be gotten from PlayerPrefs with the format: saveName_className
            //if object is active, save to PlayerPrefs
            GameObject animationArchive = GameObject.Find("Animation Archive");
            foreach(Transform classType in animationArchive.transform)
            {
                string objectSaveName = saveName + "_" + classType;
                if(classType.name != "Body_Animation (always displayed)" && classType.name != "EarDefault_Animation (always displayed)"
                && classType.name != "Tint_Indicator" && classType.name != "Highlight_Indicator" && classType.name != "Shadow_Indicator"
                && classType.name != "Lipstick_Indicator")
                {
                    bool activeFound = false;
                    string saves = "";
                    foreach(Transform classObject in classType)
                    {
                        string colorSaveName = saveName + "_" + classObject.name + "_Color";
                        string classObjectColor = "";
                        if(classObject.gameObject.activeSelf == true && classObject.gameObject.name.IndexOf("bone") == -1)
                        {
                            activeFound = true;
                            saves += classObject.name.Substring(0, classObject.name.IndexOf("_Anim")) + ";";
                            if(classObject.childCount > 0)
                            {
                                string color1 = "";
                                string color2 = "";
                                string color3 = "";
                                foreach(Transform child in classObject)
                                {
                                    //objectcolors;
                                    //c1 + c2 + c3;
                                    //r1, g1, b1, a1, + r2, g2, b2, a2, + r3, g3, b3, a3;
                                    string partName = child.gameObject.name;
                                    if(partName.IndexOf("bone") == -1)
                                    {
                                        if(partName.IndexOf("Color1") != -1)
                                        {
                                            color1 = child.gameObject.GetComponent<SpriteRenderer>().color.r + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.g + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.b + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.a + ",+";
                                        }
                                        else if(partName.IndexOf("Color2") != -1)
                                        {
                                            color2 = child.gameObject.GetComponent<SpriteRenderer>().color.r + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.g + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.b + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.a + ",+";
                                        }
                                        else if(partName.IndexOf("Color3") != -1)
                                        {
                                            color3 = child.gameObject.GetComponent<SpriteRenderer>().color.r + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.g + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.b + "," +
                                            child.gameObject.GetComponent<SpriteRenderer>().color.a + ",+";
                                        }
                                    }
                                }
                                classObjectColor = color1 + color2 + color3;
                                PlayerPrefs.SetString(colorSaveName, classObjectColor);
                                //UnityEngine.Debug.Log(classObject.name + " color: " + PlayerPrefs.GetString(colorSaveName));
                            }
                            else
                            {
                                if(classObject.gameObject.GetComponent<SpriteRenderer>() != null)
                                {
                                    //get color and save
                                    string colorValues = classObject.gameObject.GetComponent<SpriteRenderer>().color.r + "," +
                                    classObject.gameObject.GetComponent<SpriteRenderer>().color.g + "," +
                                    classObject.gameObject.GetComponent<SpriteRenderer>().color.b + "," +
                                    classObject.gameObject.GetComponent<SpriteRenderer>().color.a + ",+";
                                    PlayerPrefs.SetString(colorSaveName, colorValues);
                                    //UnityEngine.Debug.Log(classObject.name + " color: " + PlayerPrefs.GetString(colorSaveName));
                                }
                            }
                        }
                    }
                    if(activeFound == false)
                    {
                        PlayerPrefs.SetString(objectSaveName, "EMPTY");
                        UnityEngine.Debug.Log(classType.gameObject.name.Substring(0, classType.gameObject.name.IndexOf("_Animation")) + " saved: EMPTY");
                    }
                    else
                    {
                        PlayerPrefs.SetString(objectSaveName, saves);
                        UnityEngine.Debug.Log(classType.gameObject.name.Substring(0, classType.gameObject.name.IndexOf("_Animation")) + " saved: " + saves);
                    }
                }
                if(classType.name.Equals("Tint_Indicator") || classType.name.Equals("Highlight_Indicator") || classType.name.Equals("Shadow_Indicator")
                ||classType.name.Equals("Lipstick_Indicator"))
                {
                    if(classType.gameObject.activeSelf == true)
                    {
                        PlayerPrefs.SetString(objectSaveName, "True");
                        //UnityEngine.Debug.Log(classType.name + " mode saved: True");
                    }
                    else
                    {
                        PlayerPrefs.SetString(objectSaveName, "False");
                        //UnityEngine.Debug.Log(classType.name + " mode saved: False");
                    }
                }
            }
        }
    }

    IEnumerator DisplayWait()
    {
        yield return new WaitForSeconds(1);
        successMsg.SetActive(false);
        saveMenu.SetActive(false);
        gameMenu.SetActive(false);
        homeScreen.SetActive(true);
    }
}













/*string[] classes = {"Eyes", "Eyebrows"};
            foreach(string className in classes)
            {
                GameObject part = null;
                string objectSaveName = saveName + "_" + className;
                //if active and NOT a bone
                if(GameObject.Find(className + " Holder").transform.childCount > 0)
                {
                    part = GameObject.Find(className + " Holder").transform.GetChild(0).gameObject;
                    GameObject.Destroy(part);
                    //save
                    PlayerPrefs.SetString(objectSaveName, part.name);
                }
                //if nothing is active or nothing is active except bone
                else
                {
                    PlayerPrefs.SetString(objectSaveName, "EMPTY");
                }
                //UnityEngine.Debug.Log(objectSaveName + ": " + PlayerPrefs.GetString(objectSaveName));
            }*/
/*
foreach(string className in classes)
{
    GameObject part = null;
    string objectSaveName = saveName + "_" + className;
    //if active and NOT a bone
    if(GameObject.Find(className + " Holder").transform.childCount > 0)
    {
        part = GameObject.Find(className + " Holder").transform.GetChild(0).gameObject;
        GameObject.Destroy(part);
        //save
        PlayerPrefs.SetString(objectSaveName, part.name);
    }
    //if nothing is active or nothing is active except bone
    else
    {
        PlayerPrefs.SetString(objectSaveName, "EMPTY");
    }
    //UnityEngine.Debug.Log(objectSaveName + ": " + PlayerPrefs.GetString(objectSaveName));
}


new string[existingSaves.Length + 1];
string temp = existingSaves + "";
int arrCount = 0;
for(int i = 0; i < temp.Length; i++)
{
    if(temp[i].Equals(";"))
    {
        //this doesn't even show up lol
        UnityEngine.Debug.Log("name to add: " + temp.Substring(0, i));
        existingSavesArr[arrCount] = temp.Substring(0, i);
        temp = temp.Substring(i + 1);
        arrCount++;
    }
}*/
