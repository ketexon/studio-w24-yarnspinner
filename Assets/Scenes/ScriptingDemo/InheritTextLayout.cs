using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class InheritTextLayout : MonoBehaviour, ILayoutElement, ILayoutGroup
{
    [SerializeField] TextMeshProUGUI element;

    public float minWidth => element ? element.minWidth : 0;

    public float preferredWidth => element ? element.preferredWidth : 0;

    public float flexibleWidth => element ? element.flexibleWidth : 0;

    public float minHeight => element ? element.minHeight : 0;

    public float preferredHeight => element ? element.preferredHeight : 0;

    public float flexibleHeight => element ? element.flexibleHeight : 0;

    public int layoutPriority => element ? element.layoutPriority : 0;

    public void CalculateLayoutInputHorizontal()
    {
        if (element)
        {
            element.CalculateLayoutInputHorizontal();
        }
    }

    public void CalculateLayoutInputVertical()
    {
        if (element)
        {
            element.CalculateLayoutInputVertical();
        }
    }

    public void SetLayoutHorizontal()
    {
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
    }

    public void SetLayoutVertical()
    {
        // we dont need to rebuild twice, so only rebuild in horizontal
    }
}
