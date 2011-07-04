using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.Data.Dynamic.Providers;

namespace EPiSimpleQueue.Test.Helpers
{
    public class InMemoryDynamicDataStore : DynamicDataStore
    {
        static List<object> _items;

        public InMemoryDynamicDataStore() : base(new StoreDefinition(null, null))
        {
            _items = new List<object>();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        public override Identity Save(object value)
        {
            var queueItem = value as IIdentity;
            
            if (queueItem == null)
            {
                throw new InvalidOperationException("This InMemoryImplementation only works with QueueItem and things that inherits from that");
            }

            queueItem.Id = Identity.NewIdentity().ExternalId;

            _items.Add(value);

            return queueItem.Id;
        }

        public override Identity Save(object value, TypeToStoreMapper typeToStoreMapper)
        {
            throw new NotImplementedException();
        }

        public override Identity Save(object value, Identity id)
        {
            throw new NotImplementedException();
        }

        public override Identity Save(object value, Identity id, TypeToStoreMapper typeToStoreMapper)
        {
            throw new NotImplementedException();
        }

        public override object Load(Identity id)
        {
            throw new NotImplementedException();
        }

        public override TResult Load<TResult>(Identity id)
        {
            throw new NotImplementedException();
        }

        public override PropertyBag LoadAsPropertyBag(Identity id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> LoadAll()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TResult> LoadAll<TResult>()
        {
            return _items.Where(o => o is TResult).Cast<TResult>();
        }

        public override IEnumerable<PropertyBag> LoadAllAsPropertyBag()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Find(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Find(IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TResult> Find<TResult>(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TResult> Find<TResult>(IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<PropertyBag> FindAsPropertyBag(IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<PropertyBag> FindAsPropertyBag(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override IOrderedQueryable<object> Items()
        {
            throw new NotImplementedException();
        }

        public override IOrderedQueryable<TResult> Items<TResult>()
        {
            throw new NotImplementedException();
        }

        public override IOrderedQueryable<PropertyBag> ItemsAsPropertyBag()
        {
            throw new NotImplementedException();
        }

        public override void Delete(Identity id)
        {
            var itemToDelete = _items
                .Cast<IIdentity>()
                .SingleOrDefault(q => q.Id == id);

            if(itemToDelete == null)
            {
                return;
            }

            _items.Remove(itemToDelete);
        }

        public override void Delete(object value)
        {
            throw new NotImplementedException();
        }

        public override void DeleteAll()
        {
            _items.Clear();
        }

        public override StoreDefinition StoreDefinition
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override DataStoreProvider DataStoreProvider
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}