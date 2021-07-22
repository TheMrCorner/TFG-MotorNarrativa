using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal class Item
    {
        public Item(string name){
            //this.Type = type;
            this.itemName = name;
            //this.used = used;
            //this.owner = character;
        } 

        public enum ItemType
        {
            Key,
            Consumable
        }
        
        //Nombre del objeto
        private string itemName;
        //Tipo clave o consumible
        private ItemType type;
    
        //Usado o Consumido
        private bool used;
        //Poseedor
        private Character owner;

        internal ItemType Type { get => type; set => type = value; }

        //Funcion de efecto
        public virtual void Effect(){}


    }
}