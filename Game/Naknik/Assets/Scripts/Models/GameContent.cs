using System;
using System.Collections.Generic;

[Serializable]
public class GameContent
{
    public string schema_version;
    public bool from_p5;
    public string version;
    public string[] resource_types;
    public Economy economy;
    public string[] tag_pool;
    public NPC[] npcs;
    public PreviousDays previous_days;
}

[Serializable]
public class Economy
{
    public int starting_coins;
    public ReputationBands reputation_bands;
    public ReputationStart reputation_start;
    public DebtRules debt_rules;
}

[Serializable]
public class ReputationBands
{
    public ReputationBand commoners;
    public ReputationBand nobles;
    public ReputationBand guilds;
}

[Serializable]
public class ReputationBand
{
    public int min;
    public int max;
}

[Serializable]
public class ReputationStart
{
    public int commoners;
    public int nobles;
    public int guilds;
}

[Serializable]
public class DebtRules
{
    public int interest_min_pct;
    public int interest_max_pct;
    public int due_days_min;
    public int due_days_max;
}

[Serializable]
public class NPC
{
    public string id;
    public string name;
    public string role;
    public string blurb;
    public string[] tags;
}

[Serializable]
public class PreviousDays
{
    public DayContent[] days;
}

[Serializable]
public class DayContent
{
    public int index;
    public Request[] requests;
}

[Serializable]
public class Request
{
    public string id;
    public int order;
    public string npc_id;
    public string title;
    public string text;
    public string[] tags;
    public ConditionIf _if; // nullable - using underscore because 'if' is a C# keyword
    public RequestOption[] options;
}

[Serializable]
public class ConditionIf
{
    public ResourceCondition resources;
    public ReputationCondition reputations;
    public FlagCondition flags;
    public ConditionIf[] any;
    public ConditionIf[] all;
}

[Serializable]
public class ResourceCondition
{
    public IntThreshold coins;
    public IntThreshold favors;
}

[Serializable]
public class IntThreshold
{
    public int atLeast;
    public int atMost;
    public int moreThan;
    public int lessThan;
}

[Serializable]
public class ReputationCondition
{
    public IntThreshold commoners;
    public IntThreshold nobles;
    public IntThreshold guilds;
}

[Serializable]
public class FlagCondition
{
    public bool barge_priority;
    public bool harbor_weather_watch;
    public bool customs_favor;
    public bool granary_release;
    public bool market_relief;
    public bool charter_audit_cleared;
    public bool quota_push;
    public bool patronage_route;
    public bool court_introductions;
    public bool hall_audience_slot;
    public bool black_book;
    public bool quiet_guards;
}

[Serializable]
public class RequestOption
{
    public string id;
    public string type; // "accept", "deny", "bargain"
    public string label;
    public ConditionIf _if; // nullable
    public Risk risk;
    public OptionCosts costs;
    public OptionEffects effects_immediate;
    public OptionEffects effects_on_success;
    public OptionEffects effects_on_fail;
}

[Serializable]
public class Risk
{
    public float success_chance;
    public ChanceModifier[] chance_modifiers;
}

[Serializable]
public class ChanceModifier
{
    public ConditionIf _if;
    public float delta;
}

[Serializable]
public class OptionCosts
{
    public int coins;
    public int favors;
}

[Serializable]
public class OptionEffects
{
    public int coins;
    public int favors;
    public ReputationDelta reputation;
    public string[] flags_set;
    public ScheduledOutcome schedule_outcome; // nullable
    public string[] unlocks;
}

[Serializable]
public class ReputationDelta
{
    public int commoners;
    public int nobles;
    public int guilds;
}

[Serializable]
public class ScheduledOutcome
{
    public int day_index;
    public int coins;
    public int favors;
    public ReputationDelta reputation;
    public string[] flags_set;
}
