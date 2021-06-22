using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Previs_Controller : MonoBehaviour
{

    public GameObject Drone_Camera, Preview_Button, Stop_Button, Record_Button, AOI_Button;
    public Dropdown dropdown;

    private bool isRecording = false, is_Preview = false, AOI = false;

    private Contineous_movement movement_Obj;
    private Area_Of_Intrest orbit_Obj;

    private int Count = 0;

    public int speed, radius;

    public LineRenderer line;

    public Vector3[] pos_Array = new Vector3[10];

    // Start is called before the first frame update
    void Start()
    {
        line = Drone_Camera.GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRecording)
        {
            Debug.Log("Recording....");

            movement_Obj.Positions.Add(Drone_Camera.transform.position);
            movement_Obj.Rotations.Add(Drone_Camera.transform.rotation);
            pos_Array = movement_Obj.Positions.ToArray<Vector3>();
            line.positionCount = pos_Array.Length;
            line.SetPositions(pos_Array);
        }

        if (is_Preview)
        {
            if (Count >= movement_Obj.Positions.Count)
            {
                OnClick_Stop_btn();
                Debug.Log("Ending Preview...");
            }

            Debug.Log("Counting..." + Count);
            Drone_Camera.transform.position = movement_Obj.Positions[Count];
            Drone_Camera.transform.rotation = movement_Obj.Rotations[Count];


            Count++;
        }


        if (AOI)
        {
            Drone_Camera.transform.RotateAround(orbit_Obj.AOI_Position, Vector3.up, speed * Time.deltaTime);
        }
    }

    public void record()
    {
        if (!isRecording)
        {
            line.positionCount = 0;
            movement_Obj = new Contineous_movement();
            isRecording = true;
            Stop_Button.SetActive(true);
            Record_Button.SetActive(false);
            Preview_Button.SetActive(false);
        }
    }

    public void OnClick_Stop_btn()
    {
        if (isRecording)
        {
            isRecording = false;
            Debug.Log("Position List Count: " + movement_Obj.Positions.Count);
            Debug.Log("Rotation List Count: " + movement_Obj.Rotations.Count);
            pos_Array = movement_Obj.Positions.ToArray<Vector3>();
            line.positionCount = pos_Array.Length;
            line.SetPositions(pos_Array);
        }
        else if (is_Preview)
        {
            is_Preview = false;
            Count = 0;
            Drone_Camera.transform.position = movement_Obj.Positions[Count];
            Drone_Camera.transform.rotation = movement_Obj.Rotations[Count];
            pos_Array = movement_Obj.Positions.ToArray<Vector3>();
            line.positionCount = pos_Array.Length;
            line.SetPositions(pos_Array);
        }
        else if (AOI)
        {
            AOI = false;

        }


        if (dropdown.value == 0)
        {
            if (movement_Obj != null)
            {
                Preview_Button.SetActive(true);
                pos_Array = movement_Obj.Positions.ToArray<Vector3>();
                line.positionCount = pos_Array.Length;
                line.SetPositions(pos_Array);

            }
            else
            {
                line.positionCount = 0;
            }
            Record_Button.SetActive(true);
            Stop_Button.SetActive(false);
            AOI_Button.SetActive(false);

        }
        else
        {
            Preview_Button.SetActive(false);
            Record_Button.SetActive(false);
            Stop_Button.SetActive(false);
            AOI_Button.SetActive(true);
            line.positionCount = 0;
        }
    }

    public void preview()
    {
        line.positionCount = 0;
        Record_Button.SetActive(false);
        Preview_Button.SetActive(false);
        Stop_Button.SetActive(true);
        is_Preview = true;
        Count = 0;
    }


    public void Set_aoi()
    {
        orbit_Obj = new Area_Of_Intrest();
        orbit_Obj.AOI_Position = Drone_Camera.transform.position;
        Drone_Camera.transform.position = Drone_Camera.transform.position - new Vector3(radius, 0, 0);
        Drone_Camera.transform.rotation = Quaternion.Euler(90, Drone_Camera.transform.rotation.y, Drone_Camera.transform.rotation.z);

        AOI = true;

        AOI_Button.SetActive(false);
        Stop_Button.SetActive(true);

    }



    public void onDropdown_Value_Change()
    {
        OnClick_Stop_btn();
    }
}



public class Contineous_movement
{
    public List<Vector3> Positions { set; get; }
    public List<Quaternion> Rotations { set; get; }
    
    public Contineous_movement()
    {
        Positions = new List<Vector3>();
        Rotations = new List<Quaternion>();
    }
}

public class Area_Of_Intrest
{
    public Vector3 AOI_Position { get; set; }

}
