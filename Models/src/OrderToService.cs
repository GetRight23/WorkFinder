namespace Models
{
	public partial class OrderToService
	{
		public int IdService { get; set; }
		public int IdOrder { get; set; }
		public int Id { get; set; }

		public virtual Order IdOrderNavigation { get; set; }
		public virtual Service IdServiceNavigation { get; set; }
	}
}
