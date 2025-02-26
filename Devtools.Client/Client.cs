﻿using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Devtools.Client.Controllers;
using Devtools.Client.Menus;
using JetBrains.Annotations;

namespace Devtools.Client {
    [UsedImplicitly]
    public class Client : BaseScript {

        private bool _isInstantiated;

        public Client() {
            Menu = new MenuController(this);
            Tools = new DevTools(this);
            NoClip = new NoclipController(this);
            new EntityDebugger(this);
            PlayerList = Players;

            RegisterEventHandler("playerSpawned", new Action(OnSpawn));

            if (Game.PlayerPed.Position != default)
                OnSpawn();
        }

        public PlayerList PlayerList { get; }
        public DevTools Tools { get; }
        public MenuController Menu { get; }
        public NoclipController NoClip { get; }

        private void OnSpawn() {
            if (!_isInstantiated)
                TriggerServerEvent("Client.Ready");
            _isInstantiated = true;
        }

        public void RegisterEventHandler(string eventName, Delegate action) {
            EventHandlers[eventName] += action;
        }

        public void RegisterTickHandler(Func<Task> tick) {
            Tick += tick;
        }

/*
        public void DeregisterTickHandler(Func<Task> tick) {
            Tick -= tick;
        }
*/
    }
}