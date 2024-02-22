using System.Data;

namespace BW_BE_S4_Ecommerce
{
    public static class ShoppingCartDataTable
    {

        private static DataTable _cartTable = new DataTable();

        public static DataTable CartTable
        {
            get { return _cartTable; }
            set { _cartTable = value; }
        }
    }
}