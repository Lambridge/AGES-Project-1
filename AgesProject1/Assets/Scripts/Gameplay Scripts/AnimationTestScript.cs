using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour {

    enum ArmAnimationState { ArmsDown, ArmsUp }
    ArmAnimationState currentArmAnimationState = ArmAnimationState.ArmsDown;

    [SerializeField] Animator playerShoulderAnimator;
    [SerializeField] Animation rotationAnimation; //Should contain two "Animations" elements which are the actual animations
    
    [SerializeField] AnimationClip rotateUpward;
    [SerializeField] AnimationClip rotateDownward;
	
	// Update is called once per frame
	void Update () {
        PerformAnimation();
	}

    private void PerformAnimation()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (currentArmAnimationState)
            {
                case ArmAnimationState.ArmsDown:
                    playerShoulderAnimator.Play("rotateUpward");
                    currentArmAnimationState = ArmAnimationState.ArmsUp;
                    Debug.Log("Played up rotation animation?");
                    break;
                case ArmAnimationState.ArmsUp:
                    playerShoulderAnimator.Play("rotateDownward");
                    currentArmAnimationState = ArmAnimationState.ArmsDown;
                    Debug.Log("Played down rotation animation?");
                    break;
                default:
                    Debug.Log("Did not play an animation");
                    break;
            }
        }
    }
}
