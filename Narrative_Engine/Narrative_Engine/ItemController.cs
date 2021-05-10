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

        public void addItem (Item item){
            items.Add(item);
        }

        public void removeIyem(Item item){
            items.Remove(item);
        }
    }
}