using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    private Button Button;
    private ProgramManager ProgramManagerScript;

    void Start()
    {
        ProgramManagerScript = FindObjectOfType<ProgramManager>();

        Button = GetComponent<Button>();
        Button.onClick.AddListener(RotationFunction);
        GetComponent<Image>().color = Color.red;
    }
    
    void RotationFunction()
    {
        if (ProgramManagerScript.Rotation)
        {
            ProgramManagerScript.Rotation = false;
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            ProgramManagerScript.Rotation = true;
            GetComponent<Image>().color = Color.green;
        }
    }
}
