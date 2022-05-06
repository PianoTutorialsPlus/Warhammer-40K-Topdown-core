using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.GameMechanics
{
    public class ShootingPhasesTests
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
        public void SetShootingPhaseProcessor(IGamePhase gamePhase)
        {
            ShootingPhaseProcessor processor = A.ShootingPhaseProcessor.WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
        [SetUp]
        public void BeforeEveryTest()
        {
            counter = 0;
        }
        public class TheHandlePhaseMethod : ShootingPhasesTests
        {
            [Test]
            public void When_ShootingPhase_State_Is_Selection_Then_BattleRoundEvent_HandlePhase_Is_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.HandlePhase(ShootingPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.HandlePhase(ShootingPhase.Shoot);
                Assert.AreEqual(0, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.HandlePhase(ShootingPhase.Next);
                Assert.AreEqual(0, counter);
            }
        }
        public class TheClearPhaseMethod : ShootingPhasesTests
        {
            [Test]
            public void When_ShootingPhase_State_Is_Selection_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.ClearPhase(ShootingPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.ClearPhase(ShootingPhase.Shoot);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.ClearPhase(ShootingPhase.Next);
                Assert.AreEqual(1, counter);
            }
        }
        public class TheNextMethod : ShootingPhasesTests
        {
            [Test]
            public void When_ShootingPhase_State_Is_Selection_Then_Next_Is_False()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsFalse(ShootingPhaseProcessor.Next(ShootingPhase.Selection));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_Next_Is_False()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsFalse(ShootingPhaseProcessor.Next(ShootingPhase.Shoot));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_Next_Is_True()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsTrue(ShootingPhaseProcessor.Next(ShootingPhase.Next));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_ActiveUnit_Is_Null()
            {
                IGamePhase gamePhase = GetGamePhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);
                ShootingPhaseProcessor.Next(ShootingPhase.Next);

                Assert.IsNull(gamePhase.GameStats.ActiveUnit);
            }
        }
    }
}
