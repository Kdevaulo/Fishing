using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Kdevaulo.Fishing.Tools
{
    internal sealed class Pool<T> where T : BaseView
    {
        private readonly List<PoolItem> _activeItems;
        private readonly List<PoolItem> _cachedItems;
        private readonly T _prefab;

        private readonly Dictionary<PoolItem, T> _viewsByPoolItems = new Dictionary<PoolItem, T>();

        internal Pool(T prefab, int capacity)
        {
            _prefab = prefab;
            _cachedItems = new List<PoolItem>(capacity);
            _activeItems = new List<PoolItem>(capacity);

            CreateDisabledItems(capacity);
        }

        internal PoolItem Get()
        {
            if (_cachedItems.Count > 0)
            {
                return GetFirstCachedItem();
            }

            return CreateNewItem();
        }

        internal void Return(PoolItem poolItem)
        {
            poolItem.Disable();
            _cachedItems.Add(poolItem);
        }

        internal T GetView(PoolItem poolItem)
        {
            return _viewsByPoolItems[poolItem];
        }

        private PoolItem GetFirstCachedItem()
        {
            var cacheditem = _cachedItems.First();
            cacheditem.Enable();

            _activeItems.Add(cacheditem);
            _cachedItems.RemoveAt(0);
            return cacheditem;
        }

        private PoolItem CreateNewItem()
        {
            var poolItem = CreatePoolItem();
            poolItem.Enable();

            _activeItems.Add(poolItem);
            return poolItem;
        }

        private void CreateDisabledItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var poolItem = CreatePoolItem();
                poolItem.Disable();
                _cachedItems.Add(poolItem);
            }
        }

        private PoolItem CreatePoolItem()
        {
            // todo: here we can change fish visual (create fish prefab provider that provides random fish)
            var view = Object.Instantiate(_prefab);
            var poolItem = view.gameObject.AddComponent<PoolItem>();

            view.Initialize();
            _viewsByPoolItems.Add(poolItem, view);
            return poolItem;
        }
    }
}