using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.GameMechanics
{
    public class MovementPhasesTests
    {
        public int counter;

        public IGamePhase GetGamePhase()
        {
            return A.GamePhase
                .WithGameStats(A.GameStats
                    .WithActiveUnit(A.Unit.Build()))
                .WithBattleroundEvent(An.IPhaseEvent.Build())
                .Build();
        }
        public void SetHandlePhase(IGamePhase gamePhase)
        {
            gamePhase.BattleroundEvents
                .When(x => x.HandlePhase(Arg.Any<GameStatsSO>()))
                .Do(x => counter++);
        }
        public void SetClearPhase(IGamePhase gamePhase)
        {
            gamePhase.BattleroundEvents
                .When(x => x.ClearPhase(Arg.Any<GameStatsSO>()))
                .Do(x => counter++);
        }
        public void SetMovementPhaseProcessor(IGamePhase gamePhase)
        {
            MovementPhaseProcessor processor = A.MovementPhaseProcessor.WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
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
                IGamePhase gamePhase = GetGamePhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.HandlePhase(MovementPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_BattleRoundEvent_HandlePhase_Is_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetHandlePhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.HandlePhase(MovementPhase.Move);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
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
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.ClearPhase(MovementPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                MovementPhaseProcessor.ClearPhase(MovementPhase.Move);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
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
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsFalse(MovementPhaseProcessor.Next(MovementPhase.Selection));
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_Then_Next_Is_False()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsFalse(MovementPhaseProcessor.Next(MovementPhase.Move));
            }
            [Test]
            public void When_MovementPhase_State_Is_Move_And_Unit_Is_Done_Then_Next_Is_True()
            {
                IGamePhase gamePhase = GetGamePhase();
                gamePhase.GameStats.ActiveUnit.IsDone.Returns(true);
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsTrue(MovementPhaseProcessor.Next(MovementPhase.Move));
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_Next_Is_True()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);

                Assert.IsTrue(MovementPhaseProcessor.Next(MovementPhase.Next));
            }
            [Test]
            public void When_MovementPhase_State_Is_Next_Then_ActiveUnit_Is_Null()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetMovementPhaseProcessor(gamePhase);
                MovementPhaseProcessor.Next(MovementPhase.Next);

                Assert.IsNull(gamePhase.GameStats.ActiveUnit);
            }
        }
    }
}
