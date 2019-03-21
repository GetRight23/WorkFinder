namespace Models
{
	public partial class Photo : DBObject
	{

		public int Id { get; set; }

		public byte Data { get; set; }

		public int IdUser { get; set; }

		public override int getId() { return Id; }

		public virtual User IdUserNavigation { get; set; }
	}
}
