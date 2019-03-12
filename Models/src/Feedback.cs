using System;

namespace Models
{
	public partial class Feedback : DBObject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Patronymic { get; set; }
		public int GradeValue { get; set; }
		public DateTime Date { get; set; }
		public string Text { get; set; }
		public int IdWorker { get; set; }

		public override int getId() { return Id; }

		public virtual Worker IdWorkerNavigation { get; set; }
	}
}
