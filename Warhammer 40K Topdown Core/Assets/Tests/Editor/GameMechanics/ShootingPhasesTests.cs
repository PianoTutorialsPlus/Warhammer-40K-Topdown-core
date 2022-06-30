using Editor.Base;
using NUnit.Framework;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.GameMechanics
{
    public class ShootingPhasesTests : GamePhaseTestsBase
    {
        public void HandlePhase(ShootingPhase phase)
        {
            ShootingPhaseProcessor.HandlePhase(phase);
        }
        public void ClearPhase(ShootingPhase phase)
        {
            ShootingPhaseProcessor.ClearPhase(phase);
        }
        public bool Next(ShootingPhase phase)
        {
            return ShootingPhaseProcessor.Next(phase);
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
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                HandlePhase(ShootingPhase.Selection);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                HandlePhase(ShootingPhase.Shoot);

                Assert.AreEqual(0, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                HandlePhase(ShootingPhase.Next);

                Assert.AreEqual(0, counter);
            }
        }
        public class TheClearPhaseMethod : ShootingPhasesTests
        {
            [Test]
            public void When_ShootingPhase_State_Is_Selection_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ClearPhase(ShootingPhase.Selection);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ClearPhase(ShootingPhase.Shoot);

                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ClearPhase(ShootingPhase.Next);

                Assert.AreEqual(1, counter);
            }
        }
        public class TheNextMethod : ShootingPhasesTests
        {
            [Test]
            public void When_ShootingPhase_State_Is_Selection_Then_Next_Is_False()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsFalse(Next(ShootingPhase.Selection));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_Next_Is_False()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsFalse(Next(ShootingPhase.Shoot));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_Next_Is_True()
            {
                var gamePhase = GetIPhase();
                var gameStats = GetGameStats();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase,gameStats);

                Assert.IsTrue(Next(ShootingPhase.Next));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_ActiveUnit_Is_Null()
            {
                var gamePhase = GetIPhase();
                var gameStats = GetGameStats();

                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase,gameStats);

                Next(ShootingPhase.Next);

                Assert.IsNull(gameStats.ActiveUnit);
            }
        }
    }
}
