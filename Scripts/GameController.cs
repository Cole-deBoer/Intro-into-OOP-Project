using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject crosshair;
    
    public static List<GameObject> enemiesDefeated = new List<GameObject>();

    private CmdController.Client _client;
    private CommandTypes.Button _button;
    private CommandTypes.BodySwap _bodySwap;


    private void Start()
    {
        _client = new CmdController.Client();
        _client.Invoker = new CmdController.Invoker();
        _client.Receiver = new CmdController.Receiver();
        _client.Command = new CmdController.ConcreteCommand(_client.Receiver);
        _button = new CommandTypes.Button(_client.Receiver);
        _bodySwap = new CommandTypes.BodySwap(_client.Receiver);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        crosshair.transform.position = Input.mousePosition;
        
        if (Input.GetKeyDown(KeyCode.E) && SetActiveWhenInRange.InRange)
        {
            _client.Launch(_button);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _client.Launch(_bodySwap);
        }

        if (Input.GetKey(KeyCode.E) && SetActiveWhenInRange.InRange == false)
        {
            _client.Undo(_bodySwap);
        }

    }
}


