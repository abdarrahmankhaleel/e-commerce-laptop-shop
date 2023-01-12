using LapShop.Models;
namespace LapShop.Bl
{

    public interface IItemType
    {
        public List<TbItemType> GetAll();
        public TbItemType GetById(int id);
        public bool Save(TbItemType ItemType);
        public bool Dekete(int id);
    }
    public class ClsItemTypes : IItemType
    {
        LapShopContext context;
        public ClsItemTypes(LapShopContext context)
        {
            this.context = context;
        }
        public List<TbItemType> GetAll()
        {
            try
            {
                
                var lstItemTypes = context.TbItemTypes.Where(a=>a.CurrentState==1).ToList();
                return lstItemTypes;
            }
            catch
            {
                return new List<TbItemType>();
            }
        }

        public TbItemType GetById(int id)
        {
            try
            {
                
                var ItemType = context.TbItemTypes.FirstOrDefault(a => a.ItemTypeId == id && a.CurrentState == 1);
                return ItemType;
            }
            catch
            {
                return new TbItemType();
            }
        }

        public bool Save(TbItemType ItemType)
        {
            try
            {
                
                if (ItemType.ItemTypeId == 0)
                {
                    ItemType.CreatedBy = "1";
                    ItemType.CreatedDate = DateTime.Now;
                    context.TbItemTypes.Add(ItemType);
                }
                else
                {
                    ItemType.UpdatedBy = "1";
                    ItemType.UpdatedDate = DateTime.Now;
                    context.Entry(ItemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Dekete(int id)
        {
            try
            {
                
                var ItemType = GetById(id);
                ItemType.CurrentState=0;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
