﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    public class EntityDeletingEventArgs<T> : EventArgs
    {
        public T SavedEntity;

    }
}

