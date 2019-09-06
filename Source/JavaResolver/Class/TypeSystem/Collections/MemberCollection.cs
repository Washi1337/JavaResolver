using System;
using System.Collections.ObjectModel;

namespace JavaResolver.Class.TypeSystem.Collections
{
    public abstract class MemberCollection<TMember> : Collection<TMember>
        where TMember : IMemberReference
    {
        public MemberCollection(ClassDefinition owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public ClassDefinition Owner
        {
            get;
        }

        private static void AssertIsNotAddedToClass(TMember item)
        {
            if (item.DeclaringClass != null)
                throw new InvalidOperationException("Cannot add a field that is already added to another class.");
        }

        protected override void ClearItems()
        {
            foreach (var item in Items)
                SetOwner(item, null);
            base.ClearItems();
        }

        protected override void SetItem(int index, TMember item)
        {
            AssertIsNotAddedToClass(item);
            var oldField = this[index];
            base.SetItem(index, item);
            SetOwner(oldField, null);
            SetOwner(item, Owner);
        }

        protected override void InsertItem(int index, TMember item)
        {
            AssertIsNotAddedToClass(item);
            base.InsertItem(index, item);
            SetOwner(item, Owner);
        }

        protected override void RemoveItem(int index)
        {
            var oldField = this[index];
            base.RemoveItem(index);
            SetOwner(oldField, null);
        }

        protected abstract void SetOwner(TMember member, ClassDefinition newOwner);
    }
}