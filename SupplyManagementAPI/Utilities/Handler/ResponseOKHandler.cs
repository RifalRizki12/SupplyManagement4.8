namespace SupplyManagementAPI.Utilities.Handler
{
    public class ResponseOKHandler<TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }

        public ResponseOKHandler(TEntity data)
        {
            Code = 200; // Menggunakan nilai langsung 200 untuk OK.
            Status = "OK";
            Message = "Success to Retrieve Data";
            Data = data;
        }

        public ResponseOKHandler(string message)
        {
            Code = 200; // Menggunakan nilai langsung 200 untuk OK.
            Status = "OK";
            Message = message;
            Data = default(TEntity);
        }

        public ResponseOKHandler(string message, TEntity data)
        {
            Code = 200; // Menggunakan nilai langsung 200 untuk OK.
            Status = "OK";
            Message = message;
            Data = data;
        }

        public ResponseOKHandler()
        {
            Code = 200; // Menggunakan nilai langsung 200 untuk OK.
            Status = "OK";
            Message = "Success";
            Data = default(TEntity);
        }
    }
}
