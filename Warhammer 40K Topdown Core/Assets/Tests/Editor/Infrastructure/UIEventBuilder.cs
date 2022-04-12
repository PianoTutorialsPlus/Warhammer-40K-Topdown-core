using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class UIEventBuilder : TestDataBuilder<IManageUIEvents>
    {
        private InfoUIEventChannelSO _playerEventListener;
        private InfoUIEventChannelSO _enemyEventListener;

        public UIEventBuilder()
        {

        }
        public UIEventBuilder WithPlayerEventListener(InfoUIEventChannelSO eventListener)
        {
            _playerEventListener = eventListener;
            return this;
        }
        public UIEventBuilder WithEnemyEventListener(InfoUIEventChannelSO eventListener)
        {
            _enemyEventListener = eventListener;
            return this;
        }


        public override IManageUIEvents Build()
        {
            var uIEvents = Substitute.For<IManageUIEvents>();
            uIEvents.InfoUIEvent.Returns(_playerEventListener ??= An.InfoUIEventChannel);
            uIEvents.EnemyInfoUIEvent.Returns(_enemyEventListener ??= An.InfoUIEventChannel);

            return uIEvents;
        }
    }
}
