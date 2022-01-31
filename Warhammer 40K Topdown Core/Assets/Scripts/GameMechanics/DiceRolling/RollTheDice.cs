using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTheDice : MonoBehaviour
{
    public GameObject camRoll;

    private string galleryDie = "d6-red";
    Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {

        //UpdateRoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, Screen.height - 75, Screen.width - 20, 30), "");
        GUI.Label(new Rect(20, Screen.height - 70, Screen.width, 20), Dice.AsString(""));
    }

    private Vector3 Force()
    {
        Vector3 rollTarget = Vector3.zero + new Vector3(-2 - 7 * Random.value, -.5F - 4 * Random.value, -2 - 3 * Random.value);
        return Vector3.Lerp(spawnPoint, rollTarget, 1).normalized * (-35 - Random.value * 20);
    }

    public void UpdateRoll()
    {
        spawnPoint = new Vector3(-160, 16, -5);
        //if (Input.GetMouseButtonDown(Dice.MOUSE_RIGHT_BUTTON) && !PointInRect(GuiMousePosition(), rectModeSelect))
        //{
            // right mouse button clicked so roll 8 dice of dieType 'gallery die'
            Dice.Clear();
            string[] a = galleryDie.Split('-');
            Dice.Roll("2" + a[0], galleryDie, spawnPoint, Force());
        //}
    }
}
