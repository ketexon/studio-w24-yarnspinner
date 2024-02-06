using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ShowSprite : MonoBehaviour
{
    [SerializeField] Behaviour component;

    [YarnCommand("show_sprite")]
    public void Show()
    {
        component.enabled = true;
    }

    [YarnCommand("hide_sprite")]
    public void Hide()
    {
        component.enabled = false;
    }
}
