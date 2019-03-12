using System.Collections.Generic;

namespace Models
{
	public partial class ProfessionCategory : DBObject
	{
		public ProfessionCategory()
		{
			Profession = new HashSet<Profession>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public override int getId() { return Id; }

		public virtual ICollection<Profession> Profession { get; set; }
	}
}
