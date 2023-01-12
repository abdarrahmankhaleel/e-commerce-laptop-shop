namespace LapShop.Models
{
    public class VmItemDetails
    {
        public VmItemDetails()
        {

            lstItemImges = new List<TbItemImage>();
                item = new VwItem();
            lstRelatedItems = new List<VwItem>();  
        }

        public List<VwItem> lstRelatedItems { get; set; }
        public VwItem item { get; set; }

        public List<TbItemImage> lstItemImges { get; set; }
    }
}
