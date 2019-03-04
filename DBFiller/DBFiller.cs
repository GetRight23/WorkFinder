using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DBFiller
{
    public abstract class DBFiller
    {
		public DBFiller(Storage storage)
        {
			Storage = storage;
		}

		public abstract void fillEntities();
		public Storage Storage { get; private set; }
	}
}
