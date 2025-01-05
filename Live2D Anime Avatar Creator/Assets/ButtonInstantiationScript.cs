using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonInstantiationScript : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Transform location;
    [SerializeField] private TextMeshProUGUI headerText;
    //[SerializeField] private GameObject adsInitializer;
    
    void Start()
    {
        
        //setting up references to animations
        //eyeAnimation = GameObject.Find("Eye_Animation");
        //eyebrowAnimation = GameObject.Find("Eyebrow_Animation");

        //supposed to disable ad button at start but currently doesn't work
        //adsInitializer.SetActive(false);
        UnityEngine.Debug.Log("ad is disabled");

        //setting up buttons
        string[] classes = {"Eye", "Eyebrow", "Blush", "FrontHair", "Hair", "BackHair", "Mouth",
        "Nose", "Faces", "Ears", "FaceAcc", "Earrings", "Acc", "HeadAcc", "Back", "Clothes", "Jackets", "Hand", "Wallpaper"};
        foreach(string className in classes)
        {
            if(!className.Equals("Wallpaper") && !className.Equals("Ears"))
            {
                GameObject animationArchive = GameObject.Find(className + "_Animation");
                foreach(Transform child in animationArchive.transform)
                {
                    if(child.name.IndexOf("bone") == -1 && child.name.IndexOf("EmptyDefault") == -1)
                    {
                        //UnityEngine.Debug.Log(child.gameObject.name);
                        GameObject newButton = Instantiate(button, location.position, location.rotation);
                        newButton.SetActive(true);
                        string objectName = child.gameObject.name.Substring(0, child.gameObject.name.IndexOf("_Anim"));
                        newButton.name = objectName + " Button";
                        //UnityEngine.Debug.Log(objectName + "_PNG");
                        newButton.GetComponent<Image>().sprite = GameObject.Find(objectName + "_PNG").GetComponent<SpriteRenderer>().sprite;
                        newButton.transform.SetParent(GameObject.Find(className + " Grid Content").transform, false);
                    }
                }
            }
            //animationArchive.SetActive(false);
            if(className + " Selection" != "Faces Selection")
            {
                GameObject.Find(className + " Selection").SetActive(false);
            }
            //GameObject.Find("Wallpaper Selection").SetActive(false);
        }
        GameObject.Find("Color Selection - 1 Color").SetActive(false);
        GameObject.Find("Color Selection - 2 Colors").SetActive(false);
        GameObject.Find("Color Selection - 3 Colors").SetActive(false);
    }

    //destroy existing objects in holder (DONE)
    //Eye_Smile Button - button pressed (DONE)
    //Eye_Animation - duplicate overall and set parent to (DONE)
    //Eye_Smile_Anim - delete all but bones + specific object (DONE)
    public void DisplayAnimation()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        UnityEngine.Debug.Log(buttonName + " pressed");
        string objectCategory = buttonName.Substring(0, buttonName.IndexOf("_"));
        string objectName = buttonName.Substring(0, buttonName.IndexOf(" Button"));

        //finding animation archive for specific category
        GameObject objectAnimationArchive = GameObject.Find("Animation Archive");
        foreach(Transform child in GameObject.Find("Animation Archive").transform)
        {
            if(child.name == objectCategory + "_Animation")
            {
                objectAnimationArchive = child.gameObject;
            }
        }
        objectAnimationArchive.SetActive(true);

        //control which objects are displayed depending on category
        if(objectAnimationArchive.name.Equals("Acc_Animation") || objectAnimationArchive.name.Equals("FaceAcc_Animation")
        || objectAnimationArchive.name.Equals("HeadAcc_Animation"))
        {
            foreach(Transform child in objectAnimationArchive.transform)
            {
                if(child.name.Equals(objectName + "_Anim") == true && child.gameObject.activeSelf == false)
                {
                    child.gameObject.SetActive(true);
                }
                else if(child.name.Equals(objectName + "_Anim") == true && child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else if(objectAnimationArchive.name.Equals("Jackets_Animation") || objectAnimationArchive.name.Equals("Back_Animation")
        || objectAnimationArchive.name.Equals("BackHair_Animation") || objectAnimationArchive.name.Equals("Hand_Animation")
        || objectAnimationArchive.name.Equals("Blush_Animation") || objectAnimationArchive.name.Equals("Earrings_Animation")
        || objectAnimationArchive.name.Equals("Wallpaper_Animation"))
        {
            foreach(Transform child in objectAnimationArchive.transform)
            {
                //UnityEngine.Debug.Log("looking for: " + objectName + "_Anim; " + "current object: " + child.name);
                if(child.name.Equals(objectName + "_Anim") == false && child.name.IndexOf("bone") == -1
                || child.name.Equals(objectName + "_Anim") == true && child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                }
                else if(child.name.Equals(objectName + "_Anim") == true && child.gameObject.activeSelf == false)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            foreach(Transform child in objectAnimationArchive.transform)
            {
                if(child.name.Equals(objectName + "_Anim") == false && child.name.IndexOf("bone") == -1)
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void DefaultAvatar()
    {
        headerText.text = "Untitled Avatar";
        GameObject animationArchive = GameObject.Find("Animation Archive");
        ResetColor();
        foreach(Transform classType in animationArchive.transform)
        {
            if(classType.name != "Body_Animation (always displayed)" && classType.name != "EarDefault_Animation (always displayed)"
            && classType.name != "Tint_Indicator" && classType.name != "Highlight_Indicator" && classType.name != "Shadow_Indicator"
            && classType.name != "Lipstick_Indicator")
            foreach(Transform child in classType)
            {
                child.gameObject.SetActive(false);
            }
            if(classType.name != "Body_Animation (always displayed)" && classType.name != "EarDefault_Animation (always displayed)"
            && classType.name != "Acc_Animation" && classType.name != "FaceAcc_Animation" && classType.name != "HeadAcc_Animation"
            && classType.name != "Jackets_Animation" && classType.name != "Back_Animation" && classType.name != "BackHair_Animation"
            && classType.name != "Hand_Animation" && classType.name != "Blush_Animation" && classType.name != "Earrings_Animation"
            && classType.name != "Tint_Indicator" && classType.name != "Highlight_Indicator" && classType.name != "Shadow_Indicator"
            && classType.name != "Lipstick_Indicator")
            {
                //UnityEngine.Debug.Log("passed");
                //classType.gameObject.SetActive(true);
                GameObject defaultObject = classType.GetChild(0).gameObject;
                defaultObject.SetActive(true);
            }
            if(classType.name.Equals("Tint_Indicator") && classType.gameObject.activeSelf == false)
            {
                DisplayTints();
            }
            if(classType.name.Equals("Highlight_Indicator") && classType.gameObject.activeSelf == false)
            {
                DisplayHighlights();
            }
            if(classType.name.Equals("Shadow_Indicator") && classType.gameObject.activeSelf == true)
            {
                DisplayShadows();
            }
            if(classType.name.Equals("Lipstick_Indicator") && classType.gameObject.activeSelf == true)
            {
                DisplayLipstick();
            }
        }
    }

    public void ResetColor()
    {
        GameObject animationArchive = GameObject.Find("Animation Archive");
        foreach(Transform classType in animationArchive.transform)
        {
            if(classType.name != "Tint_Indicator" && classType.name != "Highlight_Indicator" && classType.name != "Shadow_Indicator"
            && classType.name != "Lipstick_Indicator" && classType.name != "Lipstick_Indicator")
            {
                foreach(Transform classObject in classType)
                {
                    if(classObject.gameObject.GetComponent<SpriteRenderer>() != null)
                    {
                        classObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                        /*if(classObject.gameObject.name.Equals("Wallpaper_Basic_Anim"))
                        {
                            classObject.gameObject.GetComponent<SpriteRenderer>().color = new Color((float)0.7, (float)0.5, (float)0.7, 1);
                        }*/
                        if(classObject.gameObject.name.IndexOf("Tint") == -1 && classObject.gameObject.name.IndexOf("bone") == -1 &&
                        (classType.name.Equals("Faces_Animation") || classType.name.Equals("EarDefault_Animation (always displayed)")
                        || classType.name.Equals("Hand_Animation") || classType.name.Equals("Body_Animation (always displayed)")))
                        {
                            //UnityEngine.Debug.Log("skin color set for " + classType.name);
                            classObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, (float)0.9490196, (float)0.9215686, 1);
                        }
                    }
                    if(classObject.childCount > 0)
                    {
                        //UnityEngine.Debug.Log("traversed " + classType);
                        foreach(Transform child in classObject)
                        {
                            if(child.gameObject.GetComponent<SpriteRenderer>() != null && child.gameObject.name.IndexOf("bone") == -1
                            && child.gameObject.name.IndexOf("Tint") == -1)
                            {
                                if(classType.name.Equals("Faces_Animation") || classType.name.Equals("EarDefault_Animation (always displayed)")
                                || classType.name.Equals("Hand_Animation") || classType.name.Equals("Ears_Animation") || classType.name.Equals("Body_Animation (always displayed)"))
                                {
                                    //UnityEngine.Debug.Log("skin color set for " + classType.name);
                                    child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, (float)0.9490196, (float)0.9215686, 1);
                                }
                                else if(child.gameObject.name.Equals("Eyebrow_Flat1") || child.gameObject.name.Equals("Eye_Normal_Wrinkle_Tint"))
                                {
                                    child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)0.3137255);
                                }
                                else if(classType.name.Equals("Eye_Animation") && child.gameObject.name.IndexOf("_Frame") != -1)
                                {
                                    child.gameObject.GetComponent<SpriteRenderer>().color = new Color((float)0.7924528, (float)0.5192549, (float)0.3326806, 1);
                                }
                                else
                                {
                                    child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void DisplayTints()
    {
        GameObject animationArchive = GameObject.Find("Animation Archive");
        foreach(Transform classType in animationArchive.transform)
        {
            foreach(Transform child in classType)
            {
                if(child.name.IndexOf("Tint") != -1 && child.gameObject.activeSelf == false)
                {
                    child.gameObject.SetActive(true);
                }
                else if(child.name.IndexOf("Tint") != -1 && child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                }
                
                if(child.transform.childCount > 0)
                {
                    foreach(Transform secondChild in child)
                    {
                        if(secondChild.name.IndexOf("Tint") != -1 && secondChild.gameObject.activeSelf == false)
                        {
                            secondChild.gameObject.SetActive(true);
                        }
                        else if(secondChild.name.IndexOf("Tint") != -1 && secondChild.gameObject.activeSelf == true)
                        {
                            secondChild.gameObject.SetActive(false);
                        }
                    }
                }
            }
            if(classType.gameObject.name.Equals("Tint_Indicator") && classType.gameObject.activeSelf == false)
            {
                classType.gameObject.SetActive(true);
            }
            else if(classType.gameObject.name.Equals("Tint_Indicator") && classType.gameObject.activeSelf == true)
            {
                classType.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayShadows()
    {
        GameObject animationArchive = GameObject.Find("Eye_Animation");
        foreach(Transform ogChild in animationArchive.transform)
        {
            foreach(Transform child in ogChild)
            {
                UnityEngine.Debug.Log("looking for shadow");
                if(child.name.IndexOf("Shadow") != -1 && child.gameObject.activeSelf == false)
                {
                    child.gameObject.SetActive(true);
                    UnityEngine.Debug.Log("shadows active");
                }
                else if(child.name.IndexOf("Shadow") != -1 && child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                    UnityEngine.Debug.Log("shadows deactive");
                }
            }
        }
        animationArchive = GameObject.Find("Animation Archive");
        foreach(Transform classType in animationArchive.transform)
        {
            if(classType.gameObject.name.Equals("Shadow_Indicator") && classType.gameObject.activeSelf == false)
            {
                classType.gameObject.SetActive(true);
            }
            else if(classType.gameObject.name.Equals("Shadow_Indicator") && classType.gameObject.activeSelf == true)
            {
                classType.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayHighlights()
    {
        GameObject animationArchive = GameObject.Find("Eye_Animation");
        foreach(Transform ogChild in animationArchive.transform)
        {
            foreach(Transform child in ogChild)
            {
                if(child.name.IndexOf("Highlight") != -1 && child.gameObject.activeSelf == false)
                {
                    child.gameObject.SetActive(true);
                }
                else if(child.name.IndexOf("Highlight") != -1 && child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        animationArchive = GameObject.Find("Animation Archive");
        foreach(Transform classType in animationArchive.transform)
        {
            if(classType.gameObject.name.Equals("Highlight_Indicator") && classType.gameObject.activeSelf == false)
            {
                classType.gameObject.SetActive(true);
            }
            else if(classType.gameObject.name.Equals("Highlight_Indicator") && classType.gameObject.activeSelf == true)
            {
                classType.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayLipstick()
    {
        GameObject animationArchive = GameObject.Find("Mouth_Animation");
        foreach(Transform ogChild in animationArchive.transform)
        {
            foreach(Transform child in ogChild)
            {
                if(child.name.IndexOf("Lipstick") != -1 && child.gameObject.activeSelf == false)
                {
                    child.gameObject.SetActive(true);
                }
                else if(child.name.IndexOf("Lipstick") != -1 && child.gameObject.activeSelf == true)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        animationArchive = GameObject.Find("Animation Archive");
        foreach(Transform classType in animationArchive.transform)
        {
            if(classType.gameObject.name.Equals("Lipstick_Indicator") && classType.gameObject.activeSelf == false)
            {
                classType.gameObject.SetActive(true);
            }
            else if(classType.gameObject.name.Equals("Lipstick_Indicator") && classType.gameObject.activeSelf == true)
            {
                classType.gameObject.SetActive(false);
            }
        }
    }

    public void ResetDisplay()
    {
        foreach(Transform child in GameObject.Find("Selection Lists").transform)
        {
            if(child.name.Equals("Faces Selection") == false)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}








//removing previous object
/*foreach(Transform child in GameObject.Find(objectCategory + " Holder").transform)
{
    Destroy(child.gameObject);
}*/

/*
        //duplicating animation archive
        GameObject objectAnimationArchive = GameObject.Find("Animation Archive");
        foreach(Transform child in GameObject.Find("Animation Archive").transform)
        {
            if(child.name == objectCategory + "_Animation")
            {
                objectAnimationArchive = child.gameObject;
            }
        }
        GameObject animationDuplicate = Instantiate(objectAnimationArchive, location.position, location.rotation);
        animationDuplicate.SetActive(true);
        animationDuplicate.name = objectName;
        animationDuplicate.transform.SetParent(GameObject.Find(objectCategory + " Holder").transform, false);

        //removing other objects under same category
        foreach(Transform child in animationDuplicate.transform)
        {
            if(child.name.IndexOf(objectName) == -1 && child.name.IndexOf("bone") == -1)
            {
                Destroy(child.gameObject);
            }
        }*/