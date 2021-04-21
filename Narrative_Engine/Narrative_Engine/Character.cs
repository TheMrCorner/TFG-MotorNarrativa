using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    class Character
    {
        public enum CharacterType
        {
            PRIMARYNPC,
            SECONDARYNPC,
            PLAYER
        }

        private string characterName;
        private Place place;
        private CharacterType type;
        private Dictionary<Item.ItemType, List<Item>> inventory;
        // List<Dialog> dialogs; TODO
        // Other stats

        public Character(string characterName, Place place, CharacterType type)
        {
            this.characterName = characterName;
            this.place = place;
            this.type = type;
            inventory = new Dictionary<Item.ItemType, List<Item>>();
            foreach (Item.ItemType itemType in Enum.GetValues(typeof(Item.ItemType)))
            {
                inventory.Add(itemType, new List<Item>());
            }
        }

        public string GetCharacterName() => characterName;
        public Place GetPlace() => place;
        public CharacterType GetCharacterType() => type;
        public void SetPlace(Place place) => this.place = place;
        public void AddItemToInventory(Item item) => inventory[item.Type].Add(item);
        public void RemoveItemFromInventory(Item item) => inventory[item.Type].Remove(item);
    }
}