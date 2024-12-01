using UnityEngine;

public class FollowingTests : MonoBehaviour
{

    public Transform followTrans;

    public Transform cube1;

    public Transform cube2;

    public Transform cube3;

    public Transform cube4;

    public Transform cube5;

    public Transform cube6;

    public Transform fly1;
    private float cube1VelocityX;
    private float cube2VelocityX;
    private float cube3VelocityX;
    private float cube4VelocityX;
    private float cube5VelocityX;
    private Vector3 cube6Velocity;

    private void Start()
    {
        followTrans.gameObject.LeanDelayedCall(3f, moveFollow).setOnStart(moveFollow).setRepeat(-1);

        LeanTween.followDamp(cube6, followTrans, LeanProp.position, 0.6f);
    }

    void Update()
    {
        var pos = cube1.position;
        pos.x = LeanSmooth.damp(cube1.position.x, followTrans.position.x, ref cube1VelocityX, 1.1f);
        cube1.position = pos;

        pos = cube2.position;
        pos.x = LeanSmooth.spring(cube2.position.x, followTrans.position.x, ref cube2VelocityX, 1.1f);
        cube2.position = pos;

        pos = cube3.position;
        pos.x = LeanSmooth.bounceOut(cube3.position.x, followTrans.position.x, ref cube3VelocityX, 1.1f);
        cube3.position = pos;

        //pos = cube4.position;
        //pos.x = LeanTween.smoothQuint(cube4.position.x, followTrans.position.x, ref cube4VelocityX, 1.1f);
        //cube4.position = pos;

        pos = cube5.position;
        pos.x = LeanSmooth.linear(cube5.position.x, followTrans.position.x, 10f);
        cube5.position = pos;


        // cube6.position = LeanTween.smoothGravity(cube6.position, followTrans.position, ref cube6Velocity, 1.1f);

        if (LeanTween.isTweening(0))
        {
            Debug.Log("Tweening");
        }
    }

    private void moveFollow()
    {
        followTrans.LeanMove(new Vector3(Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0f), 0f);
    }
}