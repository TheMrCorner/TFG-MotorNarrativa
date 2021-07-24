using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal class ItemController
    {
        private List<Item> items;

        internal ItemController(List<Item> itemList){
            this.items = itemList;
        }

        internal void addItem (Item item){
            items.Add(item);
        }

        internal void removeItem(Item item){
            items.Remove(item);
        }
    }
}