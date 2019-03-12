namespace Models
{
	public partial class Photo : DBObject
	{

		public int Id { get; set; }

		public string Link { get; set; }

		public int? IdUser { get; set; }

		public override int getId() { return Id; }

		public virtual User IdUserNavigation { get; set; }
	}
}
