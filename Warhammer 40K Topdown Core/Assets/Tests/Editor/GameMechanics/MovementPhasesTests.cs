using NSubstitute;
using NUnit.Framework;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.GameMechanics
{
    public class MovementPhasesTests : GamePhaseTestsBase
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            counter = 0;
        }

        public class TheHandlePhaseMethod : MovementPhasesTests
        {
            [Test]
            public void When_MovementPhase_State_Is_Selection_Then_BattleRoundEvent_HandlePhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.HandlePhase(MovementPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_BattleRoundEvent_HandlePhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.HandlePhase(MovementPhase.Move);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.HandlePhase(MovementPhase.Next);
                Assert.AreEqual(0, counter);
            }
        }
        public class TheClearPhaseMethod : MovementPhasesTests
        {
            [Test]
            public void When_MovementPhase_State_Is_Selection_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.ClearPhase(MovementPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.ClearPhase(MovementPhase.Move);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.ClearPhase(MovementPhase.Next);
                Assert.AreEqual(1, counter);
            }
        }
        public class TheNextMethod : MovementPhasesTests
        {
            [Test]
            public void When_MovementPhase_State_Is_Selection_Then_Next_Is_False()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsFalse(MovementPhaseProcessor.Next(MovementPhase.Selection));
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_Next_Is_False()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsFalse(MovementPhaseProcessor.Next(MovementPhase.Move));
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_And_Unit_Is_Done_Then_Next_Is_True()
            {
                var gamePhase = GetIPhase();
                //GameStatsSO.ActiveUnit.IsDone.Returns(true);
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsTrue(MovementPhaseProcessor.Next(MovementPhase.Move));
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_Next_Is_True()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsTrue(MovementPhaseProcessor.Next(MovementPhase.Next));
            }
            //[Test]
            //public void When_MovementPhase_State_Is_Next_Then_ActiveUnit_Is_Null()
            //{
            //    var gamePhase = GetIPhase();
            //    SetClearPhase(gamePhase);
            //    SetMovementPhaseProcessor(gamePhase);
            //    MovementPhaseProcessor.Next(MovementPhase.Next);

            //    Assert.IsNull(GameStatsSO.ActiveUnit);
            //}
        }
    }
}
