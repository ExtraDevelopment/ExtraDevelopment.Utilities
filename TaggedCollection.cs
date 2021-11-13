using System.Collections.Generic;
using System.Collections.Immutable;

namespace ExtraDevelopment.Utilities
{
    public class TaggedCollection<T>
    {
        private readonly Dictionary<string, HashSet<T>> _tagItems = new();
        private readonly Dictionary<T, HashSet<string>> _itemTags = new();

        public ImmutableHashSet<T> this[string tag]
            => _tagItems.TryGetValue(tag, out var items)
                ? items.ToImmutableHashSet()
                : ImmutableHashSet.Create<T>();

        public ImmutableHashSet<string> this[T item]
            => _itemTags.TryGetValue(item, out var tags)
                ? tags.ToImmutableHashSet()
                : ImmutableHashSet.Create<string>();

        public IEnumerable<T> TaggedItems => _itemTags.Keys;

        public IEnumerable<string> Tags => _tagItems.Keys;

        public void Tag(T item, string tag)
        {
            if (!_itemTags.TryGetValue(item, out var tags))
                _itemTags[item] = new HashSet<string> { tag };
            else tags.Add(tag);

            if (!_tagItems.TryGetValue(tag, out var items))
                _tagItems[tag] = new HashSet<T> { item };
            else items.Add(item);
        }

        public void UnTag(T item, string tag)
        {
            if (_itemTags.TryGetValue(item, out var tags))
            {
                tags.Remove(tag);
                if (tags.Count == 0)
                    _itemTags.Remove(item);
            }

            if (_tagItems.TryGetValue(tag, out var items))
            {
                items.Remove(item);
                if (items.Count == 0)
                    _tagItems.Remove(tag);
            }
        }
    }
}