using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DBFiller
{
    public abstract class DBFiller
    {
		protected Logger Logger { get; private set; }
		public Storage Storage { get; private set; }
		public FileLoader FileLoader { get; private set; }
		public Random Random { get; set; }

		public DBFiller(Storage storage)
		{
			Logger = LogManager.GetCurrentClassLogger();
			Storage = storage;
			FileLoader = new FileLoader();
			Random = new Random();
		}

		public abstract void fillEntities();
	}
}
