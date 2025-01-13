using GD.Items;

namespace RUB {
    public class ItemPickup {
        public AnswerData item;
        public int count;

        public ItemPickup(AnswerData item, int count = 1) {
            this.item = item;
            this.count = count; 
        }
    }
}
