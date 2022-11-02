﻿using TaleWorlds.MountAndBlade;

namespace Crpg.Module.Common;

/// <summary>
/// Used to synchronize user data between client / server.
/// </summary>
internal class CrpgUserManagerClient : MissionNetwork
{
    private MissionNetworkComponent? _missionNetworkComponent;
    private CrpgRepresentative? _crpgRepresentative;

    public override void OnBehaviorInitialize()
    {
        base.OnBehaviorInitialize();
        _missionNetworkComponent = Mission.GetMissionBehavior<MissionNetworkComponent>();
        _missionNetworkComponent.OnMyClientSynchronized += OnMyClientSynchronized;
        _crpgRepresentative?.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
    }

    public override void OnRemoveBehavior()
    {
        base.OnRemoveBehavior();
        _missionNetworkComponent!.OnMyClientSynchronized -= OnMyClientSynchronized;
    }

    private void OnMyClientSynchronized()
    {
        _crpgRepresentative = GameNetwork.MyPeer.GetComponent<CrpgRepresentative>();
        _crpgRepresentative.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
    }
}
