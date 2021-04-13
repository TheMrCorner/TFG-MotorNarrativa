using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    class ItemController{
        private List<Item> items;

        public ItemController(List<Item> itemList){
            this.items = itemList;
        }

        //Cambio de dueño o recoger
        public void changeOwner(Character newOwner, Item item){
            if(item.getOwner() != null){
                item.getOwner().RemoveItemFromInventory(item);
            }
            item.setOwner(newOwner);
            newOwner.AddItemToInventory(item);
        }

        public void addItem (Item item){
            items.Add(item);
        }

        public void removeIyem(Item item){
            items.Remove(item);
        }
    }
}