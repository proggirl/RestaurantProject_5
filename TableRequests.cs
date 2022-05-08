using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FinalProject5
{
    public class TableRequests: IEnumerable<string>
    {
        public bool InProccess = false;

        Dictionary<string, List<IItemInterface>> menuItems = new Dictionary<string, List<IItemInterface>>();
        public List<IItemInterface> this[string customer] { get => menuItems[customer]; }
        public void Add<T>(string customer) where T : IItemInterface 
        {
            if (menuItems.ContainsKey(customer))
            {
                menuItems[customer].Add(Activator.CreateInstance<T>());
                return;
            }

            List<IItemInterface> list = new List<IItemInterface>() {Activator.CreateInstance<T>() };            
           

            menuItems.Add(customer, list);
        }
      
        public ICollection<T> Get<T>()

        {
            ICollection<T> list = new Collection<T>();
           
            foreach(var item in menuItems)
            {
                var t = item.Value.Where(x => x.GetType() == typeof(T)).ToList();
              
                foreach (var i in t )
                {
                    list.Add((T)i);
                }
            }
            return list;
        }
       
        public List<IItemInterface> GetMenuItemsCustomer(string customer)
        {
            List<IItemInterface> list = new List<IItemInterface>();
            menuItems.TryGetValue(customer, out list);
            return list;            
        }

        IEnumerator  IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<string> GetEnumerator()
        {
            foreach(var e in menuItems)
            {
                yield return e.Key;
            }
        }
     
    }
    public interface IItemInterface
    {
        void Obtain();
        void Serve();
        Type GetType();
    }

}
