
using System;
using System.Collections.Generic;

[Serializable]
public class GameContent
{
    public string version;
    public string[] resource_types;
    public TagCategory[] tag_categories;
    public NPC[] npcs;
    public Request[] requests;
    public SelfVerification self_verification;
    public ExamplePlayerPath example_player_path;
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
    public Dictionary<string, string> tags;
    public string notes;
}

[Serializable]
public class Request
{
    public string id;
    public string npc_id;
    public string type; // "over" | "under" | "gray"
    public string title;
    public string description;
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
    public Dictionary<string, int> resources_delta;
    public FavorDelta[] favors_delta;
    public Flag[] flags_set;
    public UnlockRef[] unlocks;
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
    public Dictionary<string, int> resources_spend;
    public FavorRef[] favors_spend;
    public FlagRef[] flags_required;
}

[Serializable]
public class FavorDelta
{
    public string category;
    public string value;
    public int delta;
}

[Serializable]
public class FavorRef
{
    public string category;
    public string value;
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
public class UnlockRef
{
    public string request_id;
    public string mode; // "available" | "harder" | "easier" | "modified"
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
    public Dictionary<string, int> resources_delta;
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
