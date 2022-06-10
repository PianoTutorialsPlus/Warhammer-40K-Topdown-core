using NUnit.Framework;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.GameMechanics
{
    public class ShootingPhasesTests : GamePhaseTestsBase
    {
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

                ShootingPhaseProcessor.HandlePhase(ShootingPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                var gamePhase = GetIPhase();
                SetHandlePhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.HandlePhase(ShootingPhase.Shoot);
                Assert.AreEqual(0, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_BattleRoundEvent_HandlePhase_Is_Not_Raised()
            {
                var gamePhase = GetIPhase();
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
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.ClearPhase(ShootingPhase.Selection);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                ShootingPhaseProcessor.ClearPhase(ShootingPhase.Shoot);
                Assert.AreEqual(1, counter);
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_BattleRoundEvent_ClearPhase_Is_Raised()
            {
                var gamePhase = GetIPhase();
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
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsFalse(ShootingPhaseProcessor.Next(ShootingPhase.Selection));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Shoot_Then_Next_Is_False()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsFalse(ShootingPhaseProcessor.Next(ShootingPhase.Shoot));
            }
            [Test]
            public void When_ShootingPhase_State_Is_Next_Then_Next_Is_True()
            {
                var gamePhase = GetIPhase();
                SetClearPhase(gamePhase);
                SetShootingPhaseProcessor(gamePhase);

                Assert.IsTrue(ShootingPhaseProcessor.Next(ShootingPhase.Next));
            }
            //[Test]
            //public void When_ShootingPhase_State_Is_Next_Then_ActiveUnit_Is_Null()
            //{
            //    var gamePhase = GetIPhase();
            //    SetClearPhase(gamePhase);
            //    SetShootingPhaseProcessor(gamePhase);
            //    ShootingPhaseProcessor.Next(ShootingPhase.Next);

            //    Assert.IsNull(GameStatsSO.ActiveUnit);
            //}
        }
    }
}
