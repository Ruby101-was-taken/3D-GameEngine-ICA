using GD.Items;

namespace RUB {
    public class ItemPickup {
        public ItemData item;
        public int count;

        public ItemPickup(ItemData item, int count = 1) {
            this.item = item;
            this.count = count; 
        }
    }
}
