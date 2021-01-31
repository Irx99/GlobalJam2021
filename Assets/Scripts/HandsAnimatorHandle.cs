using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimatorHandle : MonoBehaviour
{
    Animator anim;
    
    public enum Anims { GRAB, PUNCH, THROW }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(Anims chosenAnim)
    {
        if (chosenAnim == Anims.GRAB)
        {
            anim.CrossFade("grab", 0.2f, 0);
        }
        else if (chosenAnim == Anims.THROW)
        {
            anim.CrossFade("throw", 0.2f, 0);
        }
        else if (chosenAnim == Anims.PUNCH)
        {
            anim.CrossFade("punch", 0.2f, 0);
        }
    }
}
