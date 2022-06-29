namespace TrackingSystem.Domain.Common
{
    public class BaseSeed<T>
    {
        protected IEnumerable<T> Items;

        public IEnumerable<T> GetItems() => Items;

        public BaseSeed(IEnumerable<T> items)
        {
            Items = items;
        }
    }
}
