using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WH40K.Essentials;
using WH40K.GameMechanics;
using WH40K.UI;

namespace Editor.GameMechanics
{
    public class MovementPhaseM_SelectionTests
    {
        public class TheSubEventsMethod
        {
            

            [Test]
            public void When_M_Selection_Has_An_Instance_Then_SubEvents_Returns_MovementPhase_Selection()
            {
                IGamePhase phase = Substitute.For<IGamePhase>();
                var selection = new M_Selection(phase);
                Assert.AreEqual(MovementPhase.Selection, selection.SubEvents);
            }

            //[Test]
            //public void When_HandlePhase_Is_Called_Then_BattleRoundEvent_HandlePhase_Is_Raised()
            //{
            //    var counter = 0;
            //     IGamePhase gamePhase = Substitute.For<IGamePhase>();

            //    gamePhase.When(x => x.BattleroundEvents.HandlePhase(Arg.Any<GameStatsSO>()))
            //        .Do(x => counter++);

            //    var selection = new M_Selection(gamePhase);
            //    selection.HandlePhase(A.GameStats);
            //    Assert.AreEqual(1, counter);
            //}
        }
    }
}
