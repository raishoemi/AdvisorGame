using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Assign a .json file here")]
    public TextAsset jsonFile;   // drag the JSON file in Inspector

    [Header("UI")]
    public Image npcImage;
    public Text npcNameText;      // or use UnityEngine.UI.Text
    public Button nextButton;

    [Header("Portraits (assign sprites here)")]
    public List<Sprite> portraitSprites;


    private GameContent data;



    void Awake()
    {

        if (jsonFile == null) { Debug.LogError("No JSON assigned."); return; }
        data = JsonUtility.FromJson<GameContent>(jsonFile.text);
        for (int i = 0; i < data.npcs.Length; i++)
        {
            Debug.Log($"NPC {i + 1}: {data.npcs[i].name}, Role: {data.npcs[i].role}");
        }

        nextButton.onClick.AddListener(ShowRandomEncounter);
    }

    void ShowRandomEncounter()
    {
        int randomIndex = Random.Range(0, data.requests.Length);
        Request randomRequest = data.requests[randomIndex];
        Debug.Log($"Random Request: {randomRequest.title} ({randomRequest.id})");

        // Random number between one and three
        const int randomImage = Random.Range(1, 4);

        NPC associatedNPC = System.Array.Find(data.npcs, npc => npc.id == randomRequest.npc_id);

        npcImage.sprite = $"NPCProfiles/gay{randomImage}.jpg";
    }

}
