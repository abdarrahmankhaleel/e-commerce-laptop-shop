
namespace LapShop.Models
{
    public class VmHomePage
    {
        public VmHomePage()
        {
            lstAllItems = new List<VwItem>();
            lstCategories = new List<TbCategory>();
            lstFreeDilevryItems = new List<VwItem>();
            lstSliders = new List<TbSlider>();
            lstRecemonededIttems = new List<VwItem>();
        }

        public List<VwItem>  lstAllItems { get; set; }
        public List<VwItem> lstFreeDilevryItems { get; set; }

        public List<VwItem> lstRecemonededIttems { get; set; }
        public List<TbCategory> lstCategories { get; set; }
        public List<TbSlider> lstSliders { get; set; }
    }
}
