using System.Collections.Generic;

namespace BlogVue.WebAPI
{
    public partial class ApiResponseObject<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }

    public partial class ApiResponseObject : ApiResponseObject<object> { }
}
