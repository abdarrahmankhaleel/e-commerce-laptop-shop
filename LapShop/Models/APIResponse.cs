namespace LapShop.Models
{
    public class APIResponse
    {
        /// <summary>
        /// data coming from api
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// list of errors of fail api
        /// </summary>
        public object Errors { get; set; }
        /// <summary>
        /// 200 success 
        /// </summary>
        public string StatusCode { get; set; }
    }

}
