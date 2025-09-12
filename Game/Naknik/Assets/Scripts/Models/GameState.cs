using System;
using System.Collections.Generic;
using Unity.VisualScripting;

class GameState
{
    static int REQUESTS_IN_DAY = 5;

    public int Coins;
    public int Reputation;
    public int CurrentDay;
    public List<FavorDelta> Favors;
    public Dictionary<string, bool> Flags;

    public List<string> FinishedRequestIDs { get; private set; } = new List<string>();

    private Economy _economy;

    public GameState(Economy economy)
    {
        _economy = economy;
        Coins = economy.initial_coins;
        Reputation = 0;
        CurrentDay = 1;
        Favors = new List<FavorDelta>();
        Flags = new Dictionary<string, bool>();
    }

    public void UpdateResources(ConsequenceBlock consequence, string requestId)
    {
        Coins += consequence.resources_delta.coins;
        Reputation += consequence.resources_delta.reputation;

        foreach (var favor in consequence.favors_delta)
        {
            var existingFavor = Favors.Find(f => f.tag == favor.tag);
            if (existingFavor != null)
            {
                existingFavor.delta += favor.delta;
            }
            else
            {
                Favors.Add(new FavorDelta { tag = favor.tag, delta = favor.delta });
            }
        }

        foreach (var flag in consequence.flags_set)
        {
            Flags[flag.key] = flag.value;
        }

        FinishedRequestIDs.Add(requestId);
        if (FinishedRequestIDs.Count % REQUESTS_IN_DAY == 0)
        {
            CurrentDay++;
            Coins += _economy.stipend_per_interval;
        }
    }

    public bool HasSufficientResources(ConsequenceBlock consequence, BargainRequirements? bargain_requirements)
    {
        int coins_delta = consequence.resources_delta.coins;
        if (coins_delta < 0 && Coins < -coins_delta)
            return false;

        // TODO: Check for bargains
        return true;
    }
}