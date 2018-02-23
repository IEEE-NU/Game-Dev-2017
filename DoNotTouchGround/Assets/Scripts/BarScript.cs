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
    }

    public IEnumerator OverheatBar()
    {
        content.fillAmount = 1;
        for (var n = 0; n < 15; n++)
        {
            content.color = Color.white;
            yield return new WaitForSeconds(.1f);
            content.color = Color.red;
            yield return new WaitForSeconds(.1f);
        }
        content.color = Color.white;
    }


}
