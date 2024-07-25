using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{

    private static BattleHandler instance;

    public static BattleHandler GetInstance()
    {
        return instance;
    }

    private CharacterBattle playerCharacterBattle;
    private CharacterBattle enemyCharacterBattle;
    private State state;

    private enum State
    {
        WaitingForPlayer,
        Busy,
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        state = State.WaitingForPlayer;
    }

    private void Update()
    {   if (state == State.WaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                state = State.Busy;
                playerCharacterBattle.Attack(enemyCharacterBattle, () =>
                {
                    state = State.WaitingForPlayer;
                });
            }
        }
    }
}
