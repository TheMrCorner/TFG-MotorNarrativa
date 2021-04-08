using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class CharacterController
{
    private List<Character> characters;

    public CharacterController(List<Character> characters)
    {
        this.characters = characters;
    }
    public void AddCharacter(Character character) => characters.Add(character);
    public void RemoveCharacter(Character character) => characters.Remove(character);
    public void AddItemToCharacter(Item item, Character character) => character.AddItemToInventory(item);
    public void RemoveItemFromCharacter(Item item, Character character) => character.RemoveItemFromInventory(item);
}
