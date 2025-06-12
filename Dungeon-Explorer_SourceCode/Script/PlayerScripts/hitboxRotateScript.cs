using UnityEngine;

public class HitboxRotateScript : MonoBehaviour
{
    public Animator anim;
    public Transform playerTrans;
    public Rigidbody2D PlayerRb;

    private void Update()
    {
        if (anim.GetBool("IsButtonS"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (PlayerRb.linearVelocityX > 0)
                playerTrans.localScale = new Vector3(-1, 1, 1);
            else
                playerTrans.localScale = new Vector3(1, 1, 1);
        }
        else if (anim.GetBool("IsButtonU"))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            playerTrans.localScale = new Vector3(1, 1, 1);
        }
        else if (anim.GetBool("IsButtonD"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            playerTrans.localScale = new Vector3(1, 1, 1);
        }
    }
}
