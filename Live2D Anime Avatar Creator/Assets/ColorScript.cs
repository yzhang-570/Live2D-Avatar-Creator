using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorScript : MonoBehaviour
{

    [SerializeField] private GameObject currentObject;
    [SerializeField] private GameObject oneColor;
    [SerializeField] private GameObject twoColor;
    [SerializeField] private GameObject threeColor;
    [SerializeField] private GameObject num1Button;
    [SerializeField] private GameObject num2Button;
    [SerializeField] private GameObject num3Button;
    [SerializeField] private GameObject specialOpsCanvas;
    private int colorSelect;
    private int colorNumber;

    void Start()
    {
        num1Button.SetActive(false);
        num2Button.SetActive(false);
        num3Button.SetActive(false);
    }

    public void ChangeButton() 
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        string objectName = button.name.Substring(0, button.name.IndexOf(" Button"));
        currentObject = GameObject.Find(objectName + "_Anim");
        //UnityEngine.Debug.Log("object pressed: " + objectName + "_Anim");
    }

    public void ChangeButton(GameObject myObj)
    {
        currentObject = myObj;
    }

    public void ChangeColorNumber()
    {
        colorNumber = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        //UnityEngine.Debug.Log("current color number: " + colorNumber);
    }

    public void DisplayColors()
    {
        specialOpsCanvas.SetActive(true);
        string objectName = currentObject.name.Substring(0, currentObject.name.IndexOf("_Anim"));
        //UnityEngine.Debug.Log("displaying colors for " + objectName);
        if(objectName.Equals("Earrings_Gem") || objectName.Equals("Acc_Armor") || objectName.Equals("Acc_Necklace")
        || objectName.Equals("Acc_NecklaceThick") || objectName.Equals("Acc_NecklaceThin") || objectName.Equals("HeadAcc_Military")
        || objectName.Equals("HeadAcc_Rose") || objectName.Equals("Clothes_China") || objectName.Equals("Clothes_Fantasy")
        || objectName.Equals("Clothes_Kimono") || objectName.Equals("Clothes_Military") || objectName.Equals("Clothes_Wedding")
        || objectName.Equals("Clothes_ShirtDress") || objectName.Equals("Jackets_Fluff") || objectName.Equals("Jackets_Cape"))
        {
            twoColor.SetActive(true);
            oneColor.SetActive(false);
            threeColor.SetActive(false);
            colorSelect = 2;
            num1Button.SetActive(true);
            num2Button.SetActive(true);
            num3Button.SetActive(false);
        }
        else if(objectName.Equals("Back_Sword") || objectName.Equals("Clothes_Uniform") || objectName.Equals("Jackets_Tech"))
        {
            threeColor.SetActive(true);
            twoColor.SetActive(false);
            oneColor.SetActive(false);
            colorSelect = 3;
            num1Button.SetActive(true);
            num2Button.SetActive(true);
            num3Button.SetActive(true);
        }
        else
        {
            oneColor.SetActive(true);
            twoColor.SetActive(false);
            threeColor.SetActive(false);
            colorSelect = 1;
            num1Button.SetActive(false);
            num2Button.SetActive(false);
            num3Button.SetActive(false);
        }
        /*else if(objectName.Equals("Back_Sword") || objectName.Equals("Clothes_Uniform") || objectName.Equals("Jackets_Tech"))
        {
            colorSelect = threeColor;
        }*/
    }

    public void SetColor()
    {
        //UnityEngine.Debug.Log("color button name: " + EventSystem.current.currentSelectedGameObject.name);
        GameObject colorObject = EventSystem.current.currentSelectedGameObject;
        Color newColor = colorObject.GetComponent<Image>().color;
        //eye
        if(currentObject.name.IndexOf("Eye") != -1)
        {
            //UnityEngine.Debug.Log("eye color changed");
            /*if(currentObject.GetComponent<SpriteRenderer>() != null && currentObject.name.IndexOf("_Ball") != -1)
            {
                UnityEngine.Debug.Log("eyeball color changed");
                currentObject.GetComponent<SpriteRenderer>().color = newColor;
            }*/
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            GameObject objectClass = GameObject.Find(currentObject.name.Substring(0, currentObject.name.IndexOf("_")) + "_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("_Ball") != -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                            UnityEngine.Debug.Log("child name: " + child.gameObject.name + " and eyeball color changed");
                        }
                    }
                }
            }
        }
        //eyebrow
        if(currentObject.name.IndexOf("Eyebrow") != -1)
        {
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            GameObject objectClass = GameObject.Find(currentObject.name.Substring(0, currentObject.name.IndexOf("_")) + "_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("_Default") != -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                            UnityEngine.Debug.Log("child name: " + child.gameObject.name + " and eyebrow color changed");
                        }
                    }
                }
            }
        }
        //blush
        if(currentObject.name.IndexOf("Blush") != -1)
        {
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            GameObject objectClass = GameObject.Find(currentObject.name.Substring(0, currentObject.name.IndexOf("_")) + "_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    myObject.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    UnityEngine.Debug.Log("child name: " + myObject.gameObject.name + " and blush color changed");
                }
            }
        }
        //hair
        if(currentObject.name.IndexOf("FrontHair") != -1 || currentObject.name.IndexOf("Hair") != -1 && currentObject.name.IndexOf("BackHair") == -1)
        {
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            GameObject objectClass = GameObject.Find(currentObject.name.Substring(0, currentObject.name.IndexOf("_")) + "_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    myObject.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    if(myObject.childCount > 0)
                    {
                        foreach(Transform child in myObject)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                    UnityEngine.Debug.Log("child name: " + myObject.gameObject.name + " and FrontHair/Hair color changed");
                }
            }
        }
        //hair
        if(currentObject.name.IndexOf("BackHair") != -1)
        {
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            //UnityEngine.Debug.Log("in BackHair section");
            GameObject objectClass = GameObject.Find(currentObject.name.Substring(0, currentObject.name.IndexOf("_")) + "_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                foreach(Transform child in myObject)
                {
                    if(child.gameObject.name.IndexOf("bone") == -1)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
                UnityEngine.Debug.Log("child name: " + myObject.gameObject.name + " and BackHair color changed");
            }
        }
        //lipstick color?
        if(currentObject.name.IndexOf("Mouth") != -1)
        {
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            GameObject objectClass = GameObject.Find(currentObject.name.Substring(0, currentObject.name.IndexOf("_")) + "_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                foreach(Transform child in myObject)
                {
                    if(child.gameObject.name.IndexOf("bone") == -1 && child.gameObject.name.IndexOf("Lipstick") != -1)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
                UnityEngine.Debug.Log("child name: " + myObject.gameObject.name + " and Mouth color changed");
            }
        }
        //changes skin color
        if(currentObject.name.IndexOf("Faces") != -1 || currentObject.name.IndexOf("Ears") != -1 || currentObject.name.IndexOf("Hand") != -1)
        {
            UnityEngine.Debug.Log("current category: " + currentObject.name.Substring(0, currentObject.name.IndexOf("_")));
            GameObject objectClass = GameObject.Find("Faces_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                foreach(Transform child in myObject)
                {
                    if(child.gameObject.name.IndexOf("bone") == -1 && child.gameObject.name.IndexOf("Default") != -1)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
            }
            objectClass = GameObject.Find("Body_Animation (always displayed)");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("Default") != -1)
                {
                    myObject.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                }
            }
            objectClass = GameObject.Find("Eye_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Wrinkle") != -1 && child.gameObject.name.IndexOf("Wrinkle_Tint") == -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
            objectClass = GameObject.Find("Eyebrow_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Shadow") != -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
            objectClass = GameObject.Find("Mouth_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Default") != -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
            objectClass = GameObject.Find("Nose_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    myObject.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                }
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Tint") == -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
            objectClass = GameObject.Find("Ears_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Tint") == -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
            objectClass = GameObject.Find("EarDefault_Animation (always displayed)");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Tint") == -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
            objectClass = GameObject.Find("Hand_Animation");
            foreach(Transform myObject in objectClass.transform)
            {
                if(myObject.gameObject.name.IndexOf("bone") == -1)
                {
                    foreach(Transform child in myObject)
                    {
                        if(child.gameObject.name.IndexOf("Tint") == -1)
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                        }
                    }
                }
            }
        }
        if(currentObject.name.IndexOf("FaceAcc") != -1)
        {
            if(currentObject.GetComponent<SpriteRenderer>() != null)
            {
                currentObject.GetComponent<SpriteRenderer>().color = newColor;
            }
            foreach(Transform child in currentObject.transform)
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
        }
        if(currentObject.name.IndexOf("Earrings") != -1 || currentObject.name.IndexOf("Acc") != -1
        || currentObject.name.IndexOf("Back") != -1 || currentObject.name.IndexOf("Clothes") != -1
        || currentObject.name.IndexOf("Jackets") != -1)
        {
            if(colorSelect == 1 || colorNumber == 0)
            {
                //UnityEngine.Debug.Log("one total color");
                if(currentObject.GetComponent<SpriteRenderer>() != null)
                {
                    currentObject.GetComponent<SpriteRenderer>().color = newColor;
                }
                foreach(Transform child in currentObject.transform)
                {
                    if(child.gameObject.name.IndexOf("bone") == -1)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
            }
            else if(colorSelect == 2)
            {
                //UnityEngine.Debug.Log("two total colors");
                if(currentObject.GetComponent<SpriteRenderer>() != null)
                {
                    currentObject.GetComponent<SpriteRenderer>().color = newColor;
                }
                foreach(Transform child in currentObject.transform)
                {
                    /*if(colorObject.transform.parent.gameObject.name.Equals("Color1") && child.gameObject.name.IndexOf("Color1") != -1
                    || colorObject.transform.parent.gameObject.name.Equals("Color2") && child.gameObject.name.IndexOf("Color2") != -1)*/
                    if(colorNumber == 1 && child.gameObject.name.IndexOf("Color1") != -1
                    || colorNumber == 2 && child.gameObject.name.IndexOf("Color2") != -1)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
            }
            else if(colorSelect == 3)
            {
                //UnityEngine.Debug.Log("three total colors");
                if(currentObject.GetComponent<SpriteRenderer>() != null)
                {
                    currentObject.GetComponent<SpriteRenderer>().color = newColor;
                }
                foreach(Transform child in currentObject.transform)
                {
                    UnityEngine.Debug.Log(colorNumber + ", " + child.gameObject.name);
                    if(colorNumber == 1 && child.gameObject.name.IndexOf("Color1") != -1
                    || colorNumber == 2 && child.gameObject.name.IndexOf("Color2") != -1
                    || colorNumber == 3 && child.gameObject.name.IndexOf("Color3") != -1)
                    {
                        //UnityEngine.Debug.Log(colorNumber + ", " + child.gameObject.name);
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
            }
        }
        if(currentObject.name.IndexOf("Wallpaper") != -1)
        {
            if(currentObject.GetComponent<SpriteRenderer>() != null)
            {
                currentObject.GetComponent<SpriteRenderer>().color = newColor;
            }
        }
    }
}