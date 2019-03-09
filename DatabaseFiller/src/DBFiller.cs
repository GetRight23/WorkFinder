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
		protected Logger m_logger { get; private set; }
		public DBFiller(Storage storage)
        {
			Storage = storage;
			m_logger = LogManager.GetCurrentClassLogger();
		}
		public abstract void fillEntities();
		public Storage Storage { get; private set; }
		public FileLoader fileLoader { get; private set; }
		public Random Random { get; set; }
	}
}
