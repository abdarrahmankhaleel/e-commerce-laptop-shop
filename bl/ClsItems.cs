using LapShop.Models;

namespace LapShop.Bl
{
    public interface IItems
    {
        public List<TbItem> GetAll();
        public List<VwItem> GetAllDataForIt(int? catid);
        public List<VwItem> GetRelatedItems(int ItemId);

        public TbItem GetById(int id);

        public VwItem GetVwItemById(int id);
        public bool Save(TbItem item);
        public bool Dekete(int id);

    }
    public class ClsItems : IItems
    {
        LapShopContext context;
        public ClsItems(LapShopContext context)
        {
            this.context = context;
        }

        public List<TbItem> GetAll()
        {
            try
            {
                var lstItems = context.TbItems.ToList();
                return lstItems;
            }
            catch
            {
                return new List<TbItem>();
            }
        }
        public List<VwItem> GetRelatedItems(int ItemId)
        {
            try
            {
                var item=context.VwItems.FirstOrDefault(x=>x.ItemId==ItemId);  
                var lstRelatedItems = context.VwItems.Where(x=>x.SalesPrice<=item.SalesPrice+50
                &&x.SalesPrice>=item.SalesPrice-50).ToList();
                return lstRelatedItems;
            }
            catch
            {
                return new List<VwItem>();
            }
        }

        public List<VwItem> GetAllDataForIt(int? catid)
        {
            try
            {
                //var quer = from i in context.TbItems
                //           join c in context.TbCategories
                //           on i.CategoryId equals c.CategoryId
                //           join it in context.TbItemTypes
                //           on i.ItemTypeId equals it.ItemTypeId
                //           join oss in context.TbOs
                //           on i.OsId equals oss.OsId
                //           select new VwItem
                //           {
                //               ItemId = i.ItemId,

                //           }
                var lstItems=context.VwItems.Where(a=>(a.CategoryId==catid ||catid==null||catid==0)
                &&a.CurrentState==1).OrderByDescending(c=>c.CreatedDate).ToList();
                return lstItems;
            }
            catch
            {
                return new List<VwItem>();
            }
        }

        public TbItem GetById(int id)
        {
            try
            {
                var item = context.TbItems.FirstOrDefault(a => a.ItemId == id&&a.CurrentState==1);
                return item;
            }
            catch
            {
                return new TbItem();
            }
        }
        
        public VwItem GetVwItemById(int id)
        {
            try
            {
                var item = context.VwItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState==1);
                return item;
            }
            catch
            {
                return new VwItem();
            }
        }


        public bool Save(TbItem item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                    item.CurrentState=1;
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                    context.TbItems.Add(item);
                }
                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                    context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var item = GetById(id);
                item.CurrentState = 0;
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
