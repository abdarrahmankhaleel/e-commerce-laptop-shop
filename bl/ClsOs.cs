using LapShop.Models;
namespace LapShop.Bl
{
    public interface IO
    {
        public List<TbO> GetAll();
        public TbO GetById(int id);
        public bool Save(TbO O);
        public bool Dekete(int id);


    }
    public class ClsOs: IO
    {
        public List<TbO> GetAll()
        {
            try
            {
                LapShopContext context = new LapShopContext();
                var lstOs = context.TbOs.Where(a => a.CurrentState == 1).ToList();
                return lstOs;
            }
            catch
            {
                return new List<TbO>();
            }
        }

        public TbO GetById(int id)
        {
            try
            {
                LapShopContext context = new LapShopContext();
                var O = context.TbOs.FirstOrDefault(a => a.OsId == id && a.CurrentState == 1);
                return O;
            }
            catch
            {
                return new TbO();
            }
        }

        public bool Save(TbO O)
        {
            try
            {
                LapShopContext context = new LapShopContext();
                if (O.OsId == 0)
                {
                    O.CreatedBy = "1";
                    O.CreatedDate = DateTime.Now;
                    context.TbOs.Add(O);
                }
                else
                {
                    O.UpdatedBy = "1";
                    O.UpdatedDate = DateTime.Now;
                    context.Entry(O).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                LapShopContext context = new LapShopContext();
                var O = GetById(id);
                O.CurrentState = 1;
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
