using Microsoft.EntityFrameworkCore;

namespace WillowBatMarketWebApiService.Entity
{
    [Keyless]
    public class ManufacturerDashboard
    {

            public string willowSellerName { get; set; }
            public string willowSellerContactNo { get; set; }
            public int willowSize { get; set; }
            public string willowType { get; set; }
            public byte[] willowImage { get; set; }
             public decimal willowPrice { get; set; }

    }
}
