using System;
using System.Data;

namespace Thunderstruck
{
    public class OutputParameterAttribute : Attribute 
    {
        int _size;
        DbType _dbType;
        public int Size
        {
            get { return this._size; }
        }

        public DbType DbType
        {
            get { return this._dbType; }
        }

        public OutputParameterAttribute(DbType dbType, int size)
        {
            this._size = size;
            this._dbType = dbType;
        }
    }
}