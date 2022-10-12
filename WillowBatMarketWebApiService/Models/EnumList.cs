namespace WillowBatMarketWebApiService.Models
{
    public enum OperationEnum
    {
        get_all,
        add,
        update,
        delete,
        get_by_id,
        Get_Bats_by_Manufac_Id
    }
    public static class EntityType
    {
        public static readonly string WILLOWSELLER = "WILLOWSELLER";
        public static readonly string BAT = "BAT";
        public static readonly string WILLOW = "WILLOW";
        public static readonly string MANUFACTURER = "MANUFACTURER";
    }

    public static class OrderStatusInfo
    {
        public static readonly string PLACED = "Placed";
        public static readonly string CONFORMED = "Conformed";
        public static readonly string PACKED = "Packed";
        public static readonly string INTRANSIT = "Intransit";
        public static readonly string DELIVERD = "Dilivered";
        public static readonly string CANCELLED = "Cancelled";
    

    }
}
