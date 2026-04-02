
using System;
using System.Collections.Generic;

[Serializable]
public class GameContent
{
    public string version;
    public Economy economy;
    public string[] resource_types;
    public string[] tag_pool;
    public NPC[] npcs;
    public Request[] requests;
    public Day[] days;
    public SelfVerification self_verification;
    public ExamplePlayerPath example_player_path;
}

[Serializable]
public class Economy
{
    public int initial_coins;
    public int stipend_per_interval;
    public int stipend_interval_requests;
}

[Serializable]
public class Day
{
    public int day;
    public string[] requests;
}

[Serializable]
public class TagCategory
{
    public string name;
    public string[] values;
}

[Serializable]
public class NPC
{
    public string id;
    public string name;
    public string role;
    public string[] tags;
    public string notes;
}

[Serializable]
public class Request
{
    public string id;
    public string npc_id;
    public string title;
    public string request_text;
    public bool locked;
    public RequestOptions options;
    public Link[] links;
}

[Serializable]
public class RequestOptions
{
    public ConsequenceBlock deny;
    public ConsequenceBlock accept;
    public BargainVariant[] bargain;
}

[Serializable]
public class ConsequenceBlock
{
    public ResourcesDelta resources_delta;
    public FavorDelta[] favors_delta;
    public Flag[] flags_set;
    public string[] unlocks;
}

[Serializable]
public class ResourcesDelta
{
    public int coins;
    public int reputation;
}


[Serializable]
public class BargainVariant
{
    public string label;
    public BargainRequirements requirements;
    public ConsequenceBlock result;
}

[Serializable]
public class BargainRequirements
{
    public int coins_spend;
    public FavorRef[] favors_spend;
    public FlagRef[] flags_required;
}

[Serializable]
public class FavorDelta
{
    public string tag;
    public int delta;
}

[Serializable]
public class FavorRef
{
    public string tag;
    public int amount;
}


[Serializable]
public class Flag
{
    public string key;
    public bool value;
}

[Serializable]
public class FlagRef
{
    public string key;
    public bool value;
}

[Serializable]
public class Link
{
    public string on; // "deny" | "accept" | "bargain"
    public string to_request_id;
    public string strength; // "tight" | "loose"
    public string effect; // "available" | "harder" | "easier" | "modified"
    public string note;
}

[Serializable]
public class SelfVerification
{
    public GameLogEntry[] game_log;
}

[Serializable]
public class GameLogEntry
{
    public int day;
    public string request_id;
    public string choice; // "deny" | "accept" | "bargain"
    public string bargain_label; // nullable
    public string immediate_consequence;
    public ResourcesDelta resources_delta;
    public FavorDelta[] favors_delta;
    public string next_request_id; // nullable
    public bool predictable;
}

[Serializable]
public class ExamplePlayerPath
{
    public PlayerStep[] steps;
    public string summary;
}

[Serializable]
public class PlayerStep
{
    public string request_id;
    public string choice; // "deny" | "accept" | "bargain"
    public string bargain_label; // nullable
    public string reason;
    public string aftereffects;
}
