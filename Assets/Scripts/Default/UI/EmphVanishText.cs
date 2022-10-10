using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MEC;

public class EmphVanishText : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] TextMeshProUGUI text;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string incomingText)
    {
        this.text.text = incomingText;
    }
    CoroutineHandle coroutine;
    public void ComeAndVanishUpward(float duration = 1f)
    {
        if (coroutine != null)
        {
            Timing.KillCoroutines(coroutine);
        }
        coroutine = Timing.RunCoroutine(ComeAndVanish(duration));
    }
    IEnumerator<float> ComeAndVanish(float duration)
    {
        yield return Timing.WaitForSeconds(0.3f);
        //rectTransform.localScale = Vector3.one;
        //var color = rectTransform.gameObject.GetComponent<TextMeshProUGUI>().color;
        //color.a = 1;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            //t = t * (3f - 2f * t);
            t = t * t * (3f - 2f * t);
            rectTransform.localScale = new Vector3
                (
                    Mathf.Lerp(1f, 3.5f, t),
                    Mathf.Lerp(1f, 3.5f, t),
                    rectTransform.localScale.z
                );
            yield return Timing.WaitForOneFrame;
        }

        time = 0;
        while (time<duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            //t = t * (3f - 2f * t);
            t = t * t * (3f - 2f * t);
            rectTransform.localScale = new Vector3
                (
                    Mathf.Lerp(3.5f, 1, t),
                    Mathf.Lerp(3.5f, 1, t),
                    rectTransform.localScale.z
                );
            yield return Timing.WaitForOneFrame;
        }
        //time = 0;
        //while (time < duration)
        //{
        //    time += Time.deltaTime;
        //    float t = time / duration;
        //    //text.maxVisibleLines =Mathf.FloorToInt(Mathf.Lerp(text.maxVisibleLines, 0, time / duration));
        //    color.a = Mathf.Lerp(1,0, t);
        //    rectTransform.anchoredPosition = new Vector2
        //        (
        //            rectTransform.localPosition.x,
        //            Mathf.Lerp(0, 200, t)
        //        );

        //    yield return null;
        //}
    }
}
