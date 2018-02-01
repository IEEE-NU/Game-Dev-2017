using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    private float fillAmount;
    [SerializeField] private Image content;

    public void BarHandler(float min, float max, float currPercentage)
    {
        fillAmount = Mathf.Lerp(min, max, currPercentage);
        content.fillAmount = fillAmount;

        /*Debug.Log(min);
        Debug.Log(max);
        Debug.Log(currPercentage);
        Debug.Log(fillAmount);
        Debug.Log("This is barScript");*/

    }

   
}
