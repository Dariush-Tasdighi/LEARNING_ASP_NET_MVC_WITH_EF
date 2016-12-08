namespace Models
{
	public class Role : BaseEntity
	{
		public Role()
			: base()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Active")]
		public bool IsActive { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50)]
		public string Name { get; set; }
		// **********

		// **********
		public virtual System.Collections.Generic.IList<User> Users { get; set; }
		// **********
	}
}
