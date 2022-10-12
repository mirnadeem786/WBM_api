using System.Data;

namespace WillowBatMarketWebApiService.Models
{
    public class SQLParam
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public SqlDbType Type { get; set; }
        public int Length { get; set; }

        public static SQLParam create(string field, object value, SqlDbType type, int len = 0)
        {
            SQLParam param = new SQLParam();
            param.Value = value;
            param.Field = field;
            param.Type = type;
            param.Length = len;
            return param;
        }

    }
}
