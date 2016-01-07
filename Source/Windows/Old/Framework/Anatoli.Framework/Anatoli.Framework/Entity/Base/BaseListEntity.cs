using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Framework.Entity.Base
{
    public class BaseListEntity<TemplateData> : IList
            where TemplateData : BaseEntity
    {

        protected const int _defaultCapacity = 16;

        protected TemplateData[] _array = null;
        protected int _count = 0;

        public BaseListEntity()
        {
            this._array = CreateArray(_defaultCapacity);
        }
        public virtual TemplateData[] CreateArray(int capacity)
        {
            return new TemplateData[capacity];
        }

        public BaseListEntity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");

            this._array = CreateArray(capacity);
        }
        public BaseListEntity(BaseListEntity<TemplateData> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this._array = CreateArray(collection.Count);
            AddRange(collection);
        }

        public BaseListEntity(TemplateData[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            this._array = CreateArray(array.Length);
            AddRange(array);
        }
        public virtual void AddRange(TemplateData[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (array.Length == 0) return;
            if (this._count + array.Length > this._array.Length)
                EnsureCapacity(this._count + array.Length);

            Array.Copy(array, 0, this._array, this._count, array.Length);
            foreach (TemplateData s in array)
            {
                //s.SetObjectOwner((BaseDataSV2<T>)this);
            }

            this._count += array.Length;
        }

        public virtual void AddRange(BaseListEntity<TemplateData> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (collection.Count == 0) return;
            if (this._count + collection.Count > this._array.Length)
                EnsureCapacity(this._count + collection.Count);

            Array.Copy(collection.InnerArray, 0,
                this._array, this._count, collection.Count);

            foreach (TemplateData s in collection)
            {
                //s.SetObjectOwner((BaseDataSV2<T>)this);
            }

            this._count += collection.Count;
        }

        public virtual object SyncRoot
        {
            get { return this; }
        }

        public virtual int Capacity
        {
            get { return this._array.Length; }
            set
            {
                if (value == this._array.Length) return;

                if (value < this._count)
                    throw new ArgumentOutOfRangeException("Capacity",
                        value, "Value cannot be less than Count.");

                if (value == 0)
                {
                    this._array = new TemplateData[_defaultCapacity];
                    return;
                }

                TemplateData[] newArray = new TemplateData[value];
                Array.Copy(this._array, newArray, this._count);
                this._array = newArray;
            }
        }

        public virtual int Count
        {
            get { return this._count; }
        }

        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        private void EnsureCapacity(int minimum)
        {
            int newCapacity = (this._array.Length == 0 ?
                _defaultCapacity : this._array.Length * 2);

            if (newCapacity < minimum) newCapacity = minimum;
            Capacity = newCapacity;
        }
        public virtual int Add(TemplateData value)
        {
            if (this._count == this._array.Length)
                EnsureCapacity(this._count + 1);

            this._array[this._count] = value;
            //value.SetObjectOwner((BaseDataSV2<T>)this);
            this._count++;
            return this._count - 1;
        }
        int IList.Add(object value)
        {
            return Add((TemplateData)value);
        }

        public virtual void Clear()
        {
            if (this._count == 0) return;

            Array.Clear(this._array, 0, this._count);
            this._count = 0;
        }

        public bool Contains(TemplateData value)
        {
            return (IndexOf(value) >= 0);
        }

        bool IList.Contains(object value)
        {
            return Contains((TemplateData)value);
        }

        public virtual int IndexOf(TemplateData value)
        {
            return Array.IndexOf(this._array, value, 0, this._count);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((TemplateData)value);
        }

        public virtual void Insert(int index, TemplateData value)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index",
                    index, "Argument cannot be negative.");

            if (index > this._count)
                throw new ArgumentOutOfRangeException("index",
                    index, "Argument cannot exceed Count.");

            if (this._count == this._array.Length)
                EnsureCapacity(this._count + 1);

            if (index < this._count)
                Array.Copy(this._array, index,
                    this._array, index + 1, this._count - index);

            this._array[index] = value;
            ++this._count;
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (TemplateData)value);
        }
        public virtual void Remove(TemplateData value)
        {
            int index = IndexOf(value);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        void IList.Remove(object value)
        {
            Remove((TemplateData)value);
        }

        private void ValidateIndex(int index)
        {

            if (index == -1 && _count > 0)
            {
                return;
            }
            if (index < 0)
                throw new ArgumentOutOfRangeException("index",
                    index, "Argument cannot be negative.");

            if (index >= this._count)
                throw new ArgumentOutOfRangeException("index",
                    index, "Argument must be less than Count.");
        }

        public virtual void RemoveAt(int index)
        {
            ValidateIndex(index);

            TemplateData obj = this[index];


            if (index < --this._count)
            {
                Array.Copy(this._array, index + 1,
                    this._array, index, this._count - index);
            }

            this._array[this._count] = default(TemplateData);

        }

        public virtual void RemoveRange(int index, int count)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index",
                    index, "Argument cannot be negative.");

            if (count < 0)
                throw new ArgumentOutOfRangeException("count",
                    count, "Argument cannot be negative.");

            if (index + count > this._count)
                throw new ArgumentException(
                    "Arguments denote invalid range of elements.");

            if (count == 0) return;

            for (int i = index; i < index + count; i++)
            {
                this.RemoveAt(i);
            }


        }

        public virtual TemplateData this[int index]
        {
            get
            {
                ValidateIndex(index);
                if (index == -1 && _count > 0) index = _count - 1;
                return this._array[index];
            }
            set
            {
                ValidateIndex(index);
                this._array[index] = value;
            }
        }
        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (TemplateData)value; }
        }

        private void CheckEnumIndex(int index)
        {
            if (index < 0 || index >= this._count)
                throw new InvalidOperationException(
                    "Enumerator is not on a collection element.");
        }

        public virtual IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private void CheckTargetArray(Array array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Rank > 1)
                throw new ArgumentException(
                    "Argument cannot be multidimensional.", "array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex",
                    arrayIndex, "Argument cannot be negative.");
            if (arrayIndex >= array.Length)
                throw new ArgumentException(
                    "Argument must be less than array length.", "arrayIndex");

            if (this._count > array.Length - arrayIndex)
                throw new ArgumentException(
                    "Argument section must be large enough for collection.", "array");
        }
        public virtual void CopyTo(object[] array)
        {
            CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(object[] array, int arrayIndex)
        {
            CheckTargetArray(array, arrayIndex);
            Array.Copy(this._array, 0, array, arrayIndex, this._count);
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            CheckTargetArray(array, arrayIndex);
            CopyTo((object[])array, arrayIndex);
        }

        protected virtual TemplateData[] InnerArray
        {
            get { return this._array; }
        }

        private sealed class Enumerator :
            IEnumerator
        {

            private readonly BaseListEntity<TemplateData> _collection;
            private readonly int _version;
            private int _index;

            internal Enumerator(BaseListEntity<TemplateData> collection)
            {
                this._collection = collection;
                this._index = -1;
            }

            public TemplateData Current
            {
                get
                {
                    this._collection.CheckEnumIndex(this._index);
                    return this._collection[this._index];
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return (++this._index < this._collection.Count);
            }

            public void Reset()
            {
                this._index = -1;
            }
        }
    }


}
