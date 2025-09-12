using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class GameManager : MonoBehaviour
{
    [Header("Assign a .json file here")]
    public TextAsset jsonFile;   // drag the JSON file in Inspector

    [Header("UI")]
    public Image npcImage;
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI NPCRole;
    public TextMeshProUGUI NPCTags;
    public TextMeshProUGUI NPCDescription;

    // Request
    public TextMeshProUGUI RequestTitle;
    public TextMeshProUGUI RequestDescription;

    public TextMeshProUGUI AcceptResourceDelta;
    public Button ChooseAcceptButton;
    public TextMeshProUGUI DenyResourceDelta;
    public Button ChooseDenyButton;

    public TextMeshProUGUI CurrentPlayerResources;

    public Button nextButton;

    [Header("Portraits (assign sprites here)")]
    public List<Sprite> sprites;


    private GameContent _data;
    private Request _currentRequest = null;
    private GameState _gameState;


    void Awake()
    {
        _data = JsonUtility.FromJson<GameContent>(jsonFile.text);
        _gameState = new GameState(_data.economy);

        nextButton.onClick.AddListener(Start);
        ChooseAcceptButton.onClick.AddListener(() => ChooseConsequence(_currentRequest.options.accept));
        ChooseDenyButton.onClick.AddListener(() => ChooseConsequence(_currentRequest.options.deny));
        // ChooseBargainButton.onClick.AddListener(() => ChooseConsequence(_currentRequest.options.bargain));
    }

    void Start()
    {
        _currentRequest = GetRandomRequest();
        UpdateUI();
    }

    void UpdateUI()
    {
        NPC associatedNPC = _data.npcs.First(npc => npc.id == _currentRequest.npc_id);
        AssignRandomNPCImage();
        AssignNPCDetails(associatedNPC);
        AssignRequestDetails(_currentRequest);
        AssignAcceptDenyDetails(AcceptResourceDelta, _currentRequest.options.accept);
        AssignAcceptDenyDetails(DenyResourceDelta, _currentRequest.options.deny);
        AssignCurrentPlayerResources();
    }

    private void AssignRandomNPCImage()
    {
        int randomSpriteIndex = Random.Range(0, sprites.Count);
        Sprite randomSprite = sprites[randomSpriteIndex];
        npcImage.sprite = randomSprite;
    }

    private void AssignNPCDetails(NPC npc)
    {
        NPCName.text = npc.name;
        NPCRole.text = npc.role;
        NPCDescription.text = npc.notes;

        // Join the tags array into a comma-separated string
        NPCTags.text = string.Join(", ", npc.tags);
    }

    private void AssignRequestDetails(Request request)
    {
        RequestTitle.text = request.title;
        RequestDescription.text = request.request_text;
    }

    private void AssignAcceptDenyDetails(TextMeshProUGUI consequenceText, ConsequenceBlock consequence)
    {
        consequenceText.text = "";

        if (consequence.resources_delta.coins != 0)
            consequenceText.text += ConvertResourceDeltaToText(consequence.resources_delta.coins) + " Coins";

        if (consequence.resources_delta.reputation != 0)
            consequenceText.text += "\n" + ConvertResourceDeltaToText(consequence.resources_delta.reputation) + " Reputation";

        if (consequence.favors_delta != null)
        {
            foreach (var favor in consequence.favors_delta)
            {
                consequenceText.text += "\n" + ConvertResourceDeltaToText(favor.delta) + $" Favor ({favor.tag})";
            }
        }
    }

    private string ConvertResourceDeltaToText(int delta)
    {
        return (delta > 0 ? "+" : "") + delta.ToString();
    }

    private void ChooseConsequence(ConsequenceBlock consequence)
    {
        if (!_gameState.HasSufficientResources(consequence, null))
        {
            Debug.Log("Insufficient resources to choose this option.");
            return;
        }

        _gameState.UpdateResources(consequence, _currentRequest.id);
        _gameState.FinishedRequestIDs.Add(_currentRequest.id);

        consequence.unlocks.ToList().ForEach(unlock =>
        {
            _data.requests.First(r => r.id == unlock).locked = false;
        });

        _currentRequest = GetRandomRequest();
        UpdateUI();
    }

    private Request GetRandomRequest()
    {
        // Get unlocked requests that haven't been finished yet
        var availableRequests = _data.requests.Where(r => !r.locked &&
                                                         !_gameState.FinishedRequestIDs.Contains(r.id))
                                                     .ToList();

        if (availableRequests.Count == 0)
            throw new System.Exception("No available requests remaining.");

        int randomIndex = Random.Range(0, availableRequests.Count);
        return availableRequests[randomIndex];
    }

    private void AssignCurrentPlayerResources()
    {
        CurrentPlayerResources.text = $"\nDay: {_gameState.CurrentDay}";
        CurrentPlayerResources.text += $"\nCoins: {_gameState.Coins}";
        CurrentPlayerResources.text += $"\nReputation: { _gameState.Reputation} ";
        _gameState.Favors.ForEach(favor =>
        {
            CurrentPlayerResources.text += $"\nFavor ({favor.tag}): {favor.delta}";
        }); 
    }

}
