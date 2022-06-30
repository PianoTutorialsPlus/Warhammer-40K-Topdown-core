using Editor.Base;
using NSubstitute;
using NUnit.Framework;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.GameMechanics
{
    public class MovementPhasesTests : GamePhaseTestsBase
    {
        public void HandlePhase(MovementPhase phase)
        {
            MovementPhaseProcessor.HandlePhase(phase);
        }
        public void ClearPhase(MovementPhase phase)
        {
            MovementPhaseProcessor.ClearPhase(phase);
        }
        public bool Next(MovementPhase phase)
        {
            return MovementPhaseProcessor.Next(phase);
        }

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

                HandlePhase(MovementPhase.Selection);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_BattleRoundEvent_HandlePhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                HandlePhase(MovementPhase.Move);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                HandlePhase(MovementPhase.Next);

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

                ClearPhase(MovementPhase.Selection);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                ClearPhase(MovementPhase.Move);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                ClearPhase(MovementPhase.Next);

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

                Assert.IsFalse(Next(MovementPhase.Selection));
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_Next_Is_False()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsFalse(Next(MovementPhase.Move));
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_And_Unit_Is_Done_Then_Next_Is_True()
            {
                var gamePhase = GetIPhase();
                var gameStats = GetGameStats(unit: GetUnit(isDone: true));

                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase, gameStats);

                Assert.IsTrue(Next(MovementPhase.Move));
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_Next_Is_True()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsTrue(Next(MovementPhase.Next));
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_ActiveUnit_Is_Null()
            {
                var gamePhase = GetIPhase();
                var gameStats = GetGameStats();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase, gameStats);

                Next(MovementPhase.Next);

                Assert.IsNull(gameStats.ActiveUnit);
            }
        }
    }
}
