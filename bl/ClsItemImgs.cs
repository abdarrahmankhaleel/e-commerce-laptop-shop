using LapShop.Models;

namespace LapShop.Bl
{
    public interface IItemImgs
    {

        public List<TbItemImage> GetByItemId(int id);

    }
    public class ClsItemImgs : IItemImgs
    {
        LapShopContext context;
        public ClsItemImgs(LapShopContext context)
        {
            this.context = context;
        }


        public List<TbItemImage>  GetByItemId(int ItemId)
        {
            try
            {
                var lstItemImgs = context.TbItemImages.Where(x=>x.ItemId== ItemId).ToList();
                return lstItemImgs;
            }
            catch
            {
                return new List<TbItemImage>();
            }
        }

      
    }
}
