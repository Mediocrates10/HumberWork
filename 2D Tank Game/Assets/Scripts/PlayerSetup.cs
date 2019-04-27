using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
    }
    

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();

        PlayerManager player = GetComponent<PlayerManager>();

        GameManager.RegisterPlayer(netID, player);
    }

    void DisableComponents()
    {
       for (int i = 0; i < componentsToDisable.Length; i++)
       {
           componentsToDisable[i].enabled = false;
       }
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

}
