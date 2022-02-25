using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class interaction_manager_test
{
    // A Test behaves as an ordinary method
    [Test]
    public void get_next_gamephase()
    {
        //ARRANGE
        GamePhase _gamePhase = GamePhase.MovementPhase;
        GamePhase _newGamePhase = GamePhase.ShootingPhase;
        //ACT
        _gamePhase = GamePhaseProcessor.SetNextPhaseToActive(_gamePhase);
        // ASSERT
        Assert.AreEqual(_newGamePhase, _gamePhase);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

}
