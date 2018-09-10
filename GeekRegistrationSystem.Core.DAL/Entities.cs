using System.Collections.Generic;
using System.Linq;
using GeekRegistrationSystem.Core.DAL.Interfaces;

namespace GeekRegistrationSystem.Core.DAL
{
    public class Entities<T> : List<T> where T : IEntity<T>
    {
        public List<T> Select(List<PrimaryKey> primarykeys)
        {
            List<T> list = new List<T>();
            this.Where(i => i.PrimaryKey == primarykeys).ToList().ForEach(i => list.Add(i));
            this.ForEach(i =>
            {
                bool f = true;
                primarykeys.ForEach(p =>
                {
                    if (!(i.PrimaryKey.Contains(p)))
                        f = false;
                });
                if (f)
                    list.Add(i);
            });
            return list;
        }

        public Entities<T> Copy()
        {
            Entities<T> newList = new Entities<T>();
            this.ForEach(i => newList.Add(((IEntity<T>)i).Copy()));
            return newList;
        }
    }
}
