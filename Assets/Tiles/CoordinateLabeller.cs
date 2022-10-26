using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



[ExecuteAlways] //Executes scripts in edit mode and play mode
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeller : MonoBehaviour
{

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockColor = Color.black;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void  Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }
    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColour();
        ToggleLabels();
    }
    void SetLabelColour()
    {// debugging function to change colour of coordinates
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            {
                label.color = blockColor;
            }
        }
    }
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        //define the coordinates of the object based on it's position, depending on the snap settings in the editor

        label.text = coordinates.x + "," + coordinates.y;
    }

void UpdateObjectName()
{
    transform.parent.name = coordinates.ToString();
    //gets the coordinates and then sets the name in editor to them as a string
}

}
