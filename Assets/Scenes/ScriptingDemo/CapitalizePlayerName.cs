using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CapitalizePlayerName : MonoBehaviour
{
    [SerializeField]
    VariableStorageBehaviour storage;

    void Start()
    {
        if(storage.TryGetValue("player_name", out string playerName))
        {
            storage.SetValue("player_name", playerName.ToUpper());
        }
        else
        {
            storage.SetValue("player_name", "ANON");
        }
    }
}
