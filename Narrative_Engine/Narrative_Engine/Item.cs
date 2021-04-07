public class Item
{
    public Item(string name, ItemType type, bool used, Character character){
        this.type = type;
        this.itemName = name;
        this.used = used;
        this.owner = character;
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

    //Funcion de efecto
    public virtual void Effect(){}

    //Get owner
    public Character getOwner(){
        return owner;
    }

    //Cambio de dueño
    public void setOwner(Character newOwner){
        this.owner = newOwner;
    }
}