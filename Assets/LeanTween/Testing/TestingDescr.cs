using UnityEngine;

public class TestingDescr : MonoBehaviour
{

    public GameObject go;

    private int tweenId;

    // start a tween
    public void startTween()
    {
        tweenId = LeanTween.moveX(go, 10f, 1f).id;
        Debug.Log("tweenId:" + tweenId);
    }

    // check tween descr
    public void checkTweenDescr()
    {
        var descr = LeanTween.descr(tweenId);
        Debug.Log("descr:" + descr);
        Debug.Log("isTweening:" + LeanTween.isTweening(tweenId));
    }
}