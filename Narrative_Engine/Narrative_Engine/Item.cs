public class Item
{
    public Item(string name, ItemType type, bool pickedUp, bool used, Character character){
        this.type = type;
        this.itemName = name;
        this.pickUp = pickedUp;
        this.used = used;
        this.owner = character;
    } 

    public enum ItemType
    {
        Key,
        Consumable
    }
    
    //Nombre del objeto
    public string itemName;
    //Tipo clave o consumible
    public ItemType type;
    //Está en el invetario
    public bool pickUp;
    //Usado o Consumido
    public bool used;
    //Poseedor
    public Character owner;

    //Funcion de efecto
    public virtual void Effect(){}

    //Add Inventory
    public void pickUpItem(){
        if(!pickUp){
            pickUp = true;
            owner.addItem(this);
        }
    }

    //Cambio de dueño
    public void setOwner(Character newOwner){
        owner.removeItem(this);
        this.owner = newOwner;
        owner.addItem(this);
    }

}