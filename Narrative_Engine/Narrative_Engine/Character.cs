using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Character
{
    public enum CharacterType
    {
        PRIMARYNPC,
        SECONDARYNPC,
        PLAYER
    }
        
    private string characterName;
    private KeyValuePair<float, float> position;
    private CharacterType type;
    private Dictionary<Item.ItemType, List<Item>> inventory;
    // List<Dialog> dialogs; TODO
    // Other stats

    public Character(string characterName, KeyValuePair<float, float> position, CharacterType type)
    {
        this.characterName = characterName;
        this.position = position;
        this.type = type;
        inventory = new Dictionary<Item.ItemType, List<Item>>();
        foreach(Item.ItemType itemType in Enum.GetValues(typeof(Item.ItemType)))
        {
            inventory.Add(itemType, new List<Item>());
        }
    }

    public string GetCharacterName() => characterName;
    public KeyValuePair<float, float> GetPosition() => position;
    public CharacterType GetCharacterType() => type;
    public void AddItemToInventory(Item item) => inventory[item.type].Add(item);
    public void RemoveItemFromInventory(Item item) => inventory[item.type].Remove(item);
}
