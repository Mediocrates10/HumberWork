using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private const string playerPrefix = "Player";

    private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();

    public static void RegisterPlayer(string netID, PlayerManager player)
    {
        string playerID = playerPrefix + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static PlayerManager GetPlayer(string playerID)
    {
        return players[playerID];
    }

}
