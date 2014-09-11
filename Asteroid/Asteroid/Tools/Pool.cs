using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;

namespace Asteroid.Tools
{
    public interface IPoolable
    {
        void reset();
    }

    public abstract class Pool<T>
    {
        private List<T> _available = new List<T>();
        private List<T> _inUse = new List<T>();

        public T GetObject()
        {
            lock (_available)
            {
                if (_available.Count != 0)
                {
                    T po = _available[0];
                    _inUse.Add(po);
                    _available.RemoveAt(0);
                    return po;
                }
                else
                {
                    T po = newObject();
                    _inUse.Add(po);
                    return po;
                }
            }
        }

        public abstract T newObject();


        public void ReleaseObject(T obj)
        {
            reset(obj);

            lock (_available)
            {
                _available.Add(obj);
                _inUse.Remove(obj);
            }
        }

        private void reset(T obj)
        {
            if (obj is IPoolable)
                ((IPoolable)obj).reset();
            else
                throw new Exception("The object is not typeof @Poolable");
        }
    }
}
