namespace ShopOnlineModels.Dtos {
    public class CartItemToAddDto {

        /*
         * This class is used to add a CartItem to a Cart.
         *
         * lets remember that a CartItem contains
         * a lot of fields but we are only interested in
         * the product id and how much of that product we are going to insert (the quantity)
         * 
         * The CartId field purpose is to indicate where
         * this new item is going to be stored (a Cart).
         */
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
