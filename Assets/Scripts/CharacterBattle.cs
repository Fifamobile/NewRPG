using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{

    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;

    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }

    private void Awake()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                float slideSpeed = 10f;
                transform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime;

                float reachedDistance = 1f;
                if(Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance)
                {
                    //Arrived at Slide Target Position
                    transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;
        }
    }

    public Vector3 GetPosition()
    {
    return transform.position; 
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        SlideToPosition(targetCharacterBattle.GetPosition(), () =>
        {

        });
    }

    private void SlideToPosition(Vector3 position, Action onSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }

}

