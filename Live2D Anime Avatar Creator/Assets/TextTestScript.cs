using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTestScript : MonoBehaviour
{
    void Start()
    {
        GameObject myText = GameObject.Find("My Text");
        myText.GetComponent<TextMeshProUGUI>().text = "this is my new text!!";
    }
}
