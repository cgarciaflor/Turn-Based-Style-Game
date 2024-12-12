using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MousePositionOnScreenText : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI posOnScreen;
    MouseInput mouseInput;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }
    // Update is called once per frame
    void Update()
    {
        if (mouseInput.active == true)
        {
            posOnScreen.text = "Position " + mouseInput.posOnGrid.x.ToString() + ":" + mouseInput.posOnGrid.y.ToString();
        }
        else
        {
            posOnScreen.text = "Outside";
        }
    }
}
